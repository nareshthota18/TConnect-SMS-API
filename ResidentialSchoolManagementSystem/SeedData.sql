
--------------------------------------------------------------------------------
--  Seed Data
--------------------------------------------------------------------------------

--------------------------------------------------------------------------------
-- Categories
--------------------------------------------------------------------------------
MERGE rsms.Categories AS t
USING (VALUES 
    (N'BC')
) AS s(Name)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, IsActive, CreatedAt) 
    VALUES (s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Grades (example: Grade 5–10 only for BC Hostel)
--------------------------------------------------------------------------------
MERGE rsms.Grades AS t
USING (VALUES 
    (N'Grade 5'), 
    (N'Grade 6'),
    (N'Grade 7'),
    (N'Grade 8'),
    (N'Grade 9'),
    (N'Grade 10')
) AS s(Name)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, IsActive, CreatedAt) 
    VALUES (s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- RSHostel (only BC Hostel)
--------------------------------------------------------------------------------
MERGE rsms.RSHostels AS t
USING (VALUES 
    (N'BC Hostel', N'Block C, Campus', N'333-3333333')
) AS s(Name, Address, Phone)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, Address, Phone, IsActive, CreatedAt) 
    VALUES (s.Name, s.Address, s.Phone, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Departments
--------------------------------------------------------------------------------
MERGE rsms.Departments AS t
USING (VALUES 
    (N'Mathematics'),
    (N'Science'),
    (N'English'),
    (N'Social Studies'),
    (N'Administration')
) AS s(Name)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, IsActive, CreatedAt) 
    VALUES (s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Designations
--------------------------------------------------------------------------------
MERGE rsms.Designations AS t
USING (VALUES 
    (N'Principal'),
    (N'Teacher'),
    (N'Warden'),
    (N'Clerk')
) AS s(Name)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, IsActive, CreatedAt) 
    VALUES (s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- ItemTypes
--------------------------------------------------------------------------------
MERGE rsms.ItemTypes AS t
USING (VALUES 
    (N'Grocery'),
    (N'Stationery'),
    (N'Uniform')
) AS s(Name)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, IsActive, CreatedAt) 
    VALUES (s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Suppliers
--------------------------------------------------------------------------------
MERGE rsms.Suppliers AS t
USING (VALUES 
    (N'XYZ Groceries', N'GST5678', N'groceries@xyz.com', N'9876543210', N'City Center')
) AS s(Name, GSTNumber, Email, Phone, Address)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, GSTNumber, Email, Phone, Address, IsActive, CreatedAt) 
    VALUES (s.Name, s.GSTNumber, s.Email, s.Phone, s.Address, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Roles
--------------------------------------------------------------------------------
MERGE rsms.Roles AS t
USING (VALUES
    (N'SuperAdmin', N'Access to all institutions and admin functions'),
    (N'Admin', N'BC Hostel-level administration'),
    (N'Staff', N'General staff access')
) AS s(Name, Description)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, Description, IsActive, CreatedAt) 
    VALUES (s.Name, s.Description, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Permissions
--------------------------------------------------------------------------------
MERGE rsms.Permissions AS t
USING (VALUES
    (N'students.read', N'Read students'),
    (N'students.write', N'Create/update students'),
    (N'attendance.manage', N'Manage attendance'),
    (N'inventory.manage', N'Manage inventory & stock'),
    (N'assets.manage', N'Manage asset distribution'),
    (N'reports.read', N'View reports')
) AS s(Name, Description)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Name, Description, CreatedAt) 
    VALUES (s.Name, s.Description, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- RolePermissions
--------------------------------------------------------------------------------
DECLARE @SuperAdminId INT = (SELECT RoleId FROM rsms.Roles WHERE Name = N'SuperAdmin');
DECLARE @AdminId INT = (SELECT RoleId FROM rsms.Roles WHERE Name = N'Admin');

-- SuperAdmin → all permissions
INSERT INTO rsms.RolePermissions (RoleId, PermissionId, CreatedAt)
SELECT @SuperAdminId, p.PermissionId, SYSUTCDATETIME()
FROM rsms.Permissions p
WHERE NOT EXISTS (
    SELECT 1 FROM rsms.RolePermissions rp WHERE rp.RoleId = @SuperAdminId AND rp.PermissionId = p.PermissionId
);

-- Admin → limited permissions
INSERT INTO rsms.RolePermissions (RoleId, PermissionId, CreatedAt)
SELECT @AdminId, p.PermissionId, SYSUTCDATETIME()
FROM rsms.Permissions p
WHERE p.Name IN (N'students.read', N'attendance.manage', N'reports.read')
AND NOT EXISTS (
    SELECT 1 FROM rsms.RolePermissions rp WHERE rp.RoleId = @AdminId AND rp.PermissionId = p.PermissionId
);
GO

--------------------------------------------------------------------------------
-- Staff
--------------------------------------------------------------------------------
INSERT INTO rsms.Staff 
(StaffCode, FullName, Email, Phone, DepartmentId, DesignationId, IsTeaching, Status, CreatedAt)
SELECT 'STF001', 'Ramesh Kumar', 'ramesh.kumar@bcschool.com', '9000000001',
       (SELECT DepartmentId FROM rsms.Departments WHERE Name = 'Administration'),
       (SELECT DesignationId FROM rsms.Designations WHERE Name = 'Principal'),
       0, 'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Staff WHERE StaffCode = 'STF001');

INSERT INTO rsms.Staff 
(StaffCode, FullName, Email, Phone, DepartmentId, DesignationId, IsTeaching, Status, CreatedAt)
SELECT 'STF002', 'Lakshmi Devi', 'lakshmi.devi@bcschool.com', '9000000002',
       (SELECT DepartmentId FROM rsms.Departments WHERE Name = 'Administration'),
       (SELECT DesignationId FROM rsms.Designations WHERE Name = 'Warden'),
       0, 'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Staff WHERE StaffCode = 'STF002');
GO

--------------------------------------------------------------------------------
-- Users (SuperAdmin + Admin + Staff accounts)
--------------------------------------------------------------------------------
INSERT INTO rsms.Users (Username, Email, Phone, StaffId, ExternalId, IsActive, CreatedAt)
SELECT 'superadmin', 'superadmin@rsms.com', '9000000999', NULL, NULL, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Users WHERE Username = 'superadmin');

INSERT INTO rsms.Users (Username, Email, Phone, StaffId, ExternalId, IsActive, CreatedAt)
SELECT 'bcadmin', 'admin@bcschool.com', '9000000888',
       (SELECT StaffId FROM rsms.Staff WHERE StaffCode = 'STF001'),
       NULL, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Users WHERE Username = 'bcadmin');
GO

--------------------------------------------------------------------------------
-- UserRoles
--------------------------------------------------------------------------------
DECLARE @SuperAdminUserId BIGINT = (SELECT UserId FROM rsms.Users WHERE Username = 'superadmin');
DECLARE @AdminUserId BIGINT = (SELECT UserId FROM rsms.Users WHERE Username = 'bcadmin');
DECLARE @SuperAdminRoleId INT = (SELECT RoleId FROM rsms.Roles WHERE Name = 'SuperAdmin');
DECLARE @AdminRoleId INT = (SELECT RoleId FROM rsms.Roles WHERE Name = 'Admin');

INSERT INTO rsms.UserRoles (UserId, RoleId, CreatedAt)
SELECT @SuperAdminUserId, @SuperAdminRoleId, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.UserRoles WHERE UserId = @SuperAdminUserId AND RoleId = @SuperAdminRoleId);

INSERT INTO rsms.UserRoles (UserId, RoleId, CreatedAt)
SELECT @AdminUserId, @AdminRoleId, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.UserRoles WHERE UserId = @AdminUserId AND RoleId = @AdminRoleId);
GO

--------------------------------------------------------------------------------
-- Students (all BC Hostel, different grades)
--------------------------------------------------------------------------------
INSERT INTO rsms.Students 
(AdmissionNumber, FirstName, LastName, DateOfBirth, CategoryId, ParentName, ParentContact, RSHId, GradeId, Status, CreatedAt)
SELECT 'ADM001', 'Arjun', 'Rao', '2010-06-15',
       (SELECT CategoryId FROM rsms.Categories WHERE Name = 'BC'),
       'Suresh Rao', '9001111111',
       (SELECT RSHId FROM rsms.RSHostels WHERE Name = 'BC Hostel'),
       (SELECT GradeId FROM rsms.Grades WHERE Name = 'Grade 6'),
       'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Students WHERE AdmissionNumber = 'ADM001');

INSERT INTO rsms.Students 
(AdmissionNumber, FirstName, LastName, DateOfBirth, CategoryId, ParentName, ParentContact, RSHId, GradeId, Status, CreatedAt)
SELECT 'ADM002', 'Meena', 'Sharma', '2011-01-20',
       (SELECT CategoryId FROM rsms.Categories WHERE Name = 'BC'),
       'Rajesh Sharma', '9002222222',
       (SELECT RSHId FROM rsms.RSHostels WHERE Name = 'BC Hostel'),
       (SELECT GradeId FROM rsms.Grades WHERE Name = 'Grade 7'),
       'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Students WHERE AdmissionNumber = 'ADM002');
GO

--------------------------------------------------------------------------------
-- Items (basic hostel inventory)
--------------------------------------------------------------------------------
INSERT INTO rsms.Items (ItemCode, Name, ItemTypeId, UOM, ReorderLevel, IsActive, CreatedAt)
SELECT 'ITM001', 'Rice (50kg Bag)', (SELECT ItemTypeId FROM rsms.ItemTypes WHERE Name = 'Grocery'),
       'Bag', 5, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Items WHERE ItemCode = 'ITM001');

INSERT INTO rsms.Items (ItemCode, Name, ItemTypeId, UOM, ReorderLevel, IsActive, CreatedAt)
SELECT 'ITM002', 'Notebook (200 pages)', (SELECT ItemTypeId FROM rsms.ItemTypes WHERE Name = 'Stationery'),
       'Piece', 50, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Items WHERE ItemCode = 'ITM002');

INSERT INTO rsms.Items (ItemCode, Name, ItemTypeId, UOM, ReorderLevel, IsActive, CreatedAt)
SELECT 'ITM003', 'Uniform Set', (SELECT ItemTypeId FROM rsms.ItemTypes WHERE Name = 'Uniform'),
       'Set', 20, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Items WHERE ItemCode = 'ITM003');
GO

