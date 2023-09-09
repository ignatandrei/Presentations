--SELECT name, is_broker_enabled FROM sys.databases

--ALTER DATABASE Test SET ENABLE_BROKER;

create database Test;
go
ALTER DATABASE Test SET ENABLE_BROKER;
go
USE [Test]
GO

/****** Object:  Table [dbo].[Department]    Script Date: 9/9/2023 1:52:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Department](
	[IdDept] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

update Department set Name='asxaasd' where IdDept=1
insert into Department( Name) values ('asdasd')



