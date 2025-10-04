if not exists (select * from sys.objects where object_id = object_id(N'Department') and type in (N'U'))
begin
    print 'Creating table Department'
Create Table Department
(
    Id INT PRIMARY KEY  identity,
    Name NVARCHAR(100) NOT NULL,
);

Insert into Department (Name) values ('HR');
Insert into Department (Name) values ('IT');
end

