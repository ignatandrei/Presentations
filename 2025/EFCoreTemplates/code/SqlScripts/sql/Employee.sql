if not exists (select * from sys.objects where object_id = object_id(N'Employee') and type in (N'U'))
begin
    print 'Creating table Employee'
Create Table Employee
(
    Id INT PRIMARY KEY  identity,
    Name NVARCHAR(100) NOT NULL,
    DepartmentId INT FOREIGN KEY REFERENCES Department(Id)
);

Insert into Employee (Name, DepartmentId) values ('A HR', 1);
Insert into Employee (Name, DepartmentId) values ('A IT 1', 2);
Insert into Employee (Name, DepartmentId) values ('A IT 2', 2);
end

