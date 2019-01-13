DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Posts]') AND [c].[name] = N'PostCreator');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Posts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Posts] DROP COLUMN [PostCreator];

GO

ALTER TABLE [Posts] ADD [ApplicationUserRefId] int NOT NULL DEFAULT 0;

GO

CREATE TABLE [User] (
    [UserId] int NOT NULL IDENTITY,
    [UserEmailAddress] nvarchar(max) NULL,
    [UserScreenName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
);

GO

CREATE INDEX [IX_Posts_ApplicationUserRefId] ON [Posts] ([ApplicationUserRefId]);

GO

ALTER TABLE [Posts] ADD CONSTRAINT [FK_Posts_User_ApplicationUserRefId] FOREIGN KEY ([ApplicationUserRefId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181231025345_AddedUser', N'2.2.0-rtm-35687');

GO

ALTER TABLE [Posts] DROP CONSTRAINT [FK_Posts_User_ApplicationUserRefId];

GO

ALTER TABLE [User] DROP CONSTRAINT [PK_User];

GO

EXEC sp_rename N'[User]', N'Users';

GO

ALTER TABLE [Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]);

GO

ALTER TABLE [Posts] ADD CONSTRAINT [FK_Posts_Users_ApplicationUserRefId] FOREIGN KEY ([ApplicationUserRefId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181231030230_UpdatedUsertoUsers', N'2.2.0-rtm-35687');

GO

CREATE TABLE [AnimeItems] (
    [Id] int NOT NULL IDENTITY,
    [SeriesId] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImageId] int NOT NULL,
    [ProductionId] int NOT NULL,
    [DistributorId] int NOT NULL,
    [CreatorAuthorId] int NOT NULL,
    [RunTime] nvarchar(max) NULL,
    [FormatId] int NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    CONSTRAINT [PK_AnimeItems] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [CreatorAuthors] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    CONSTRAINT [PK_CreatorAuthors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Distributors] (
    [Id] int NOT NULL IDENTITY,
    [DistributorName] nvarchar(max) NULL,
    CONSTRAINT [PK_Distributors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [GenreType] nvarchar(max) NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Images] (
    [Id] int NOT NULL IDENTITY,
    [ImagePath] nvarchar(max) NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [MangaItems] (
    [Id] int NOT NULL IDENTITY,
    [SeriesId] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImageId] int NOT NULL,
    [DistributorId] int NOT NULL,
    [CreatorAuthorId] int NOT NULL,
    [PageCount] int NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    CONSTRAINT [PK_MangaItems] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [MediaTypes] (
    [Id] int NOT NULL IDENTITY,
    [MediaTypeName] nvarchar(max) NULL,
    CONSTRAINT [PK_MediaTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ProductionStudios] (
    [Id] int NOT NULL IDENTITY,
    [ProductionStudioName] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductionStudios] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Series] (
    [SeriesId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [CreatorAuthorId] int NOT NULL,
    [GenreId] int NOT NULL,
    [MediaTypeId] int NOT NULL,
    CONSTRAINT [PK_Series] PRIMARY KEY ([SeriesId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181231182147_AnimeMangaTables', N'2.2.0-rtm-35687');

GO

DROP TABLE [MediaTypes];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Series]') AND [c].[name] = N'MediaTypeId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Series] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Series] DROP COLUMN [MediaTypeId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MangaItems]') AND [c].[name] = N'ReleaseDate');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [MangaItems] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [MangaItems] ALTER COLUMN [ReleaseDate] datetime2 NULL;

GO

ALTER TABLE [MangaItems] ADD [FormatId] int NOT NULL DEFAULT 0;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AnimeItems]') AND [c].[name] = N'ReleaseDate');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AnimeItems] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [AnimeItems] ALTER COLUMN [ReleaseDate] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181231212057_RemovedMediaType', N'2.2.0-rtm-35687');

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Series]') AND [c].[name] = N'GenreId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Series] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Series] DROP COLUMN [GenreId];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190105180155_RemoveGenreFromSeries', N'2.2.0-rtm-35687');

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Series]') AND [c].[name] = N'CreatorAuthorId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Series] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Series] DROP COLUMN [CreatorAuthorId];

GO

CREATE TABLE [Formats] (
    [FormatId] int NOT NULL IDENTITY,
    [FormatName] nvarchar(max) NULL,
    CONSTRAINT [PK_Formats] PRIMARY KEY ([FormatId])
);

GO

CREATE TABLE [SeriesCreators] (
    [SeriesId] int NOT NULL,
    [CreatorId] int NOT NULL,
    CONSTRAINT [PK_SeriesCreators] PRIMARY KEY ([CreatorId], [SeriesId]),
    CONSTRAINT [FK_SeriesCreators_CreatorAuthors_CreatorId] FOREIGN KEY ([CreatorId]) REFERENCES [CreatorAuthors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SeriesCreators_Series_SeriesId] FOREIGN KEY ([SeriesId]) REFERENCES [Series] ([SeriesId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_SeriesCreators_SeriesId] ON [SeriesCreators] ([SeriesId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190106011645_AddSeriesCreator', N'2.2.0-rtm-35687');

GO

