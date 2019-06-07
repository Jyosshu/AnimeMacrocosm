BEGIN TRANSACTION;

DROP TABLE IF EXISTS Series_Creators;
DROP TABLE IF EXISTS SeriesItem_Distributor;
DROP TABLE IF EXISTS SeriesItem_Production;
DROP TABLE IF EXISTS SeriesItem_Images;
DROP TABLE IF EXISTS CreatorAuthors;
DROP TABLE IF EXISTS Distributors;
DROP TABLE IF EXISTS ProductionStudios;
DROP TABLE IF EXISTS SeriesItems;
DROP TABLE IF EXISTS Series;
DROP TABLE IF EXISTS Formats;
DROP TABLE IF EXISTS Genres;
DROP TABLE IF EXISTS Images;
DROP TABLE IF EXISTS MediaTypes;
DROP TABLE IF EXISTS Posts;
DROP TABLE IF EXISTS Users;



CREATE TABLE CreatorAuthors
(
    CreatorAuthorId SERIAL PRIMARY KEY,
    FirstName_English VARCHAR(255) NULL,
    LastName_English VARCHAR(255) NULL,
    FirstName VARCHAR(255) NULL,
    LastName VARCHAR(255) NULL,
    CountryOfOrigin VARCHAR(50) NULL
);

CREATE TABLE Distributors
(
    DistributorId SERIAL PRIMARY KEY,
    DistributorName VARCHAR(255) NULL,
    CountryOfOrigin VARCHAR(50) NULL,
    WebsiteURL VARCHAR(255) NULL
);

CREATE TABLE Formats
(
    FormatId SERIAL PRIMARY KEY,
    FormatName VARCHAR(255) NULL
);

CREATE TABLE Genres
(
    GenreId SERIAL PRIMARY KEY,
    GenreType VARCHAR(255) NULL
);

CREATE TABLE Images
(
    ImageId SERIAL PRIMARY KEY,
    ImagePath VARCHAR(255) NULL,
    ImageText VARCHAR(255) NULL
);

CREATE TABLE MediaTypes
(
    MediaTypeId SERIAL PRIMARY KEY,
    MediaTypeName VARCHAR(255) NULL,
    Resolution VARCHAR(30) NULL
);

CREATE TABLE Users
(
    UserId SERIAL PRIMARY KEY,
    UserEmailAddress VARCHAR(255) NULL,
    UserScreenName VARCHAR(255) NOT NULL
);

CREATE TABLE Posts
(
    PostId SERIAL PRIMARY KEY,
    PostTitle VARCHAR(100) NULL,    
    PostDate TIMESTAMP NOT NULL,
    PostContent TEXT NULL,
    UserId INTEGER NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE ProductionStudios
(
    ProductionStudioId SERIAL PRIMARY KEY,
    ProductionStudioName VARCHAR(255) NULL,
    CountryOfOrigin VARCHAR(50) NULL,
    WebsiteURL VARCHAR(255) NULL
);

CREATE TABLE Series
(
    SeriesId SERIAL PRIMARY KEY,
    Title VARCHAR(255) NULL
);

CREATE TABLE SeriesItems
(
    SeriesItemId SERIAL PRIMARY KEY,
    SeriesId INTEGER NOT NULL,
    Title VARCHAR(255) NULL,
    Description TEXT NULL,
    Length VARCHAR(100) NULL,
    FormatId INTEGER NULL,
    ReleaseDate DATE NULL,
    CollectionNumber INTEGER NULL
    
    CONSTRAINT FK_SeriesItem_Series FOREIGN KEY (SeriesId) REFERENCES Series (SeriesId)
);

CREATE TABLE SeriesItem_Production
(
	SeriesItemId INTEGER REFERENCES SeriesItems (SeriesItemId) ON UPDATE CASCADE ON DELETE CASCADE,
	ProductionStudioId INTEGER REFERENCES ProductionStudios (ProductionStudioId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_SeriesItem_Production PRIMARY KEY (SeriesItemId, ProductionStudioId)
);

CREATE TABLE SeriesItem_Distributor (
	SeriesItemId INTEGER REFERENCES SeriesItems (SeriesItemId) ON UPDATE CASCADE ON DELETE CASCADE,
	DistributorId INTEGER REFERENCES Distributors (DistributorId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_SeriesItem_Distributor PRIMARY KEY (SeriesItemId, DistributorId)
);

CREATE TABLE Series_Creators(
	SeriesId INTEGER REFERENCES Series (SeriesId) ON UPDATE CASCADE ON DELETE CASCADE,
	CreatorAuthorId INTEGER REFERENCES CreatorAuthors (CreatorAuthorId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_SeriesCreators PRIMARY KEY (CreatorAuthorId, SeriesId)
);

CREATE TABLE SeriesItem_Images(
	SeriesItemId INTEGER REFERENCES SeriesItems (SeriesItemId) ON UPDATE CASCADE ON DELETE CASCADE,
	ImageId INTEGER REFERENCES Images (ImageId) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_SeriesItemImages PRIMARY KEY (SeriesItemId, ImageId)
);




--ROLLBACK;


COMMIT;