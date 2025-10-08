USE TConnect_CommonDB; 
GO

--------------------------------------------------------------------------------
-- 1. Declare your existing HostelId (School)
--------------------------------------------------------------------------------
DECLARE @HostelId UNIQUEIDENTIFIER = 'D1F90D34-37FB-4205-AD03-4F5A82E61DC8';
PRINT 'Using RSHostelId: ' + CAST(@HostelId AS NVARCHAR(50));
GO

--------------------------------------------------------------------------------
-- 2. Add RSHostelId columns (nullable first)
--------------------------------------------------------------------------------
DECLARE @sql NVARCHAR(MAX);

-- Tables that should reference RSHostels
DECLARE @tables TABLE (TableName NVARCHAR(100));
INSERT INTO @tables VALUES
(N'Staff'),
(N'Users'),
(N'Suppliers'),
(N'PurchaseInvoices'),
(N'StockLedger'),
(N'AssetIssues');

DECLARE @tbl NVARCHAR(100);
DECLARE cur CURSOR FOR SELECT TableName FROM @tables;
OPEN cur;
FETCH NEXT FROM cur INTO @tbl;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = '
        IF NOT EXISTS (
            SELECT 1 FROM sys.columns WHERE Name = ''RSHostelId'' 
            AND Object_ID = OBJECT_ID(''rsms.' + @tbl + ''')
        )
        BEGIN
            ALTER TABLE rsms.' + @tbl + ' ADD RSHostelId UNIQUEIDENTIFIER NULL;
            PRINT '' Added RSHostelId to rsms.' + @tbl + ''';
        END';
    EXEC (@sql);
    FETCH NEXT FROM cur INTO @tbl;
END
CLOSE cur;
DEALLOCATE cur;
GO

--------------------------------------------------------------------------------
-- 3. Update existing data with current HostelId
--------------------------------------------------------------------------------

DECLARE @HostelId UNIQUEIDENTIFIER = 'D1F90D34-37FB-4205-AD03-4F5A82E61DC8';
UPDATE rsms.Staff SET RSHostelId = @HostelId WHERE RSHostelId IS NULL;
UPDATE rsms.StockLedger SET RSHostelId = @HostelId WHERE RSHostelId IS NULL;
UPDATE rsms.Users SET RSHostelId = @HostelId WHERE RSHostelId IS NULL;
UPDATE rsms.Suppliers SET RSHostelId = @HostelId WHERE RSHostelId IS NULL;
UPDATE rsms.PurchaseInvoices SET RSHostelId = @HostelId WHERE RSHostelId IS NULL;
UPDATE rsms.AssetIssues SET RSHostelId = @HostelId WHERE RSHostelId IS NULL;
GO

--------------------------------------------------------------------------------
-- 4. Alter columns to NOT NULL & add FK constraints (to RSHostels)
--------------------------------------------------------------------------------
DECLARE @sql NVARCHAR(MAX);
-- Tables that should reference RSHostels
DECLARE @tables TABLE (TableName NVARCHAR(100));
INSERT INTO @tables VALUES
(N'Staff'),
(N'Users'),
(N'Suppliers'),
(N'PurchaseInvoices'),
(N'StockLedger'),
(N'AssetIssues');
DECLARE @tbl NVARCHAR(100);
DECLARE cur2 CURSOR FOR SELECT TableName FROM @tables;
OPEN cur2;
FETCH NEXT FROM cur2 INTO @tbl;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = '
        BEGIN TRY
            ALTER TABLE rsms.' + @tbl + ' 
            ALTER COLUMN RSHostelId UNIQUEIDENTIFIER NOT NULL;

            IF NOT EXISTS (
                SELECT 1 FROM sys.foreign_keys WHERE name = ''FK_' + @tbl + '_Hostel''
            )
            BEGIN
                ALTER TABLE rsms.' + @tbl + '
                ADD CONSTRAINT FK_' + @tbl + '_Hostel FOREIGN KEY (RSHostelId)
                REFERENCES rsms.RSHostels(Id);
                PRINT '' Linked rsms.' + @tbl + ' to rsms.RSHostels'';
            END
        END TRY
        BEGIN CATCH
            PRINT '' Error processing rsms.' + @tbl + ': '' + ERROR_MESSAGE();
        END CATCH
    ';
    EXEC (@sql);
    FETCH NEXT FROM cur2 INTO @tbl;
END
CLOSE cur2;
DEALLOCATE cur2;
GO

--------------------------------------------------------------------------------
-- 5. Optional: Add indexes for performance
--------------------------------------------------------------------------------
DECLARE @sql NVARCHAR(MAX);
-- Tables that should reference RSHostels
DECLARE @tables TABLE (TableName NVARCHAR(100));
INSERT INTO @tables VALUES
(N'Staff'),
(N'Users'),
(N'Suppliers'),
(N'PurchaseInvoices'),
(N'StockLedger'),
(N'AssetIssues');
DECLARE @tbl NVARCHAR(100);
DECLARE cur3 CURSOR FOR SELECT TableName FROM @tables;
OPEN cur3;
FETCH NEXT FROM cur3 INTO @tbl;
WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = '
        IF NOT EXISTS (
            SELECT 1 FROM sys.indexes 
            WHERE name = ''IX_' + @tbl + '_RSHostelId'' AND object_id = OBJECT_ID(''rsms.' + @tbl + ''')
        )
        BEGIN
            CREATE INDEX IX_' + @tbl + '_RSHostelId ON rsms.' + @tbl + '(RSHostelId);
            PRINT '' Created index IX_' + @tbl + '_RSHostelId'';
        END';
    EXEC (@sql);
    FETCH NEXT FROM cur3 INTO @tbl;
END
CLOSE cur3;
DEALLOCATE cur3;
GO

