IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Patients] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(126) NOT NULL,
    [MiddleName] nvarchar(126) NULL,
    [LastName] nvarchar(126) NOT NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([Id])
);

CREATE TABLE [LabResults] (
    [Id] uniqueidentifier NOT NULL,
    [PatientId] uniqueidentifier NOT NULL,
    [TestName] nvarchar(max) NOT NULL,
    [ResultValue] nvarchar(max) NOT NULL,
    [Unit] nvarchar(max) NOT NULL,
    [ObservedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_LabResults] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LabResults_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_LabResults_PatientId] ON [LabResults] ([PatientId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907132328_InitialPatientsTable', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LabResults]') AND [c].[name] = N'Unit');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [LabResults] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [LabResults] ALTER COLUMN [Unit] nvarchar(32) NOT NULL;

DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LabResults]') AND [c].[name] = N'TestName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LabResults] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [LabResults] ALTER COLUMN [TestName] nvarchar(128) NOT NULL;

DECLARE @var2 nvarchar(max);
SELECT @var2 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LabResults]') AND [c].[name] = N'ResultValue');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [LabResults] DROP CONSTRAINT ' + @var2 + ';');
ALTER TABLE [LabResults] ALTER COLUMN [ResultValue] nvarchar(256) NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907142032_AddLabResultTableProperties', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Patients] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);

ALTER TABLE [LabResults] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260907185258_AddIsActiveColumnToEntities', N'10.0.11');

COMMIT;
GO

-- Insertion Scripts
USE [LabResultsDb]
GO

INSERT INTO [dbo].[Patients]
           ([Id]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[IsActive])
     VALUES
           ('4CD74513-D907-4379-0A64-08DF0CE43759'
           ,'John'
           ,'Michael'
           ,'Smith'
           ,1),
           ('7A3E2B91-6F42-4C8D-B1A5-9E73D42F8C16'
           ,'Emily'
           ,'Rose'
           ,'Johnson'
           ,1)
GO


USE [LabResultsDb]
GO

INSERT INTO [dbo].[LabResults]
           ([Id]
           ,[PatientId]
           ,[TestName]
           ,[ResultValue]
           ,[Unit]
           ,[ObservedDate]
           ,[IsActive])
     VALUES
           ('A12F4C83-5B29-4E71-9D36-2F8A7C1B6E45'
           ,'4CD74513-D907-4379-0A64-08DF0CE43759'
           ,'Hemoglobin'
           ,'14.2'
           ,'g/dL'
           ,'2026-09-01 09:30:00'
           ,1),

           ('B73D91E6-2A48-4F05-8C17-6D39E5A2B841'
           ,'4CD74513-D907-4379-0A64-08DF0CE43759'
           ,'Glucose'
           ,'95'
           ,'mg/dL'
           ,'2026-09-01 09:35:00'
           ,1),

           ('C56A82D4-9E13-47B6-AF28-3D71C9E5B204'
           ,'7A3E2B91-6F42-4C8D-B1A5-9E73D42F8C16'
           ,'Cholesterol'
           ,'185'
           ,'mg/dL'
           ,'2026-09-02 10:15:00'
           ,1),

           ('D91B47F2-6C35-4A89-BC16-5E72D8A3F940'
           ,'7A3E2B91-6F42-4C8D-B1A5-9E73D42F8C16'
           ,'Blood Pressure'
           ,'120/80'
           ,'mmHg'
           ,'2026-09-02 10:20:00'
           ,1)
GO