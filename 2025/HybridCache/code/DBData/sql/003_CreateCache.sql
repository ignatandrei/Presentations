-- SQL Server init script

-- Create the AddressBook database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'CachingData')
BEGIN
  CREATE DATABASE CachingData;
END;
GO

USE CachingData;
GO

-- Create the Employee table
IF OBJECT_ID(N'TestCache', N'U') IS NULL
BEGIN

CREATE TABLE [dbo].[TestCache](
	[Id] [nvarchar](449) NOT NULL,
	[Value] [varbinary](max) NOT NULL,
	[ExpiresAtTime] [datetimeoffset](7) NOT NULL,
	[SlidingExpirationInSeconds] [bigint] NULL,
	[AbsoluteExpiration] [datetimeoffset](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END;

GO

-- Create the Department table
IF OBJECT_ID(N'Department', N'U') IS NULL
BEGIN
    CREATE TABLE Department
    (
        Id        INT PRIMARY KEY IDENTITY(1,1) ,
        Name VARCHAR(255) NOT NULL,
    );

END;
GO

ALTER TABLE dbo.Employee ADD CONSTRAINT
	FK_Employee_Department FOREIGN KEY
	(
	IdDepartment
	) REFERENCES dbo.Department
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

-- Insert some sample data into the Contacts table
IF (SELECT COUNT(*) FROM Department) = 0
BEGIN
    INSERT INTO Department (Name)
    VALUES
        ('IT'),
        ('HR');
END;


GO