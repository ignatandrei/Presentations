-- SQL Server init script

-- Create the AddressBook database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'EmpDep')
BEGIN
  CREATE DATABASE EmpDep;
END;
GO

USE EmpDep;
GO

-- Create the Employee table
IF OBJECT_ID(N'Employee', N'U') IS NULL
BEGIN
    CREATE TABLE Employee
    (
        Id        INT PRIMARY KEY IDENTITY(1,1) ,
        IdDepartment INT NOT NULL,
        FirstName VARCHAR(255) NOT NULL,
        LastName  VARCHAR(255) NOT NULL,
        Email     VARCHAR(255) NULL,
        Phone     VARCHAR(255) NULL
    );
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