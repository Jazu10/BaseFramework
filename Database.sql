CREATE DATABASE BaseFramework;
USE BaseFramework;

CREATE TABLE [dbo].[Users]
(
	[UserId] [nvarchar](450) PRIMARY KEY REFERENCES AspNetUsers(Id),
	[FirstName] [varchar](max) NULL,
	[LastName] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[DOB] [datetime] NULL,
	[DOJ] [datetime] NULL,
	[Address] [varchar](max) NULL,
	[Gender] [varchar](max) NULL
);

CREATE TABLE News
(
	NewsId VARCHAR(50) PRIMARY KEY DEFAULT NEWID(),
	CreatedAt DATETIME,
	Subject VARCHAR(MAX),
	Content VARCHAR(MAX),
	Image VARCHAR(MAX),
	IsActive bit default 1,
	IsDeleted bit default 0,
	UserId nvarchar(450) REFERENCES Users(UserId)
);

CREATE TABLE Advertisements
(
	AdvertisementId VARCHAR(50) PRIMARY KEY DEFAULT NEWID(),
	CreatedAt DATETIME,
	Subject VARCHAR(MAX),
	Content VARCHAR(MAX),
	IsActive bit default 1,
	IsDeleted bit default 0,
	UserId nvarchar(450) REFERENCES Users(UserId)
);

CREATE TABLE Posts
(
	PostId VARCHAR(50) PRIMARY KEY DEFAULT NEWID(),
	CreatedAt DATETIME,
	Subject VARCHAR(MAX),
	Content VARCHAR(MAX),
	Image VARCHAR(MAX), 
	IsActive bit default 1,
	IsDeleted bit default 0,
	UserId nvarchar(450) REFERENCES Users(UserId)
);

CREATE TABLE Images
(
	ImageId VARCHAR(50) PRIMARY KEY DEFAULT NEWID(),
	AdvertisementId VARCHAR(50) REFERENCES ADVERTISEMENTS(AdvertisementId)
);

CREATE TABLE Likes
(
	UserId nvarchar(450) REFERENCES Users(UserId),
	ContentId VARCHAR(50),
	PRIMARY KEY(UserId, ContentId)
);

CREATE TABLE Comments
(
	UserId nvarchar(450) REFERENCES Users(UserId),
	ContentId VARCHAR(50),
	Comment VARCHAR(MAX),
	PRIMARY KEY(UserId, ContentId)
);