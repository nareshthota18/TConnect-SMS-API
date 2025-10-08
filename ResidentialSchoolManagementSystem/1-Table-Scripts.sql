--------------------------------------------------------------------------------
-- Residential School Management System (RSMS) - Table Creation Script
--------------------------------------------------------------------------------

-- 0) Schema
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'rsms')
    EXEC('CREATE SCHEMA rsms');
GO

-- 1.1 Categories
CREATE TABLE rsms.Categories (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.2 Grades
CREATE TABLE rsms.Grades (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.3 Residential School Hostels
CREATE TABLE rsms.RSHostels (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(150) NOT NULL UNIQUE,
    Address      NVARCHAR(300) NULL,
    Phone        NVARCHAR(30) NULL,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.4 Departments
CREATE TABLE rsms.Departments (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(100) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.5 Designations
CREATE TABLE rsms.Designations (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(100) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.6 ItemTypes
CREATE TABLE rsms.ItemTypes (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.7 Suppliers
CREATE TABLE rsms.Suppliers (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(150) NOT NULL UNIQUE,
    GSTNumber    NVARCHAR(30) NULL,
    Email        NVARCHAR(150) NULL,
    Phone        NVARCHAR(30) NULL,
    Address      NVARCHAR(300) NULL,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.8 Roles
CREATE TABLE rsms.Roles (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(50) NOT NULL UNIQUE,
    Description  NVARCHAR(200) NULL,
    IsActive     BIT NOT NULL DEFAULT(1),
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.9 Permissions
CREATE TABLE rsms.Permissions (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name         NVARCHAR(100) NOT NULL UNIQUE,
    Description  NVARCHAR(200) NULL,
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL
);
GO

-- 1.10 RolePermissions
CREATE TABLE rsms.RolePermissions (
    RoleId       UNIQUEIDENTIFIER NOT NULL,
    PermissionId UNIQUEIDENTIFIER NOT NULL,
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    PRIMARY KEY (RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES rsms.Roles(Id),
    FOREIGN KEY (PermissionId) REFERENCES rsms.Permissions(Id)
);
GO

--------------------------------------------------------------------------------
-- People & Security
--------------------------------------------------------------------------------

-- 2.1 Staff
CREATE TABLE rsms.Staff (
    Id            UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    StaffCode     NVARCHAR(50) NOT NULL UNIQUE,
    FullName      NVARCHAR(150) NOT NULL,
    Email         NVARCHAR(150) NULL,
    Phone         NVARCHAR(30) NULL,
    DepartmentId  UNIQUEIDENTIFIER NULL,
    DesignationId UNIQUEIDENTIFIER NULL,
    IsTeaching    BIT NOT NULL DEFAULT(1),
    Status        NVARCHAR(20) NOT NULL DEFAULT('Active'),
    CreatedAt     DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy     UNIQUEIDENTIFIER NULL,
    UpdatedAt     DATETIME2(0) NULL,
    UpdatedBy     UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Staff_Department FOREIGN KEY (DepartmentId) REFERENCES rsms.Departments(Id),
    CONSTRAINT FK_Staff_Designation FOREIGN KEY (DesignationId) REFERENCES rsms.Designations(Id),
    CONSTRAINT CK_Staff_Status CHECK (Status IN (N'Active', N'Inactive'))
);
GO

-- 2.2 Users
CREATE TABLE rsms.Users (
    Id          UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Username    NVARCHAR(100) NOT NULL UNIQUE,
    Email       NVARCHAR(150) NULL,
    Phone       NVARCHAR(30) NULL,
    StaffId     UNIQUEIDENTIFIER NULL,
    ExternalId  NVARCHAR(100) NULL,
    IsActive    BIT NOT NULL DEFAULT(1),
    CreatedAt   DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy   UNIQUEIDENTIFIER NULL,
    UpdatedAt   DATETIME2(0) NULL,
    UpdatedBy   UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Users_Staff FOREIGN KEY (StaffId) REFERENCES rsms.Staff(Id)
);
GO

-- 2.3 UserHostels
CREATE TABLE rsms.UserHostels (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RSHostelId UNIQUEIDENTIFIER NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,      -- hostel-specific role
    IsPrimary BIT NOT NULL DEFAULT(0),
    CreatedAt DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT PK_UserHostels PRIMARY KEY (UserId, RSHostelId),
    CONSTRAINT FK_UserHostels_Users FOREIGN KEY (UserId) REFERENCES rsms.Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserHostels_RSHostels FOREIGN KEY (RSHostelId) REFERENCES rsms.RSHostels(Id) ON DELETE CASCADE,
    CONSTRAINT FK_UserHostels_Roles FOREIGN KEY (RoleId) REFERENCES rsms.Roles(Id)
);

--------------------------------------------------------------------------------
-- Students & Attendance
--------------------------------------------------------------------------------

-- 3.1 Students
CREATE TABLE rsms.Students (
    Id             UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    AdmissionNumber NVARCHAR(50) NOT NULL UNIQUE,
    FirstName       NVARCHAR(100) NOT NULL,
    LastName        NVARCHAR(100) NULL,
    DateOfBirth     DATE NOT NULL,
    CategoryId      UNIQUEIDENTIFIER NULL,
    ParentName      NVARCHAR(150) NULL,
    ParentContact   NVARCHAR(50) NULL,
    RSHostelId      UNIQUEIDENTIFIER NULL,
    GradeId         UNIQUEIDENTIFIER NULL,
    Status          NVARCHAR(20) NOT NULL DEFAULT('Active'),
    HealthInfo      NVARCHAR(MAX) NULL,
    CreatedAt       DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy       UNIQUEIDENTIFIER NULL,
    UpdatedAt       DATETIME2(0) NULL,
    UpdatedBy       UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Students_Category FOREIGN KEY (CategoryId) REFERENCES rsms.Categories(Id),
    CONSTRAINT FK_Students_Hostel FOREIGN KEY (RSHostelId) REFERENCES rsms.RSHostels(Id),
    CONSTRAINT FK_Students_Class FOREIGN KEY (GradeId) REFERENCES rsms.Grades(Id),
    CONSTRAINT CK_Students_Status CHECK (Status IN (N'Active', N'Inactive'))
);
GO

-- 3.2 StudentAttendance
CREATE TABLE rsms.StudentAttendance (
    Id            UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    StudentId     UNIQUEIDENTIFIER NOT NULL,
    AttendanceDate DATE NOT NULL,
    Session        NVARCHAR(10) NOT NULL DEFAULT(N'Morning'),
    Status         NVARCHAR(10) NOT NULL DEFAULT(N'Present'),
    Remarks        NVARCHAR(200) NULL,
    CreatedAt      DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy      UNIQUEIDENTIFIER NULL,
    UpdatedAt      DATETIME2(0) NULL,
    UpdatedBy      UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_StudentAttendance_Student FOREIGN KEY (StudentId) REFERENCES rsms.Students(Id),
    CONSTRAINT CK_StudentAttendance_Session CHECK (Session IN (N'Morning', N'Evening')),
    CONSTRAINT CK_StudentAttendance_Status CHECK (Status IN (N'Present', N'Absent', N'Leave')),
    CONSTRAINT UQ_StudentAttendance UNIQUE (StudentId, AttendanceDate, Session)
);
GO

-- 3.3 StaffAttendance
CREATE TABLE rsms.StaffAttendance (
    Id          UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    StaffId     UNIQUEIDENTIFIER NOT NULL,
    AttendanceDate DATE NOT NULL,
    Status      NVARCHAR(10) NOT NULL DEFAULT(N'Present'),
    Remarks     NVARCHAR(200) NULL,
    CreatedAt   DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy   UNIQUEIDENTIFIER NULL,
    UpdatedAt   DATETIME2(0) NULL,
    UpdatedBy   UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_StaffAttendance_Staff FOREIGN KEY (StaffId) REFERENCES rsms.Staff(Id),
    CONSTRAINT CK_StaffAttendance_Status CHECK (Status IN (N'Present', N'Absent', N'Leave')),
    CONSTRAINT UQ_StaffAttendance UNIQUE (StaffId, AttendanceDate)
);
GO

--------------------------------------------------------------------------------
-- Inventory & Assets
--------------------------------------------------------------------------------

-- 4.1 Items
CREATE TABLE rsms.Items (
    Id          UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    ItemCode    NVARCHAR(50) NOT NULL UNIQUE,
    Name        NVARCHAR(150) NOT NULL,
    ItemTypeId  UNIQUEIDENTIFIER NOT NULL,
    UOM         NVARCHAR(20) NOT NULL,
    ReorderLevel DECIMAL(18,3) NOT NULL DEFAULT(0),
    IsActive    BIT NOT NULL DEFAULT(1),
    CreatedAt   DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy   UNIQUEIDENTIFIER NULL,
    UpdatedAt   DATETIME2(0) NULL,
    UpdatedBy   UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Items_ItemType FOREIGN KEY (ItemTypeId) REFERENCES rsms.ItemTypes(Id)
);
GO

-- 4.2 PurchaseInvoices
CREATE TABLE rsms.PurchaseInvoices (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    SupplierId   UNIQUEIDENTIFIER NULL,
    InvoiceNumber NVARCHAR(50) NULL,
    InvoiceDate  DATE NULL,
    ReceivedDate DATE NOT NULL,
    Notes        NVARCHAR(300) NULL,
    CreatedAt    DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy    UNIQUEIDENTIFIER NULL,
    UpdatedAt    DATETIME2(0) NULL,
    UpdatedBy    UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_PurchaseInvoices_Supplier FOREIGN KEY (SupplierId) REFERENCES rsms.Suppliers(Id)
);
GO

-- 4.3 PurchaseItems
CREATE TABLE rsms.PurchaseItems (
    Id         UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    PurchaseId UNIQUEIDENTIFIER NOT NULL,
    ItemId     UNIQUEIDENTIFIER NOT NULL,
    Quantity   DECIMAL(18,3) NOT NULL CHECK (Quantity > 0),
    UnitPrice  DECIMAL(18,2) NOT NULL CHECK (UnitPrice >= 0),
    CreatedAt  DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy  UNIQUEIDENTIFIER NULL,
    UpdatedAt  DATETIME2(0) NULL,
    UpdatedBy  UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_PurchaseItems_Purchase FOREIGN KEY (PurchaseId) REFERENCES rsms.PurchaseInvoices(Id) ON DELETE CASCADE,
    CONSTRAINT FK_PurchaseItems_Item FOREIGN KEY (ItemId) REFERENCES rsms.Items(Id)
);
GO

-- 4.4 StockLedger
CREATE TABLE rsms.StockLedger (
    Id         UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    ItemId     UNIQUEIDENTIFIER NOT NULL,
    TranDate   DATETIME2(0) NOT NULL,
    TranType   NVARCHAR(20) NOT NULL,
    Quantity   DECIMAL(18,3) NOT NULL,
    Reference  NVARCHAR(100) NULL,
    Remarks    NVARCHAR(200) NULL,
    CreatedAt  DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy  UNIQUEIDENTIFIER NULL,
    UpdatedAt  DATETIME2(0) NULL,
    UpdatedBy  UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_StockLedger_Item FOREIGN KEY (ItemId) REFERENCES rsms.Items(Id),
    CONSTRAINT CK_StockLedger_TranType CHECK (TranType IN (N'Intake', N'Consumption', N'Issue', N'Adjustment', N'Return'))
);
GO

-- 4.5 AssetIssues
CREATE TABLE rsms.AssetIssues (
    Id        UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    StudentId UNIQUEIDENTIFIER NOT NULL,
    ItemId    UNIQUEIDENTIFIER NOT NULL,
    Quantity  DECIMAL(18,3) NOT NULL CHECK (Quantity > 0),
    IssueDate DATE NOT NULL,
    Remarks   NVARCHAR(200) NULL,
    CreatedAt DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    CreatedBy UNIQUEIDENTIFIER NULL,
    UpdatedAt DATETIME2(0) NULL,
    UpdatedBy UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_AssetIssues_Student FOREIGN KEY (StudentId) REFERENCES rsms.Students(Id),
    CONSTRAINT FK_AssetIssues_Item FOREIGN KEY (ItemId) REFERENCES rsms.Items(Id)
);
GO

--------------------------------------------------------------------------------
-- Audit Logs
--------------------------------------------------------------------------------

CREATE TABLE rsms.AuditLogs (
    Id          UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    EntityName  NVARCHAR(100) NOT NULL,
    EntityId    NVARCHAR(100) NOT NULL,
    Action      NVARCHAR(20) NOT NULL, -- Create/Update/Delete
    ChangedBy   UNIQUEIDENTIFIER NULL,
    ChangedAt   DATETIME2(0) NOT NULL DEFAULT SYSUTCDATETIME(),
    Changes     NVARCHAR(MAX) NULL,
    CONSTRAINT FK_AuditLogs_User FOREIGN KEY (ChangedBy) REFERENCES rsms.Users(Id)
);
GO
