/****** Object:  Database MomentumTest    Script Date: 5/15/2013 3:36:19 PM ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MomentumTest')
	DROP DATABASE [MomentumTest]
GO

CREATE DATABASE [MomentumTest]  ON (NAME = N'MomentumTest_Data', FILENAME = N'd:\Program Files\Microsoft SQL Server\MSSQL\data\MomentumTest_Data.MDF' , SIZE = 1, FILEGROWTH = 10%) LOG ON (NAME = N'MomentumTest_Log', FILENAME = N'd:\Program Files\Microsoft SQL Server\MSSQL\data\MomentumTest_Log.LDF' , SIZE = 1, FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

use [MomentumTest]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 5/15/2013 3:36:22 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Contact]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Contact]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 5/15/2013 3:36:22 PM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Customer]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Customer]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 5/15/2013 3:36:24 PM ******/
CREATE TABLE [dbo].[Contact] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[CustomerId] [int] NOT NULL ,
	[CreateDate] [datetime] NOT NULL ,
	[Note] [varchar] (240) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 5/15/2013 3:36:25 PM ******/
CREATE TABLE [dbo].[Customer] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[FirstName] [varchar] (70) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[LastName] [varchar] (70) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PhoneNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Email] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

