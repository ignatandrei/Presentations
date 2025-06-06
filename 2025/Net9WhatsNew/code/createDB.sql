--//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew
--//https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes
--//docker pull mcr.microsoft.com/mssql/server:2022-latest
--//docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
--//dotnet tool install --global dotnet-ef --no-
--//dotnet new install Microsoft.EntityFrameworkCore.Templates
--//dotnet new ef-templates
--//dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=tests;UID=sa;PWD=yourStrong(!)Password;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer

USE [master]
GO
/****** Object:  Database [test]    Script Date: 12/2/2023 5:16:41 PM ******/
create database tests
go

USE [tests]
GO
create table test(
id int,
name varchar(200)
)
/****** Object:  ForeignKey [FK_Employee_Department]    Script Date: 07/05/2010 01:50:15 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Department]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_Department]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/05/2010 01:50:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07/05/2010 01:50:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07/05/2010 01:50:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Department](
	[IDDepartment] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[IDDepartment] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
--GO
--SET IDENTITY_INSERT [dbo].[Department] ON
--INSERT [dbo].[Department] ([IDDepartment], [Name]) VALUES (2, N'IT')
--INSERT [dbo].[Department] ([IDDepartment], [Name]) VALUES (3, N'HR')
--SET IDENTITY_INSERT [dbo].[Department] OFF
/****** Object:  Table [dbo].[Employee]    Script Date: 07/05/2010 01:50:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[IDEmployee] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[IDDepartment] [bigint] NOT NULL,
	[Salary] [bigint] NOT NULL
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[IDEmployee] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Employee_Department]    Script Date: 07/05/2010 01:50:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Department]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([IDDepartment])
REFERENCES [dbo].[Department] ([IDDepartment])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Department]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
