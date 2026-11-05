-- =====================================================================
-- K4J LPG Inventory & Operational Checklist System
-- Step 2: Database creation script
-- Target : XAMPP (MariaDB 10.4+) or MySQL 8.0.16+  (CHECK constraints
--          are enforced on these versions)
-- Run in : phpMyAdmin > SQL tab, or  mysql -u root < 01_k4j_lpg_schema.sql
-- WARNING: re-running this script DROPS and recreates all tables
--          (development convenience). Do not run on real data.
-- =====================================================================

CREATE DATABASE IF NOT EXISTS k4j_lpg
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;
USE k4j_lpg;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS InventoryMovement;
DROP TABLE IF EXISTS InventoryChecklistDetail;
DROP TABLE IF EXISTS InventoryChecklist;
DROP TABLE IF EXISTS Inventory;
DROP TABLE IF EXISTS Product;
DROP TABLE IF EXISTS `User`;
DROP TABLE IF EXISTS Branch;
DROP TABLE IF EXISTS `Role`;
SET FOREIGN_KEY_CHECKS = 1;

-- ---------------------------------------------------------------------
-- 1. Role
-- ---------------------------------------------------------------------
CREATE TABLE `Role` (
    RoleID      INT UNSIGNED NOT NULL AUTO_INCREMENT,
    RoleName    VARCHAR(30)  NOT NULL,
    Description VARCHAR(150) NULL,
    PRIMARY KEY (RoleID),
    UNIQUE KEY uq_role_name (RoleName)
) ENGINE=InnoDB;

-- Fixed IDs: the service layer and the User CHECK constraint rely on them.
INSERT INTO `Role` (RoleID, RoleName, Description) VALUES
    (1, 'SuperAdmin',  'Views inventory and checklists of all branches'),
    (2, 'BranchAdmin', 'Manages inventory and checklists of one assigned branch');

-- ---------------------------------------------------------------------
-- 2. Branch
-- ---------------------------------------------------------------------
CREATE TABLE Branch (
    BranchID   INT UNSIGNED NOT NULL AUTO_INCREMENT,
    BranchName VARCHAR(50)  NOT NULL,
    Address    VARCHAR(200) NULL,
    IsActive   TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (BranchID),
    UNIQUE KEY uq_branch_name (BranchName)
) ENGINE=InnoDB;

INSERT INTO Branch (BranchID, BranchName) VALUES
    (1, 'JPang'),
    (2, 'Daet');

-- ---------------------------------------------------------------------
-- 3. User
--    SuperAdmin -> BranchID IS NULL      (sees every branch)
--    BranchAdmin -> BranchID IS NOT NULL (sees only that branch)
-- ---------------------------------------------------------------------
CREATE TABLE `User` (
    UserID       INT UNSIGNED NOT NULL AUTO_INCREMENT,
    RoleID       INT UNSIGNED NOT NULL,
    BranchID     INT UNSIGNED NULL,
    FullName     VARCHAR(100) NOT NULL,
    Email        VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,   -- PBKDF2 string from PasswordSecurity.vb
    IsActive     TINYINT(1)   NOT NULL DEFAULT 1,
    CreatedAt    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    LastLoginAt  DATETIME     NULL,
    PRIMARY KEY (UserID),
    UNIQUE KEY uq_user_email (Email),
    KEY ix_user_role   (RoleID),
    KEY ix_user_branch (BranchID),
    CONSTRAINT fk_user_role   FOREIGN KEY (RoleID)   REFERENCES `Role` (RoleID),
    CONSTRAINT fk_user_branch FOREIGN KEY (BranchID) REFERENCES Branch (BranchID),
    CONSTRAINT ck_user_role_branch CHECK (
        (RoleID = 1 AND BranchID IS NULL) OR
        (RoleID = 2 AND BranchID IS NOT NULL)
    )
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- 4. Product  (catalog; shared by both branches)
-- ---------------------------------------------------------------------
CREATE TABLE Product (
    ProductID      INT UNSIGNED NOT NULL AUTO_INCREMENT,
    ProductCode    VARCHAR(30)  NOT NULL,
    ProductName    VARCHAR(100) NOT NULL,
    Brand          VARCHAR(50)  NULL,
    Category       ENUM('LPG_CYLINDER','ACCESSORY') NOT NULL,
    CylinderSizeKg DECIMAL(5,2) NULL,
    IsActive       TINYINT(1)   NOT NULL DEFAULT 1,
    PRIMARY KEY (ProductID),
    UNIQUE KEY uq_product_code (ProductCode),
    KEY ix_product_category (Category),
    CONSTRAINT ck_product_size CHECK (
        (Category = 'LPG_CYLINDER' AND CylinderSizeKg IS NOT NULL AND CylinderSizeKg > 0) OR
        (Category = 'ACCESSORY'    AND CylinderSizeKg IS NULL)
    )
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- 5. Inventory  (one row per Branch + Product + StockCondition)
--    Cylinders use FILLED / EMPTY / REVOLVING / DAMAGED.
--    Accessories use GENERAL. (Row creation is enforced by the service layer.)
--    REVOLVING = cylinders currently out in the field.
-- ---------------------------------------------------------------------
CREATE TABLE Inventory (
    InventoryID    INT UNSIGNED NOT NULL AUTO_INCREMENT,
    BranchID       INT UNSIGNED NOT NULL,
    ProductID      INT UNSIGNED NOT NULL,
    StockCondition ENUM('FILLED','EMPTY','REVOLVING','DAMAGED','GENERAL') NOT NULL,
    Quantity       INT          NOT NULL DEFAULT 0,
    LastUpdatedAt  DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP
                                ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (InventoryID),
    UNIQUE KEY uq_inventory_item (BranchID, ProductID, StockCondition),
    KEY ix_inventory_product (ProductID),
    CONSTRAINT fk_inventory_branch  FOREIGN KEY (BranchID)  REFERENCES Branch (BranchID),
    CONSTRAINT fk_inventory_product FOREIGN KEY (ProductID) REFERENCES Product (ProductID),
    CONSTRAINT ck_inventory_qty CHECK (Quantity >= 0)
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- 6. InventoryChecklist  (header: one Opening + one Closing per branch per day)
-- ---------------------------------------------------------------------
CREATE TABLE InventoryChecklist (
    ChecklistID    INT UNSIGNED NOT NULL AUTO_INCREMENT,
    BranchID       INT UNSIGNED NOT NULL,
    ChecklistType  ENUM('OPENING','CLOSING') NOT NULL,
    ChecklistDate  DATE         NOT NULL,
    Status         ENUM('DRAFT','SUBMITTED') NOT NULL DEFAULT 'DRAFT',
    CreatedBy      INT UNSIGNED NOT NULL,
    CreatedAt      DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    SubmittedBy    INT UNSIGNED NULL,
    SubmissionDate DATETIME     NULL,
    PRIMARY KEY (ChecklistID),
    UNIQUE KEY uq_checklist_per_day (BranchID, ChecklistType, ChecklistDate),
    UNIQUE KEY uq_checklist_branch  (ChecklistID, BranchID),   -- target of detail FK
    KEY ix_checklist_date (ChecklistDate),
    KEY ix_checklist_created_by   (CreatedBy),
    KEY ix_checklist_submitted_by (SubmittedBy),
    CONSTRAINT fk_checklist_branch    FOREIGN KEY (BranchID)    REFERENCES Branch (BranchID),
    CONSTRAINT fk_checklist_created   FOREIGN KEY (CreatedBy)   REFERENCES `User` (UserID),
    CONSTRAINT fk_checklist_submitted FOREIGN KEY (SubmittedBy) REFERENCES `User` (UserID),
    CONSTRAINT ck_checklist_submit CHECK (
        (Status = 'DRAFT'     AND SubmittedBy IS NULL     AND SubmissionDate IS NULL) OR
        (Status = 'SUBMITTED' AND SubmittedBy IS NOT NULL AND SubmissionDate IS NOT NULL)
    )
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- 7. InventoryChecklistDetail
--    Difference = PhysicalQuantity - SystemQuantity  (computed by the database)
--      negative = missing stock, positive = excess stock
--    PhysicalQuantity is NULL until the admin enters a count (draft).
--    PreviousClosingQuantity is shown on Opening checklists (reference only).
--    BranchID is repeated here so that composite FKs guarantee the detail row
--    belongs to the same branch as both its checklist and its inventory item.
-- ---------------------------------------------------------------------
CREATE TABLE InventoryChecklistDetail (
    DetailID                INT UNSIGNED NOT NULL AUTO_INCREMENT,
    ChecklistID             INT UNSIGNED NOT NULL,
    BranchID                INT UNSIGNED NOT NULL,
    ProductID               INT UNSIGNED NOT NULL,
    StockCondition          ENUM('FILLED','EMPTY','REVOLVING','DAMAGED','GENERAL') NOT NULL,
    PreviousClosingQuantity INT NULL,
    SystemQuantity          INT NOT NULL,
    PhysicalQuantity        INT NULL,
    Difference              INT GENERATED ALWAYS AS (PhysicalQuantity - SystemQuantity) STORED,
    Remarks                 VARCHAR(255) NULL,
    PRIMARY KEY (DetailID),
    UNIQUE KEY uq_detail_item (ChecklistID, ProductID, StockCondition),
    KEY ix_detail_inventory (BranchID, ProductID, StockCondition),
    CONSTRAINT fk_detail_checklist FOREIGN KEY (ChecklistID, BranchID)
        REFERENCES InventoryChecklist (ChecklistID, BranchID) ON DELETE CASCADE,
    CONSTRAINT fk_detail_inventory FOREIGN KEY (BranchID, ProductID, StockCondition)
        REFERENCES Inventory (BranchID, ProductID, StockCondition),
    CONSTRAINT ck_detail_qty CHECK (
        SystemQuantity >= 0 AND (PhysicalQuantity IS NULL OR PhysicalQuantity >= 0)
    )
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- 8. InventoryMovement  (append-only ledger of every stock change)
--    NewBalance = PreviousBalance + QuantityIn - QuantityOut
--    INCOMING  -> QuantityIn  > 0, QuantityOut = 0
--    OUTGOING  -> QuantityOut > 0, QuantityIn  = 0
--    ADJUSTMENT-> either one > 0, reason mandatory; optionally linked to the
--                 checklist line (ChecklistDetailID) that revealed the discrepancy
-- ---------------------------------------------------------------------
CREATE TABLE InventoryMovement (
    MovementID        INT UNSIGNED NOT NULL AUTO_INCREMENT,
    BranchID          INT UNSIGNED NOT NULL,
    ProductID         INT UNSIGNED NOT NULL,
    StockCondition    ENUM('FILLED','EMPTY','REVOLVING','DAMAGED','GENERAL') NOT NULL,
    MovementType      ENUM('INCOMING','OUTGOING','ADJUSTMENT') NOT NULL,
    QuantityIn        INT          NOT NULL DEFAULT 0,
    QuantityOut       INT          NOT NULL DEFAULT 0,
    PreviousBalance   INT          NOT NULL,
    NewBalance        INT          NOT NULL,
    MovementDate      DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Description       VARCHAR(255) NOT NULL,
    RecordedBy        INT UNSIGNED NOT NULL,
    ChecklistDetailID INT UNSIGNED NULL,
    PRIMARY KEY (MovementID),
    KEY ix_movement_branch_date (BranchID, MovementDate),
    KEY ix_movement_item        (BranchID, ProductID, StockCondition),
    KEY ix_movement_recorded_by (RecordedBy),
    KEY ix_movement_detail      (ChecklistDetailID),
    CONSTRAINT fk_movement_inventory FOREIGN KEY (BranchID, ProductID, StockCondition)
        REFERENCES Inventory (BranchID, ProductID, StockCondition),
    CONSTRAINT fk_movement_user   FOREIGN KEY (RecordedBy)        REFERENCES `User` (UserID),
    CONSTRAINT fk_movement_detail FOREIGN KEY (ChecklistDetailID) REFERENCES InventoryChecklistDetail (DetailID),
    CONSTRAINT ck_movement_qty CHECK (QuantityIn >= 0 AND QuantityOut >= 0 AND (QuantityIn + QuantityOut) > 0),
    CONSTRAINT ck_movement_type CHECK (
        (MovementType = 'INCOMING'   AND QuantityIn  > 0 AND QuantityOut = 0) OR
        (MovementType = 'OUTGOING'   AND QuantityOut > 0 AND QuantityIn  = 0) OR
        (MovementType = 'ADJUSTMENT' AND (QuantityIn = 0 OR QuantityOut = 0))
    ),
    CONSTRAINT ck_movement_balance CHECK (
        PreviousBalance >= 0 AND NewBalance >= 0 AND
        NewBalance = PreviousBalance + QuantityIn - QuantityOut
    ),
    CONSTRAINT ck_movement_reason CHECK (CHAR_LENGTH(TRIM(Description)) > 0)
) ENGINE=InnoDB;

-- ---------------------------------------------------------------------
-- Integrity triggers
-- ---------------------------------------------------------------------
DELIMITER $$

-- The movement ledger is append-only: no edits, no deletes.
CREATE TRIGGER trg_movement_no_update BEFORE UPDATE ON InventoryMovement
FOR EACH ROW
BEGIN
    SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'InventoryMovement is append-only: updates are not allowed.';
END$$

CREATE TRIGGER trg_movement_no_delete BEFORE DELETE ON InventoryMovement
FOR EACH ROW
BEGIN
    SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'InventoryMovement is append-only: deletes are not allowed.';
END$$

-- A submitted checklist is locked.
CREATE TRIGGER trg_checklist_lock_update BEFORE UPDATE ON InventoryChecklist
FOR EACH ROW
BEGIN
    IF OLD.Status = 'SUBMITTED' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'A submitted checklist cannot be modified.';
    END IF;
END$$

CREATE TRIGGER trg_checklist_lock_delete BEFORE DELETE ON InventoryChecklist
FOR EACH ROW
BEGIN
    IF OLD.Status = 'SUBMITTED' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'A submitted checklist cannot be deleted.';
    END IF;
END$$

CREATE TRIGGER trg_detail_lock_insert BEFORE INSERT ON InventoryChecklistDetail
FOR EACH ROW
BEGIN
    IF (SELECT Status FROM InventoryChecklist WHERE ChecklistID = NEW.ChecklistID) = 'SUBMITTED' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Cannot add lines to a submitted checklist.';
    END IF;
END$$

CREATE TRIGGER trg_detail_lock_update BEFORE UPDATE ON InventoryChecklistDetail
FOR EACH ROW
BEGIN
    IF (SELECT Status FROM InventoryChecklist WHERE ChecklistID = OLD.ChecklistID) = 'SUBMITTED' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Cannot edit lines of a submitted checklist.';
    END IF;
END$$

CREATE TRIGGER trg_detail_lock_delete BEFORE DELETE ON InventoryChecklistDetail
FOR EACH ROW
BEGIN
    IF (SELECT Status FROM InventoryChecklist WHERE ChecklistID = OLD.ChecklistID) = 'SUBMITTED' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Cannot delete lines of a submitted checklist.';
    END IF;
END$$

DELIMITER ;

-- End of Step 2. Sample data (users, products, stock) comes in Step 8.
