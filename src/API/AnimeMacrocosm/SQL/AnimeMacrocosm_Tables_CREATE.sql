BEGIN TRANSACTION;

CREATE TABLE [AnimeItems] (
    [SeriesItemId] int NOT NULL IDENTITY,
    [SeriesId] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImageId] int NOT NULL,
    [ProductionId] int NOT NULL,
    [DistributorId] int NOT NULL,
    [CreatorAuthorId] int NOT NULL,
    [RunTime] nvarchar(max) NULL,
    [FormatId] int NOT NULL,
    [ReleaseDate] datetime2 NULL,
    CONSTRAINT [PK_AnimeItems] PRIMARY KEY ([SeriesItemId])
);

GO

CREATE TABLE [CreatorAuthors] (
    [CreatorAuthorId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    CONSTRAINT [PK_CreatorAuthors] PRIMARY KEY ([CreatorAuthorId])
);

GO

CREATE TABLE [Distributors] (
    [DistributorId] int NOT NULL IDENTITY,
    [DistributorName] nvarchar(max) NULL,
    CONSTRAINT [PK_Distributors] PRIMARY KEY ([DistributorId])
);

GO

CREATE TABLE [Genres] (
    [GenreId] int NOT NULL IDENTITY,
    [GenreType] nvarchar(max) NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([GenreId])
);

GO

CREATE TABLE [Images] (
    [ImageId] int NOT NULL IDENTITY,
    [ImagePath] nvarchar(max) NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([ImageId])
);

GO

CREATE TABLE [MangaItems] (
    [SeriesItemId] int NOT NULL IDENTITY,
    [SeriesId] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ImageId] int NOT NULL,
    [DistributorId] int NOT NULL,
    [CreatorAuthorId] int NOT NULL,
    [PageCount] int NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    CONSTRAINT [PK_MangaItems] PRIMARY KEY ([SeriesItemId])
);

GO

CREATE TABLE [MediaTypes] (
    [MediaTypeId] int NOT NULL IDENTITY,
    [MediaTypeName] nvarchar(max) NULL,
    CONSTRAINT [PK_MediaTypes] PRIMARY KEY ([MediaTypeId])
);

GO

CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [PostTitle] nvarchar(100) NULL,
    [PostCreator] nvarchar(30) NULL,
    [PostDate] datetime2 NOT NULL,
    [PostContent] nvarchar(max) NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId])
);

GO

CREATE TABLE [ProductionStudios] (
    [ProductionStudioId] int NOT NULL IDENTITY,
    [ProductionStudioName] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductionStudios] PRIMARY KEY ([ProductionStudioId])
);

GO

CREATE TABLE [Series] (
    [SeriesId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL
	CONSTRAINT [PK_Series] PRIMARY KEY ([SeriesId])
);

GO

CREATE TABLE [Series_SeriesItems] (
	[SeriesId] int NOT NULL,
	[SeriesItemId] int NOT NULL,
	CONSTRAINT [PK_Series_SeriesItems] PRIMARY KEY CLUSTERED
	(
		[SeriesId] ASC,
		[SeriesItemId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Series_Creators](
	[SeriesId] [int] NOT NULL,
	[CreatorId] [int] NOT NULL,
 CONSTRAINT [PK_SeriesCreators] PRIMARY KEY CLUSTERED 
(
	[CreatorId] ASC,
	[SeriesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SeriesItem_Images](
	[SeriesItemId] [int] NOT NULL,
	[ImageId] [int] NOT NULL,
 CONSTRAINT [PK_SeriesItemImages] PRIMARY KEY CLUSTERED 
(
	[SeriesItemId] ASC,
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [User] (
    [UserId] int NOT NULL IDENTITY,
    [UserEmailAddress] nvarchar(max) NULL,
    [UserScreenName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [User_Posts] (
	[UserId] int NOT NULL,
	[PostId] int NOT NULL,
	CONSTRAINT [PK_User_Posts] PRIMARY KEY CLUSTERED
	(
		[UserId] ASC,
		[PostId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

Go


ROLLBACK;

COMMIT;