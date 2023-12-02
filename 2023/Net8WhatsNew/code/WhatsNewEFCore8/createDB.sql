--//https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew
--//https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes
--//docker pull mcr.microsoft.com/mssql/server:2022-latest
--//docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
--//dotnet tool install --global dotnet-ef --no-cache
--//dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=Test;UID=sa;PWD=yourStrong(!)Password;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer

USE [master]
GO
/****** Object:  Database [test]    Script Date: 12/2/2023 5:16:41 PM ******/
CREATE DATABASE [test]
GO
ALTER DATABASE [test] SET COMPATIBILITY_LEVEL = 160
GO
USE [test]
GO
/****** Object:  Table [dbo].[Emp]    Script Date: 12/2/2023 5:16:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emp](
	[ID] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Emp] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [test] SET  READ_WRITE 
GO

USE [test]
GO

INSERT INTO [dbo].[Emp]
           (
           [Name])
     VALUES
           ('andre;')
GO


