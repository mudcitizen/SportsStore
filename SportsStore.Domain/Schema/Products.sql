USE [C:\USERS\LANEK\GOOGLE DRIVE\ASP.NET.MVC\SPORTSSTORE\SPORTSSTORE.DOMAIN\APP_DATA\SPORTSSTORE.MDF]
GO

/****** Object: Table [dbo].[Products] Script Date: 4/25/2017 7:34:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products] (
    [ProductID]   INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (500)  NOT NULL,
    [Category]    NVARCHAR (50)   NOT NULL,
    [Price]       DECIMAL (16, 2) NOT NULL,
	[ImageData]   VARBINARY(MAX)  NULL ,
	[ImageMimeType]   VARCHAR(50) NULL
);


