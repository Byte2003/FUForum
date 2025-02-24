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
GO

CREATE SEQUENCE [KnowledgeBaseSequence] START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [ActivityLogs] (
    [Id] int NOT NULL IDENTITY,
    [Action] varchar(50) NOT NULL,
    [EntityName] varchar(50) NOT NULL,
    [EntityId] varchar(50) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    [UserId] varchar(50) NOT NULL,
    [Content] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoles] (
    [Id] varchar(50) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] varchar(50) NOT NULL,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Dob] datetime2 NOT NULL,
    [NumberOfKnowledgeBases] int NULL,
    [NumberOfVotes] int NULL,
    [NumberOfReports] int NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Attachments] (
    [Id] int NOT NULL IDENTITY,
    [FileName] nvarchar(200) NOT NULL,
    [FilePath] nvarchar(200) NOT NULL,
    [FileType] varchar(4) NOT NULL,
    [FileSize] bigint NOT NULL,
    [KnowledgeBaseId] int NULL,
    [CommentId] int NULL,
    [Type] varchar(10) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [SeoAlias] varchar(200) NOT NULL,
    [SeoDescription] nvarchar(500) NOT NULL,
    [SortOrder] int NOT NULL,
    [ParentId] int NULL,
    [NumberOfTickets] int NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CommandInFunctions] (
    [CommandId] varchar(50) NOT NULL,
    [FunctionId] varchar(50) NOT NULL,
    CONSTRAINT [PK_CommandInFunctions] PRIMARY KEY ([CommandId], [FunctionId])
);
GO

CREATE TABLE [Commands] (
    [Id] varchar(50) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Commands] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(500) NOT NULL,
    [KnowledgeBaseId] int NOT NULL,
    [OwnwerUserId] varchar(50) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Functions] (
    [Id] varchar(50) NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Url] nvarchar(200) NOT NULL,
    [SortOrder] int NOT NULL,
    [ParentId] varchar(50) NOT NULL,
    CONSTRAINT [PK_Functions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [KnowledgeBases] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [Title] nvarchar(500) NOT NULL,
    [SeoAlias] varchar(500) NOT NULL,
    [Description] nvarchar(500) NOT NULL,
    [Environment] nvarchar(500) NOT NULL,
    [Problem] nvarchar(500) NOT NULL,
    [StepToReproduce] nvarchar(max) NOT NULL,
    [ErrorMessage] nvarchar(500) NOT NULL,
    [Workaround] nvarchar(500) NOT NULL,
    [Note] nvarchar(max) NOT NULL,
    [OwnerUserId] varchar(50) NOT NULL,
    [Labels] nvarchar(max) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    [NumberOfComments] int NULL,
    [NumberOfVotes] int NULL,
    [NumberOfReports] int NULL,
    CONSTRAINT [PK_KnowledgeBases] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [LabelInKnowledgeBases] (
    [KnowledgeBaseId] int NOT NULL,
    [LabelId] varchar(50) NOT NULL,
    CONSTRAINT [PK_LabelInKnowledgeBases] PRIMARY KEY ([KnowledgeBaseId], [LabelId])
);
GO

CREATE TABLE [Labels] (
    [Id] varchar(50) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Labels] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Permissions] (
    [FunctionId] varchar(50) NOT NULL,
    [RoleId] varchar(50) NOT NULL,
    [CommandId] varchar(50) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([FunctionId], [RoleId], [CommandId])
);
GO

CREATE TABLE [Reports] (
    [Id] int NOT NULL IDENTITY,
    [KnowledgeBaseId] int NULL,
    [CommentId] int NULL,
    [Content] nvarchar(500) NOT NULL,
    [ReportUserId] varchar(50) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    [IsProcessed] bit NOT NULL,
    [Type] varchar(50) NOT NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Votes] (
    [KnowledgeBaseId] int NOT NULL,
    [UserId] varchar(50) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastModifiedDate] datetime2 NULL,
    CONSTRAINT [PK_Votes] PRIMARY KEY ([KnowledgeBaseId], [UserId])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] varchar(50) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] varchar(50) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] varchar(50) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] varchar(50) NOT NULL,
    [RoleId] varchar(50) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] varchar(50) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240414170010_InitialDb', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Functions]') AND [c].[name] = N'ParentId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Functions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Functions] ALTER COLUMN [ParentId] varchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240415070820_EnableNullableForFunctionTable', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Comments].[OwnwerUserId]', N'OwnerUserId', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240526082357_Rename field in Comment table', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Attachments]') AND [c].[name] = N'CommentId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Attachments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Attachments] DROP COLUMN [CommentId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Attachments]') AND [c].[name] = N'Type');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Attachments] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Attachments] DROP COLUMN [Type];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reports]') AND [c].[name] = N'KnowledgeBaseId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Reports] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [Reports] SET [KnowledgeBaseId] = 0 WHERE [KnowledgeBaseId] IS NULL;
ALTER TABLE [Reports] ALTER COLUMN [KnowledgeBaseId] int NOT NULL;
ALTER TABLE [Reports] ADD DEFAULT 0 FOR [KnowledgeBaseId];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Attachments]') AND [c].[name] = N'KnowledgeBaseId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Attachments] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [Attachments] SET [KnowledgeBaseId] = 0 WHERE [KnowledgeBaseId] IS NULL;
ALTER TABLE [Attachments] ALTER COLUMN [KnowledgeBaseId] int NOT NULL;
ALTER TABLE [Attachments] ADD DEFAULT 0 FOR [KnowledgeBaseId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240604154716_Adjust attachment and report model', N'8.0.4');
GO

COMMIT;
GO

