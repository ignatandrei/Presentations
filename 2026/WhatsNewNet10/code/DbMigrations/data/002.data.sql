USE [database]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 
GO
INSERT [dbo].[Department] ([Id], [Name]) VALUES (1, N'HR')
GO
INSERT [dbo].[Department] ([Id], [Name]) VALUES (2, N'IT')
GO
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [TerminationDate]) VALUES (1, N'A HR', 1, NULL)
GO
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [TerminationDate]) VALUES (2, N'A IT 1', 2, CAST(N'2025-01-01' AS Date))
GO
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [TerminationDate]) VALUES (3, N'A IT 2', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
