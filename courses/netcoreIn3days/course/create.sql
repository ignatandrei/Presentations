--Todo 

GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_Department]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/12/2018 7:45:31 AM ******/
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/12/2018 7:45:31 AM ******/
DROP TABLE [dbo].[Department]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/12/2018 7:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[IDDepartment] [int] IDENTITY(1,1) NOT NULL,
	[NameDepartment] [nvarchar](50) NOT NULL,
	[TenantID] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[IDDepartment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/12/2018 7:45:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[IDEmployee] [int] IDENTITY(1,1) NOT NULL,
	[NameEmployee] [nvarchar](50) NOT NULL,
	[IDDepartment] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[IDEmployee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([IDDepartment], [NameDepartment], [TenantID]) VALUES (1, N'IT', NULL)
GO
INSERT [dbo].[Department] ([IDDepartment], [NameDepartment], [TenantID]) VALUES (2, N'HR', NULL)
GO
INSERT [dbo].[Department] ([IDDepartment], [NameDepartment], [TenantID]) VALUES (3, N'Information Technology', NULL)
GO
INSERT [dbo].[Department] ([IDDepartment], [NameDepartment], [TenantID]) VALUES (4, N'Human Resources', NULL)
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([IDEmployee], [NameEmployee], [IDDepartment]) VALUES (1, N'Andrei', 1)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([IDDepartment])
REFERENCES [dbo].[Department] ([IDDepartment])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
