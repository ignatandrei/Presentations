USE EmpDep;
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

USE [EmpDep]
GO

INSERT INTO [dbo].[Employee]
           ([IdDepartment]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Phone])
     VALUES
           (1
           ,'Andrei'
           ,'Ignat'
           ,'ignatandrei@yahoo.com'
           ,'0728200034')
GO


