--------------------------------------------------------------------------------
-- Residential School Management System (RSMS)
-- SQL Server DDL: Schema + Lookup Tables
--------------------------------------------------------------------------------

-- 0) Schema
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'rsms')
    EXEC('CREATE SCHEMA rsms');
GO

-- 1.1 Categories
CREATE TABLE rsms.Categories (
    CategoryId   INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.2 Grades
CREATE TABLE rsms.Grades (
    GradeId      INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.3 Residential School Hostels
CREATE TABLE rsms.RSHostels (
    RSHId     INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(150) NOT NULL UNIQUE,
    Address      NVARCHAR(300) NULL,
    Phone        NVARCHAR(30) NULL,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.4 Departments
CREATE TABLE rsms.Departments (
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(100) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.5 Designations
CREATE TABLE rsms.Designations (
    DesignationId INT IDENTITY(1,1) PRIMARY KEY,
    Name          NVARCHAR(100) NOT NULL UNIQUE,
    IsActive      BIT NOT NULL DEFAULT(1),
    CreatedAt     DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy     BIGINT NULL,
    UpdatedAt     DATETIME2(0) NULL,
    UpdatedBy     BIGINT NULL,
    RowVersion    ROWVERSION
);
GO

-- 1.6 ItemTypes
CREATE TABLE rsms.ItemTypes (
    ItemTypeId   INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.7 Suppliers
CREATE TABLE rsms.Suppliers (
    SupplierId   BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(150) NOT NULL UNIQUE,
    GSTNumber    NVARCHAR(30) NULL,
    Email        NVARCHAR(150) NULL,
    Phone        NVARCHAR(30) NULL,
    Address      NVARCHAR(300) NULL,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.8 Roles
CREATE TABLE rsms.Roles (
    RoleId       INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    Description  NVARCHAR(200) NULL,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.9 Permissions
CREATE TABLE rsms.Permissions (
    PermissionId INT IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(100) NOT NULL UNIQUE,
    Description  NVARCHAR(200) NULL,
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION
);
GO

-- 1.10 RolePermissions
CREATE TABLE rsms.RolePermissions (
    RoleId       INT NOT NULL,
    PermissionId INT NOT NULL,
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    PRIMARY KEY (RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES rsms.Roles(RoleId),
    FOREIGN KEY (PermissionId) REFERENCES rsms.Permissions(PermissionId)
);
GO

--------------------------------------------------------------------------------
--  People & Security
--------------------------------------------------------------------------------

-- 2.1 Staff
CREATE TABLE rsms.Staff (
    StaffId       BIGINT IDENTITY(1,1) PRIMARY KEY,
    StaffCode     NVARCHAR(50) NOT NULL UNIQUE,
    FullName      NVARCHAR(150) NOT NULL,
    Email         NVARCHAR(150) NULL,
    Phone         NVARCHAR(30) NULL,
    DepartmentId  INT NULL,
    DesignationId INT NULL,
    IsTeaching    BIT NOT NULL DEFAULT(1),
    Status        NVARCHAR(20) NOT NULL DEFAULT('Active'),
    CreatedAt     DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy     BIGINT NULL,
    UpdatedAt     DATETIME2(0) NULL,
    UpdatedBy     BIGINT NULL,
    RowVersion    ROWVERSION,
    CONSTRAINT FK_Staff_Department FOREIGN KEY (DepartmentId) REFERENCES rsms.Departments(DepartmentId),
    CONSTRAINT FK_Staff_Designation FOREIGN KEY (DesignationId) REFERENCES rsms.Designations(DesignationId),
    CONSTRAINT CK_Staff_Status CHECK (Status IN (N'Active', N'Inactive'))
);
GO

-- 2.2 Users (links optionally to Staff)
CREATE TABLE rsms.Users (
    UserId     BIGINT IDENTITY(1,1) PRIMARY KEY,
    Username   NVARCHAR(100) NOT NULL UNIQUE,
    Email      NVARCHAR(150) NULL,
    Phone      NVARCHAR(30) NULL,
    StaffId    BIGINT NULL,
    ExternalId NVARCHAR(100) NULL, -- for Okta/Ping integration
    IsActive   BIT NOT NULL DEFAULT(1),
    CreatedAt  DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy  BIGINT NULL,
    UpdatedAt  DATETIME2(0) NULL,
    UpdatedBy  BIGINT NULL,
    RowVersion ROWVERSION,
    CONSTRAINT FK_Users_Staff FOREIGN KEY (StaffId) REFERENCES rsms.Staff(StaffId)
);
GO

-- 2.3 UserRoles
CREATE TABLE rsms.UserRoles (
    UserId    BIGINT NOT NULL,
    RoleId    INT NOT NULL,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy BIGINT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES rsms.Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES rsms.Roles(RoleId)
);
GO

--------------------------------------------------------------------------------
-- Students & Attendance
--------------------------------------------------------------------------------

-- 3.1 Students
CREATE TABLE rsms.Students (
    StudentId       BIGINT IDENTITY(1,1) PRIMARY KEY,
    AdmissionNumber NVARCHAR(50) NOT NULL UNIQUE,
    FirstName       NVARCHAR(100) NOT NULL,
    LastName        NVARCHAR(100) NULL,
    DateOfBirth     DATE NOT NULL,
    CategoryId      INT NULL,
    ParentName      NVARCHAR(150) NULL,
    ParentContact   NVARCHAR(50) NULL,
    RSHId        INT NULL,
    GradeId         INT NULL,
    Status          NVARCHAR(20) NOT NULL DEFAULT('Active'),
    HealthInfo      NVARCHAR(MAX) NULL,
    CreatedAt       DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy       BIGINT NULL,
    UpdatedAt       DATETIME2(0) NULL,
    UpdatedBy       BIGINT NULL,
    RowVersion      ROWVERSION,
    CONSTRAINT FK_Students_Category FOREIGN KEY (CategoryId) REFERENCES rsms.Categories(CategoryId),
    CONSTRAINT FK_Students_Hostel FOREIGN KEY (RSHId) REFERENCES rsms.RSHostels(RSHId),
    CONSTRAINT FK_Students_Class FOREIGN KEY (GradeId) REFERENCES rsms.Grades(GradeId),
    CONSTRAINT CK_Students_Status CHECK (Status IN (N'Active', N'Inactive'))
);
GO

-- 3.2 StudentAttendance
CREATE TABLE rsms.StudentAttendance (
    AttendanceId   BIGINT IDENTITY(1,1) PRIMARY KEY,
    StudentId      BIGINT NOT NULL,
    AttendanceDate DATE NOT NULL,
    Session        NVARCHAR(10) NOT NULL DEFAULT(N'Morning'),
    Status         NVARCHAR(10) NOT NULL DEFAULT(N'Present'),
    Remarks        NVARCHAR(200) NULL,
    CreatedAt      DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy      BIGINT NULL,
    UpdatedAt      DATETIME2(0) NULL,
    UpdatedBy      BIGINT NULL,
    RowVersion     ROWVERSION,
    CONSTRAINT FK_StuAtt_Student FOREIGN KEY (StudentId) REFERENCES rsms.Students(StudentId),
    CONSTRAINT CK_StuAtt_Session CHECK (Session IN (N'Morning', N'Evening')),
    CONSTRAINT CK_StuAtt_Status CHECK (Status IN (N'Present', N'Absent', N'Leave')),
    CONSTRAINT UQ_StuAtt UNIQUE (StudentId, AttendanceDate, Session)
);
GO

-- 3.3 StaffAttendance
CREATE TABLE rsms.StaffAttendance (
    StaffAttendanceId BIGINT IDENTITY(1,1) PRIMARY KEY,
    StaffId        BIGINT NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status         NVARCHAR(10) NOT NULL DEFAULT(N'Present'),
    Remarks        NVARCHAR(200) NULL,
    CreatedAt      DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy      BIGINT NULL,
    UpdatedAt      DATETIME2(0) NULL,
    UpdatedBy      BIGINT NULL,
    RowVersion     ROWVERSION,
    CONSTRAINT FK_StfAtt_Staff FOREIGN KEY (StaffId) REFERENCES rsms.Staff(StaffId),
    CONSTRAINT CK_StfAtt_Status CHECK (Status IN (N'Present', N'Absent', N'Leave')),
    CONSTRAINT UQ_StfAtt UNIQUE (StaffId, AttendanceDate)
);
GO

--------------------------------------------------------------------------------
-- Inventory & Assets
--------------------------------------------------------------------------------

-- 4.1 Items
CREATE TABLE rsms.Items (
    ItemId       BIGINT IDENTITY(1,1) PRIMARY KEY,
    ItemCode     NVARCHAR(50) NOT NULL UNIQUE,
    Name         NVARCHAR(150) NOT NULL,
    ItemTypeId   INT NOT NULL,
    UOM          NVARCHAR(20) NOT NULL,
    ReorderLevel DECIMAL(18,3) NOT NULL DEFAULT(0),
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    BIGINT NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    BIGINT NULL,
    RowVersion   ROWVERSION,
    CONSTRAINT FK_Items_ItemType FOREIGN KEY (ItemTypeId) REFERENCES rsms.ItemTypes(ItemTypeId)
);
GO

-- 4.2 PurchaseInvoices
CREATE TABLE rsms.PurchaseInvoices (
    PurchaseId    BIGINT IDENTITY(1,1) PRIMARY KEY,
    SupplierId    BIGINT NULL,
    InvoiceNumber NVARCHAR(50) NULL,
    InvoiceDate   DATE NULL,
    ReceivedDate  DATE NOT NULL,
    Notes         NVARCHAR(300) NULL,
    CreatedAt     DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy     BIGINT NULL,
    UpdatedAt     DATETIME2(0) NULL,
    UpdatedBy     BIGINT NULL,
    RowVersion    ROWVERSION,
    CONSTRAINT FK_PurHdr_Supplier FOREIGN KEY (SupplierId) REFERENCES rsms.Suppliers(SupplierId)
);
GO

-- 4.3 PurchaseItems
CREATE TABLE rsms.PurchaseItems (
    PurchaseItemId BIGINT IDENTITY(1,1) PRIMARY KEY,
    PurchaseId     BIGINT NOT NULL,
    ItemId         BIGINT NOT NULL,
    Quantity       DECIMAL(18,3) NOT NULL CHECK (Quantity > 0),
    UnitPrice      DECIMAL(18,2) NOT NULL CHECK (UnitPrice >= 0),
    CreatedAt      DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy      BIGINT NULL,
    UpdatedAt      DATETIME2(0) NULL,
    UpdatedBy      BIGINT NULL,
    RowVersion     ROWVERSION,
    CONSTRAINT FK_PurItem_Purchase FOREIGN KEY (PurchaseId) REFERENCES rsms.PurchaseInvoices(PurchaseId) ON DELETE CASCADE,
    CONSTRAINT FK_PurItem_Item FOREIGN KEY (ItemId) REFERENCES rsms.Items(ItemId)
);
GO

-- 4.4 StockLedger
CREATE TABLE rsms.StockLedger (
    LedgerId   BIGINT IDENTITY(1,1) PRIMARY KEY,
    ItemId     BIGINT NOT NULL,
    TranDate   DATETIME2(0) NOT NULL,
    TranType   NVARCHAR(20) NOT NULL, -- Intake, Consumption, Issue, Adjustment, Return
    Quantity   DECIMAL(18,3) NOT NULL,
    Reference  NVARCHAR(100) NULL,
    Remarks    NVARCHAR(200) NULL,
    CreatedAt  DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy  BIGINT NULL,
    UpdatedAt  DATETIME2(0) NULL,
    UpdatedBy  BIGINT NULL,
    RowVersion ROWVERSION,
    CONSTRAINT FK_StkLed_Item FOREIGN KEY (ItemId) REFERENCES rsms.Items(ItemId),
    CONSTRAINT CK_StkLed_TranType CHECK (TranType IN (N'Intake', N'Consumption', N'Issue', N'Adjustment', N'Return'))
);
GO

-- 4.5 AssetIssues
CREATE TABLE rsms.AssetIssues (
    IssueId   BIGINT IDENTITY(1,1) PRIMARY KEY,
    StudentId BIGINT NOT NULL,
    ItemId    BIGINT NOT NULL,
    Quantity  DECIMAL(18,3) NOT NULL CHECK (Quantity > 0),
    IssueDate DATE NOT NULL,
    Remarks   NVARCHAR(200) NULL,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy BIGINT NULL,
    UpdatedAt DATETIME2(0) NULL,
    UpdatedBy BIGINT NULL,
    RowVersion ROWVERSION,
    CONSTRAINT FK_AssetIssues_Student FOREIGN KEY (StudentId) REFERENCES rsms.Students(StudentId),
    CONSTRAINT FK_AssetIssues_Item FOREIGN KEY (ItemId) REFERENCES rsms.Items(ItemId)
);
GO

--------------------------------------------------------------------------------
-- Audit Logs
--------------------------------------------------------------------------------

CREATE TABLE rsms.AuditLogs (
    AuditId    BIGINT IDENTITY(1,1) PRIMARY KEY,
    EntityName NVARCHAR(100) NOT NULL,
    EntityId   NVARCHAR(100) NOT NULL,
    Action     NVARCHAR(20) NOT NULL, -- Create/Update/Delete
    ChangedBy  BIGINT NULL,
    ChangedAt  DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    Changes    NVARCHAR(MAX) NULL,
    RowVersion ROWVERSION,
    CONSTRAINT FK_AuditLogs_User FOREIGN KEY (ChangedBy) REFERENCES rsms.Users(UserId)
);
GO
