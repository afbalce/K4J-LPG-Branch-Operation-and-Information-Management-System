-- =====================================================================
-- K4J LPG Inventory & Operational Checklist System
-- Sample data (JPang and Daet). Run AFTER 01_k4j_lpg_schema.sql, on an empty schema.
-- Demo logins (all use the password  K4J@2026):
--   superadmin@k4j.local   Super Admin
--   jpang.admin@k4j.local  Branch Admin - JPang
--   daet.admin@k4j.local   Branch Admin - Daet
-- The products and quantities below are made-up sample values.
-- Opening stock is recorded as INCOMING movements so the movement
-- history explains every balance.
-- =====================================================================
USE k4j_lpg;

INSERT INTO `User` (UserID, RoleID, BranchID, FullName, Email, PasswordHash) VALUES
  (1, 1, NULL, 'Super Admin', 'superadmin@k4j.local', 'PBKDF2-SHA256:600000:PE0auGV6JxcRBWaM6krM/Q==:SCoOVBoO98VYCaEInaNhP7IRO9Zu4NUZzS2i6Y4Z99Y='),
  (2, 2, 1, 'Maria Santos', 'jpang.admin@k4j.local', 'PBKDF2-SHA256:600000:ww84hYlcRscs0xtDNvHtBg==:Fxr6mb3/hTkW/z5gFXXW0AknXeiDRsvnrMWmoSvIQwI='),
  (3, 2, 2, 'Juan Dela Cruz', 'daet.admin@k4j.local', 'PBKDF2-SHA256:600000:1KTOQsxFMSpPSqDeu/Xa2Q==:iONgHFBAlrHfiLhn1VIad92BLl74G/stYKrqMggKMsM=');

INSERT INTO Product (ProductID, ProductCode, ProductName, Brand, Category, CylinderSizeKg) VALUES
	(1, 'GAS-11', 'Superkalan 11 kg', 'Superkalan', 'LPG_CYLINDER', 11.00),
  (2, 'GAS-22', 'Superkalan 22 kg', 'Superkalan', 'LPG_CYLINDER', 22.00),
  (3, 'GAS-50', 'Superkalan 50 kg', 'Superkalan', 'LPG_CYLINDER', 50.00),
  (4, 'SOL-11', 'Solane 11 kg', 'Solane', 'LPG_CYLINDER', 11.00),
  (5, 'SOL-22', 'Solane 22 kg', 'Solane', 'LPG_CYLINDER', 22.00),
  (6, 'ACC-REG', 'Regulator', NULL, 'ACCESSORY', NULL),
  (7, 'ACC-HOSE', 'LPG hose (1.5 m)', NULL, 'ACCESSORY', NULL),
  (8, 'ACC-STOVE', 'Single-burner stove', NULL, 'ACCESSORY', NULL);

INSERT INTO Inventory (BranchID, ProductID, StockCondition, Quantity) VALUES
  (1, 1, 'FILLED', 40),
  (1, 1, 'EMPTY', 25),
  (1, 1, 'REVOLVING', 60),
  (1, 1, 'DAMAGED', 2),
  (1, 2, 'FILLED', 15),
  (1, 2, 'EMPTY', 10),
  (1, 2, 'REVOLVING', 20),
  (1, 2, 'DAMAGED', 1),
  (1, 3, 'FILLED', 6),
  (1, 3, 'EMPTY', 4),
  (1, 3, 'REVOLVING', 5),
  (1, 3, 'DAMAGED', 0),
  (1, 4, 'FILLED', 30),
  (1, 4, 'EMPTY', 18),
  (1, 4, 'REVOLVING', 40),
  (1, 4, 'DAMAGED', 1),
  (1, 5, 'FILLED', 12),
  (1, 5, 'EMPTY', 8),
  (1, 5, 'REVOLVING', 15),
  (1, 5, 'DAMAGED', 0),
  (1, 6, 'GENERAL', 30),
  (1, 7, 'GENERAL', 45),
  (1, 8, 'GENERAL', 12),
  (2, 1, 'FILLED', 32),
  (2, 1, 'EMPTY', 20),
  (2, 1, 'REVOLVING', 45),
  (2, 1, 'DAMAGED', 1),
  (2, 2, 'FILLED', 10),
  (2, 2, 'EMPTY', 8),
  (2, 2, 'REVOLVING', 14),
  (2, 2, 'DAMAGED', 0),
  (2, 3, 'FILLED', 4),
  (2, 3, 'EMPTY', 3),
  (2, 3, 'REVOLVING', 2),
  (2, 3, 'DAMAGED', 0),
  (2, 4, 'FILLED', 25),
  (2, 4, 'EMPTY', 12),
  (2, 4, 'REVOLVING', 30),
  (2, 4, 'DAMAGED', 2),
  (2, 5, 'FILLED', 9),
  (2, 5, 'EMPTY', 6),
  (2, 5, 'REVOLVING', 10),
  (2, 5, 'DAMAGED', 1),
  (2, 6, 'GENERAL', 22),
  (2, 7, 'GENERAL', 38),
  (2, 8, 'GENERAL', 9);

INSERT INTO InventoryMovement (BranchID, ProductID, StockCondition, MovementType, QuantityIn, QuantityOut, PreviousBalance, NewBalance, MovementDate, Description, RecordedBy) VALUES
  (1, 1, 'FILLED', 'INCOMING', 30, 0, 0, 30, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 1, 'FILLED', 'INCOMING', 20, 0, 30, 50, DATE_SUB(NOW(), INTERVAL 3 DAY), 'Delivery from supplier', 2),
  (1, 1, 'FILLED', 'OUTGOING', 0, 10, 50, 40, DATE_SUB(NOW(), INTERVAL 1 DAY), 'Distributed to dealers', 2),
  (1, 1, 'EMPTY', 'INCOMING', 25, 0, 0, 25, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 1, 'REVOLVING', 'INCOMING', 60, 0, 0, 60, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 1, 'DAMAGED', 'INCOMING', 2, 0, 0, 2, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 2, 'FILLED', 'INCOMING', 15, 0, 0, 15, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 2, 'EMPTY', 'INCOMING', 10, 0, 0, 10, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 2, 'REVOLVING', 'INCOMING', 20, 0, 0, 20, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 2, 'DAMAGED', 'INCOMING', 2, 0, 0, 2, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 2, 'DAMAGED', 'ADJUSTMENT', 0, 1, 2, 1, DATE_SUB(NOW(), INTERVAL 2 DAY), 'Damaged cylinder written off after inspection', 2),
  (1, 3, 'FILLED', 'INCOMING', 6, 0, 0, 6, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 3, 'EMPTY', 'INCOMING', 4, 0, 0, 4, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 3, 'REVOLVING', 'INCOMING', 5, 0, 0, 5, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 4, 'FILLED', 'INCOMING', 24, 0, 0, 24, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 4, 'FILLED', 'INCOMING', 12, 0, 24, 36, DATE_SUB(NOW(), INTERVAL 3 DAY), 'Delivery from supplier', 2),
  (1, 4, 'FILLED', 'OUTGOING', 0, 6, 36, 30, DATE_SUB(NOW(), INTERVAL 1 DAY), 'Distributed to dealers', 2),
  (1, 4, 'EMPTY', 'INCOMING', 18, 0, 0, 18, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 4, 'REVOLVING', 'INCOMING', 40, 0, 0, 40, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 4, 'DAMAGED', 'INCOMING', 1, 0, 0, 1, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 5, 'FILLED', 'INCOMING', 12, 0, 0, 12, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 5, 'EMPTY', 'INCOMING', 8, 0, 0, 8, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 5, 'REVOLVING', 'INCOMING', 15, 0, 0, 15, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 6, 'GENERAL', 'INCOMING', 30, 0, 0, 30, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 7, 'GENERAL', 'INCOMING', 45, 0, 0, 45, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (1, 8, 'GENERAL', 'INCOMING', 12, 0, 0, 12, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 2),
  (2, 1, 'FILLED', 'INCOMING', 25, 0, 0, 25, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 1, 'FILLED', 'INCOMING', 15, 0, 25, 40, DATE_SUB(NOW(), INTERVAL 3 DAY), 'Delivery from supplier', 3),
  (2, 1, 'FILLED', 'OUTGOING', 0, 8, 40, 32, DATE_SUB(NOW(), INTERVAL 1 DAY), 'Distributed to dealers', 3),
  (2, 1, 'EMPTY', 'INCOMING', 20, 0, 0, 20, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 1, 'REVOLVING', 'INCOMING', 45, 0, 0, 45, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 1, 'DAMAGED', 'INCOMING', 1, 0, 0, 1, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 2, 'FILLED', 'INCOMING', 10, 0, 0, 10, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 2, 'EMPTY', 'INCOMING', 8, 0, 0, 8, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 2, 'REVOLVING', 'INCOMING', 14, 0, 0, 14, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 3, 'FILLED', 'INCOMING', 4, 0, 0, 4, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 3, 'EMPTY', 'INCOMING', 3, 0, 0, 3, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 3, 'REVOLVING', 'INCOMING', 2, 0, 0, 2, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 4, 'FILLED', 'INCOMING', 20, 0, 0, 20, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 4, 'FILLED', 'INCOMING', 10, 0, 20, 30, DATE_SUB(NOW(), INTERVAL 3 DAY), 'Delivery from supplier', 3),
  (2, 4, 'FILLED', 'OUTGOING', 0, 5, 30, 25, DATE_SUB(NOW(), INTERVAL 1 DAY), 'Distributed to dealers', 3),
  (2, 4, 'EMPTY', 'INCOMING', 12, 0, 0, 12, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 4, 'REVOLVING', 'INCOMING', 30, 0, 0, 30, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 4, 'DAMAGED', 'INCOMING', 2, 0, 0, 2, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 5, 'FILLED', 'INCOMING', 9, 0, 0, 9, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 5, 'EMPTY', 'INCOMING', 6, 0, 0, 6, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 5, 'REVOLVING', 'INCOMING', 10, 0, 0, 10, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 5, 'DAMAGED', 'INCOMING', 1, 0, 0, 1, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 6, 'GENERAL', 'INCOMING', 22, 0, 0, 22, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 7, 'GENERAL', 'INCOMING', 38, 0, 0, 38, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3),
  (2, 8, 'GENERAL', 'INCOMING', 9, 0, 0, 9, DATE_SUB(NOW(), INTERVAL 5 DAY), 'Opening stock (sample data)', 3);

-- Yesterday's submitted CLOSING checklist for each branch, so today's OPENING checklist
-- shows 'Previous closing' values. Each has one deliberate, not-yet-adjusted difference.
-- (Lines are added while the checklist is a draft; it is then submitted and locked.)
INSERT INTO InventoryChecklist (ChecklistID, BranchID, ChecklistType, ChecklistDate, Status, CreatedBy) VALUES
  (1, 1, 'CLOSING', DATE_SUB(CURDATE(), INTERVAL 1 DAY), 'DRAFT', 2),
  (2, 2, 'CLOSING', DATE_SUB(CURDATE(), INTERVAL 1 DAY), 'DRAFT', 3);

INSERT INTO InventoryChecklistDetail (ChecklistID, BranchID, ProductID, StockCondition, SystemQuantity, PhysicalQuantity)
SELECT CASE i.BranchID WHEN 1 THEN 1 ELSE 2 END, i.BranchID, i.ProductID, i.StockCondition, i.Quantity, i.Quantity
FROM Inventory i;

UPDATE InventoryChecklistDetail
   SET PhysicalQuantity = SystemQuantity - 1, Remarks = 'One empty cylinder not found during the count'
 WHERE ChecklistID = 1 AND ProductID = 1 AND StockCondition = 'EMPTY';

UPDATE InventoryChecklistDetail
   SET PhysicalQuantity = SystemQuantity + 1, Remarks = 'Extra filled cylinder found, not yet recorded'
 WHERE ChecklistID = 2 AND ProductID = 5 AND StockCondition = 'FILLED';

UPDATE InventoryChecklist
   SET Status = 'SUBMITTED', SubmittedBy = CreatedBy, SubmissionDate = DATE_SUB(NOW(), INTERVAL 1 DAY)
 WHERE ChecklistID IN (1, 2);
