# Inventory and Opening/Closing Checklists – module guide

This covers the part of the K4J LPG system for **Inventory**, **Opening Checklist** and **Closing Checklist**.

> **Status:** written and structure-checked, but **not yet compiled or run**. The environment it was written in had no .NET SDK or MySQL. Build it in Visual Studio, run the SQL in XAMPP, then work through the test cases below and record the results.

## 1. Setup

1. Start **Apache** and **MySQL** in the XAMPP Control Panel.
2. Open phpMyAdmin → **SQL** tab → run `Database/01_k4j_lpg_schema.sql`, then `Database/02_sample_data.sql`.
3. Copy the new files into the project (see section 2) and open the `.slnx` in Visual Studio. NuGet restores `MySql.Data`.
4. Press **F5**. The project's startup form is currently `SuperAdminAddRiderAccount`; to see the module screens, temporarily open `AdminGlobalInventory`, `AdminOpeningChecklist`, `AdminClosingChecklist` or `SuperAdminGlobalInventory` (for example, set it as the startup form under My Project → Application).

Database settings default to XAMPP (`root`, empty password, database `k4j_lpg`). To change them without editing code, set the environment variable `K4J_DB_CONNECTION` to a full MySQL connection string.

Demo accounts (password `K4J@2026`): `superadmin@k4j.local`, `jpang.admin@k4j.local`, `daet.admin@k4j.local`.

## 2. What was added or changed

New folders (copy into the VB project folder, next to the existing `.vb` files):

| Folder | Files | Layer |
|---|---|---|
| `Models/` | `InventoryModels.vb` | shared data classes, constants |
| `Data/` | `Db.vb`, `InventoryRepository.vb`, `ChecklistRepository.vb` | Data access (SQL only, all parameterized) |
| `Services/` | `AppSession.vb`, `InventoryService.vb`, `ChecklistService.vb` | Service (rules, validation, role/branch authorization, transactions) |
| `Views/` | `BranchInventoryView.vb`, `ChecklistView.vb`, `GlobalInventoryView.vb`, `StockMovementDialog.vb`, `ChecklistAdjustmentDialog.vb`, `UiTheme.vb` | UI (Windows Forms controls built in code) |

Existing files edited (small additions only):

| File | Change |
|---|---|
| `AdminGlobalInventory.vb` | `AttachContent()` call in Load + helper that puts `BranchInventoryView` in `ContentPanel` |
| `AdminOpeningChecklist.vb` | same, with `ChecklistView(Opening)` |
| `AdminClosingChecklist.vb` | same, with `ChecklistView(Closing)` |
| `SuperAdminGlobalInventory.vb` | `AttachContent()` + positions `GlobalInventoryView` under the heading |

## 3. Connecting the login (for your teammate)

The services read the signed-in user from `AppSession.Current`. After the login form verifies the password it must call:

```vb
AppSession.SignIn(New SessionUser With {
    .UserID = ..., .FullName = ..., .Email = ...,
    .RoleID = Roles.BranchAdmin,          ' or Roles.SuperAdmin
    .BranchID = ..., .BranchName = "JPang"})   ' BranchID/BranchName Nothing for Super Admin
```

and `AppSession.SignOut()` on sign-out. `UserRepository.FindActiveByEmail(email)` returns a `SessionUser`; the password hash is in `User.PasswordHash` and is checked with the existing `PasswordSecurity.VerifyPassword`.

Until then, `AppSession.DevAutoSignIn = True` signs in a demo account when a screen opens (JPang admin, or the Super Admin on the global screen). **Set `DevAutoSignIn` to `False` once the real login works.** To try Daet, change `AppSession.DevBranchAdminEmail` to `daet.admin@k4j.local`.

## 4. How the rules are enforced

| Rule | Where |
|---|---|
| `New Balance = Previous + In − Out` | `InventoryService.ApplyMovement` (and a database CHECK) |
| `Difference = Physical − System` | database generated column; mirrored live in the grid |
| Branch Admin sees/changes only their branch | `Authorization.ResolveBranch`; the branch is never passed in from the UI for writes |
| Super Admin is view-only | write methods call `RequireBranchAdmin`, which refuses Super Admin |
| A checklist never changes stock | `ChecklistService` has no code that touches `Inventory` |
| Adjustment needs a reason and a user | `InventoryService.RecordAdjustment` / `AdjustFromChecklistLine`; recorded in `InventoryMovement` |
| Stock changes are atomic | one transaction: lock row → update balance → insert movement |
| Submitted checklists are locked | database triggers + service check |
| Stock moved while counting | submit is refused and the checklist reloads (`ChecklistChangedException`) |

## 5. Test cases

Record the outcome in the last column when you run them. Use `jpang.admin` unless stated.

| # | Test | Steps | Expected | Result |
|---|---|---|---|---|
| 1 | Stock in | Inventory → Stock in → Gasul 11 kg, Filled, 5, reason "Delivery" | Filled rises by 5; a new INCOMING row in Movement history with previous/new balance and your name | |
| 2 | Stock out | Stock out → same item, 3 | Filled falls by 3; OUTGOING row | |
| 3 | Not enough stock | Stock out more than the balance | Message "Not enough stock…"; balance unchanged; no movement row | |
| 4 | Reason required | Stock in with blank reason | Message "Enter a reason or description."; nothing saved | |
| 5 | Conditions tracked separately | Stock in 2 to Empty only | Only the Empty column changes | |
| 6 | Accessory condition | Choose Regulator | Only "General" condition is offered | |
| 7 | Adjustment | Adjust balance → set a new value with reason | Balance equals the new value; ADJUSTMENT row with reason and user | |
| 8 | Adjustment same value | Adjust to the current value | Refused ("same as the current balance") | |
| 9 | Branch separation | Compare JPang and Daet balances for the same product (Super Admin → Compare branches) | Different numbers per branch; "combined" is the sum | |
| 10 | Branch restriction | Sign in as Daet admin (`DevBranchAdminEmail`), open Inventory | Only Daet's stock and movements appear | |
| 11 | Super Admin is read-only | Open Global Inventory | No stock in/out/adjust controls; calling a write service as Super Admin throws "Only a Branch Admin…" | |
| 12 | Opening: previous closing | Open Opening Checklist | "Previous closing" shows yesterday's counts; System shows current balances | |
| 13 | Opening: difference | Enter a physical count different from System | Difference shows live (+/−), colored; Remarks cell turns red until filled | |
| 14 | Submit needs all counts | Leave a count blank → Submit | Refused ("Enter a physical count for every item") | |
| 15 | Submit needs remarks | Count differs, no remark → Submit | Refused ("Add a remark…") | |
| 16 | Save draft | Enter some counts → Save draft → Reload | Counts are restored; balances unchanged | |
| 17 | Submit locks | Fill everything → Submit | Status Submitted; grid read-only; Save/Submit hidden | |
| 18 | Discrepancy does not change stock | After submitting with a difference, open Inventory | Balance unchanged | |
| 19 | Adjust discrepancy | Select a line with a difference → Adjust selected line → reason | ADJUSTMENT movement linked to the line; line shows "Adjusted"; balance changed by the difference | |
| 20 | Adjust only once | Adjust the same line again | Refused ("already adjusted") | |
| 21 | Closing checklist | Open Closing Checklist and submit | Same behavior; system = expected balance after today's movements | |
| 22 | Stock changes while counting | Open a checklist, record a stock-in in another window, then Submit | Submit refused with "Stock changed while you were counting"; reload shows the new system quantity and keeps your typed counts | |
| 23 | Super Admin review | Global Inventory → Checklists | Submitted checklists listed with differences and adjusted count; selecting one shows its lines | |
| 24 | Database protection | In phpMyAdmin, try `UPDATE InventoryMovement SET QuantityIn = 1` | Error "append-only" | |

Known limits: the Branch Admin screens work on **today's** checklist only; one Opening and one Closing checklist exist per branch per day; transfers between branches and cylinder refill/exchange flows are not part of this module.
