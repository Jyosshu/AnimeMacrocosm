BEGIN TRANSACTION;

DROP TABLE IF EXISTS AMdev.dbo.Series_Creators, AMdev.dbo.CreatorAuthors;

CREATE TABLE [CreatorAuthors] (
    [CreatorAuthorId] int NOT NULL IDENTITY,
    [FirstName_English] nvarchar(max) NULL,
    [LastName_English] nvarchar(max) NULL,
	[FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
	[CountryOfOrigin] nvarchar(50) NULL,
    CONSTRAINT [PK_CreatorAuthors] PRIMARY KEY ([CreatorAuthorId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Distributor, AMdev.dbo.Distributors;

CREATE TABLE [Distributors] (
    [DistributorId] int NOT NULL IDENTITY,
    [DistributorName] nvarchar(max) NULL,
	[CountryOfOrigin] nvarchar(50) NULL,
	[WebsiteURL] nvarchar(max) NULL,
    CONSTRAINT [PK_Distributors] PRIMARY KEY ([DistributorId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.Formats;

CREATE TABLE [Formats] (
    [FormatId] int NOT NULL IDENTITY,
    [FormatName] nvarchar(max) NULL,
    CONSTRAINT [PK_Formats] PRIMARY KEY ([FormatId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.Genres;

CREATE TABLE [Genres] (
    [GenreId] int NOT NULL IDENTITY,
    [GenreType] nvarchar(max) NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([GenreId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Images, AMdev.dbo.Images;

CREATE TABLE [Images] (
    [ImageId] int NOT NULL IDENTITY,
    [ImagePath] nvarchar(max) NULL,
	[ImageText] nvarchar(max) NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([ImageId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.MediaTypes;

CREATE TABLE [MediaTypes] (
    [MediaTypeId] int NOT NULL IDENTITY,
    [MediaTypeName] nvarchar(max) NULL,
	[Resolution] nvarchar(30) NULL,
    CONSTRAINT [PK_MediaTypes] PRIMARY KEY ([MediaTypeId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.Posts, AMdev.dbo.Users;

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [UserEmailAddress] nvarchar(max) NULL,
    [UserScreenName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.Posts;

CREATE TABLE [Posts] (
    [PostId] int NOT NULL IDENTITY,
    [PostTitle] nvarchar(100) NULL,    
    [PostDate] datetime2 NOT NULL,
    [PostContent] nvarchar(max) NULL,
	[UserId] int NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId]),
	FOREIGN KEY ([UserId]) REFERENCES Users([UserId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Production, AMdev.dbo.ProductionStudios;

CREATE TABLE [ProductionStudios] (
    [ProductionStudioId] int NOT NULL IDENTITY,
    [ProductionStudioName] nvarchar(max) NULL,
	[CountryOfOrigin] nvarchar(50) NULL,
	[WebsiteURL] nvarchar(max) NULL,
    CONSTRAINT [PK_ProductionStudios] PRIMARY KEY ([ProductionStudioId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.Series_Creators, AMdev.dbo.SeriesItems, AMdev.dbo.Series;

CREATE TABLE [Series] (
    [SeriesId] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL
	CONSTRAINT [PK_Series] PRIMARY KEY ([SeriesId])
);

GO

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Distributor, AMdev.dbo.SeriesItem_Production, AMdev.dbo.SeriesItem_Images, AMdev.dbo.SeriesItems;

CREATE TABLE [SeriesItems] (
    [SeriesItemId] int NOT NULL IDENTITY,
	[SeriesId] int NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Length] nvarchar(100) NULL,
    [FormatId] int NULL,
    [ReleaseDate] datetime2 NULL,
    CONSTRAINT [PK_AnimeItems] PRIMARY KEY ([SeriesItemId]),
	FOREIGN KEY ([SeriesId]) REFERENCES Series (SeriesId)
);

Go

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Production;

CREATE TABLE [SeriesItem_Production] (
	[SeriesItemId] int NOT NULL,
	[ProductionStudioId] int NOT NULL,
	CONSTRAINT [PK_SeriesItem_Production] PRIMARY KEY CLUSTERED
	(
		[SeriesItemId] ASC,
		[ProductionStudioId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	FOREIGN KEY ([SeriesItemId]) REFERENCES SeriesItems ([SeriesItemId]),
	FOREIGN KEY ([ProductionStudioId]) REFERENCES ProductionStudios ([ProductionStudioId])
) ON [PRIMARY]

GO

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Distributor;

CREATE TABLE [SeriesItem_Distributor] (
	[SeriesItemId] int NOT NULL,
	[DistributorId] int NOT NULL,
	CONSTRAINT [PK_SeriesItem_Distributor] PRIMARY KEY CLUSTERED
	(
		[SeriesItemId] ASC,
		[DistributorId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		FOREIGN KEY ([SeriesItemId]) REFERENCES SeriesItems ([SeriesItemId]),
		FOREIGN KEY ([DistributorId]) REFERENCES Distributors ([DistributorId])
) ON [PRIMARY]

GO

DROP TABLE IF EXISTS AMdev.dbo.Series_Creators;

CREATE TABLE [dbo].[Series_Creators](
	[SeriesId] [int] NOT NULL,
	[CreatorAuthorId] [int] NOT NULL,
 CONSTRAINT [PK_SeriesCreators] PRIMARY KEY CLUSTERED 
(
	[CreatorAuthorId] ASC,
	[SeriesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
FOREIGN KEY ([SeriesId]) REFERENCES Series ([SeriesId]),
FOREIGN KEY ([CreatorAuthorId]) REFERENCES CreatorAuthors ([CreatorAuthorId])
) ON [PRIMARY]
GO

DROP TABLE IF EXISTS AMdev.dbo.SeriesItem_Images;

CREATE TABLE [dbo].[SeriesItem_Images](
	[SeriesItemId] [int] NOT NULL,
	[ImageId] [int] NOT NULL,
 CONSTRAINT [PK_SeriesItemImages] PRIMARY KEY CLUSTERED 
(
	[SeriesItemId] ASC,
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
FOREIGN KEY ([SeriesItemId]) REFERENCES SeriesItems ([SeriesItemId]),
FOREIGN KEY ([ImageId]) REFERENCES Images ([ImageId])
) ON [PRIMARY]
GO



--ROLLBACK;


--COMMIT;