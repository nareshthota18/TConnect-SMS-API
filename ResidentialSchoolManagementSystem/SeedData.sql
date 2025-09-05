--------------------------------------------------------------------------------
-- Categories
--------------------------------------------------------------------------------
MERGE rsms.Categories AS t
USING (VALUES 
    (N'BC')
) AS s(Name)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Id, Name, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- Grades
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
    INSERT (Id, Name, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, 1, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- RSHostels
--------------------------------------------------------------------------------
MERGE rsms.RSHostels AS t
USING (VALUES 
    (N'BC Hostel', N'Block C, Campus', N'333-3333333')
) AS s(Name, Address, Phone)
ON (t.Name = s.Name)
WHEN NOT MATCHED THEN 
    INSERT (Id, Name, Address, Phone, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, s.Address, s.Phone, 1, SYSUTCDATETIME());
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
    INSERT (Id, Name, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, 1, SYSUTCDATETIME());
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
    INSERT (Id, Name, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, 1, SYSUTCDATETIME());
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
    INSERT (Id, Name, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, 1, SYSUTCDATETIME());
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
    INSERT (Id, Name, GSTNumber, Email, Phone, Address, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, s.GSTNumber, s.Email, s.Phone, s.Address, 1, SYSUTCDATETIME());
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
    INSERT (Id, Name, Description, IsActive, CreatedAt) 
    VALUES (NEWID(), s.Name, s.Description, 1, SYSUTCDATETIME());
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
    INSERT (Id, Name, Description, CreatedAt) 
    VALUES (NEWID(), s.Name, s.Description, SYSUTCDATETIME());
GO

--------------------------------------------------------------------------------
-- RolePermissions
--------------------------------------------------------------------------------
DECLARE @SuperAdminId UNIQUEIDENTIFIER = (SELECT Id FROM rsms.Roles WHERE Name = N'SuperAdmin');
DECLARE @AdminId UNIQUEIDENTIFIER = (SELECT Id FROM rsms.Roles WHERE Name = N'Admin');

-- SuperAdmin → all permissions
INSERT INTO rsms.RolePermissions (RoleId, PermissionId, CreatedAt)
SELECT @SuperAdminId, p.Id, SYSUTCDATETIME()
FROM rsms.Permissions p
WHERE NOT EXISTS (
    SELECT 1 FROM rsms.RolePermissions rp WHERE rp.RoleId = @SuperAdminId AND rp.PermissionId = p.Id
);

-- Admin → limited permissions
INSERT INTO rsms.RolePermissions (RoleId, PermissionId, CreatedAt)
SELECT @AdminId, p.Id, SYSUTCDATETIME()
FROM rsms.Permissions p
WHERE p.Name IN (N'students.read', N'attendance.manage', N'reports.read')
AND NOT EXISTS (
    SELECT 1 FROM rsms.RolePermissions rp WHERE rp.RoleId = @AdminId AND rp.PermissionId = p.Id
);
GO

--------------------------------------------------------------------------------
-- Staff
--------------------------------------------------------------------------------
INSERT INTO rsms.Staff 
(Id, StaffCode, FullName, Email, Phone, DepartmentId, DesignationId, IsTeaching, Status, CreatedAt)
SELECT NEWID(), 'STF001', 'Ramesh Kumar', 'ramesh.kumar@bcschool.com', '9000000001',
       (SELECT Id FROM rsms.Departments WHERE Name = 'Administration'),
       (SELECT Id FROM rsms.Designations WHERE Name = 'Principal'),
       0, 'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Staff WHERE StaffCode = 'STF001');

INSERT INTO rsms.Staff 
(Id, StaffCode, FullName, Email, Phone, DepartmentId, DesignationId, IsTeaching, Status, CreatedAt)
SELECT NEWID(), 'STF002', 'Lakshmi Devi', 'lakshmi.devi@bcschool.com', '9000000002',
       (SELECT Id FROM rsms.Departments WHERE Name = 'Administration'),
       (SELECT Id FROM rsms.Designations WHERE Name = 'Warden'),
       0, 'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Staff WHERE StaffCode = 'STF002');
GO

--------------------------------------------------------------------------------
-- Users
--------------------------------------------------------------------------------
INSERT INTO rsms.Users (Id, Username, Email, Phone, StaffId, ExternalId, IsActive, CreatedAt)
SELECT NEWID(), 'superadmin', 'superadmin@rsms.com', '9000000999', NULL, NULL, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Users WHERE Username = 'superadmin');

INSERT INTO rsms.Users (Id, Username, Email, Phone, StaffId, ExternalId, IsActive, CreatedAt)
SELECT NEWID(), 'bcadmin', 'admin@bcschool.com', '9000000888',
       (SELECT Id FROM rsms.Staff WHERE StaffCode = 'STF001'),
       NULL, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Users WHERE Username = 'bcadmin');
GO

--------------------------------------------------------------------------------
-- UserRoles
--------------------------------------------------------------------------------
DECLARE @SuperAdminUserId UNIQUEIDENTIFIER = (SELECT Id FROM rsms.Users WHERE Username = 'superadmin');
DECLARE @AdminUserId UNIQUEIDENTIFIER = (SELECT Id FROM rsms.Users WHERE Username = 'bcadmin');
DECLARE @SuperAdminRoleId UNIQUEIDENTIFIER = (SELECT Id FROM rsms.Roles WHERE Name = 'SuperAdmin');
DECLARE @AdminRoleId UNIQUEIDENTIFIER = (SELECT Id FROM rsms.Roles WHERE Name = 'Admin');

INSERT INTO rsms.UserRoles (UserId, RoleId, CreatedAt)
SELECT @SuperAdminUserId, @SuperAdminRoleId, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.UserRoles WHERE UserId = @SuperAdminUserId AND RoleId = @SuperAdminRoleId);

INSERT INTO rsms.UserRoles (UserId, RoleId, CreatedAt)
SELECT @AdminUserId, @AdminRoleId, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.UserRoles WHERE UserId = @AdminUserId AND RoleId = @AdminRoleId);
GO

--------------------------------------------------------------------------------
-- Students
--------------------------------------------------------------------------------
INSERT INTO rsms.Students 
(Id, AdmissionNumber, FirstName, LastName, DateOfBirth, CategoryId, ParentName, ParentContact, RSHostelId, GradeId, Status, CreatedAt)
SELECT NEWID(), 'ADM001', 'Arjun', 'Rao', '2010-06-15',
       (SELECT Id FROM rsms.Categories WHERE Name = 'BC'),
       'Suresh Rao', '9001111111',
       (SELECT Id FROM rsms.RSHostels WHERE Name = 'BC Hostel'),
       (SELECT Id FROM rsms.Grades WHERE Name = 'Grade 6'),
       'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Students WHERE AdmissionNumber = 'ADM001');

INSERT INTO rsms.Students 
(Id, AdmissionNumber, FirstName, LastName, DateOfBirth, CategoryId, ParentName, ParentContact, RSHostelId, GradeId, Status, CreatedAt)
SELECT NEWID(), 'ADM002', 'Meena', 'Sharma', '2011-01-20',
       (SELECT Id FROM rsms.Categories WHERE Name = 'BC'),
       'Rajesh Sharma', '9002222222',
       (SELECT Id FROM rsms.RSHostels WHERE Name = 'BC Hostel'),
       (SELECT Id FROM rsms.Grades WHERE Name = 'Grade 7'),
       'Active', SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Students WHERE AdmissionNumber = 'ADM002');
GO

--------------------------------------------------------------------------------
-- Items
--------------------------------------------------------------------------------
INSERT INTO rsms.Items (Id, ItemCode, Name, ItemTypeId, UOM, ReorderLevel, IsActive, CreatedAt)
SELECT NEWID(), 'ITM001', 'Rice (50kg Bag)', (SELECT Id FROM rsms.ItemTypes WHERE Name = 'Grocery'),
       'Bag', 5, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Items WHERE ItemCode = 'ITM001');

INSERT INTO rsms.Items (Id, ItemCode, Name, ItemTypeId, UOM, ReorderLevel, IsActive, CreatedAt)
SELECT NEWID(), 'ITM002', 'Notebook (200 pages)', (SELECT Id FROM rsms.ItemTypes WHERE Name = 'Stationery'),
       'Piece', 50, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Items WHERE ItemCode = 'ITM002');

INSERT INTO rsms.Items (Id, ItemCode, Name, ItemTypeId, UOM, ReorderLevel, IsActive, CreatedAt)
SELECT NEWID(), 'ITM003', 'Uniform Set', (SELECT Id FROM rsms.ItemTypes WHERE Name = 'Uniform'),
       'Set', 20, 1, SYSUTCDATETIME()
WHERE NOT EXISTS (SELECT 1 FROM rsms.Items WHERE ItemCode = 'ITM003');
GO
