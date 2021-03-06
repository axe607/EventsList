USE [EventsListDB]
GO
set language english
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

GO
INSERT [dbo].[Addresses] ([id], [Address]) VALUES (2, N'Беларусь, Могилёвская область, Могилёв, Пушкинский просп., 7')
GO
INSERT [dbo].[Addresses] ([id], [Address]) VALUES (3, N'Беларусь, Минск, просп. Победителей, 7А')
GO
INSERT [dbo].[Addresses] ([id], [Address]) VALUES (5, N'Кафе "Огёнёк"')
GO
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (1, N'Творческие', NULL)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (2, N'Досуг', NULL)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (3, N'Конференция', NULL)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (4, N'Музыкальные', NULL)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (5, N'На открытом воздухе', NULL)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (6, N'Творческая встреча', 1)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (7, N'Творческий вечер', 1)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (8, N'Творческий проект', 1)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (9, N'Досуговая программа', 2)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (10, N'Интеллектуальная игра', 2)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (11, N'Бизнесс конференция', 3)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (12, N'IT конференция', 3)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (13, N'Форум', 3)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (14, N'Фестиваль', 4)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (15, N'Концерт', 4)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (16, N'Велособытие', 5)
GO
INSERT [dbo].[Categories] ([id], [Name], [pid]) VALUES (17, N'Салют', 5)
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (2, N'Kostik', N'8C-B2-23-7D-06-79-CA-88-DB-64-64-EA-C6-0D-A9-63-45-51-39-64', N'1@X.com')
GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (13, N'Max', N'7C-4A-8D-09-CA-37-62-AF-61-E5-95-20-94-3D-C2-64-94-F8-94-1B', N'13@1.co')
GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (19, N'AXE', N'7C-4A-8D-09-CA-37-62-AF-61-E5-95-20-94-3D-C2-64-94-F8-94-1B', N'max@axe.com')
GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (24, N'MsTest', N'40-BD-00-15-63-08-5F-C3-51-65-32-9E-A1-FF-5C-5E-CB-DB-BE-EF', N'm@m.com')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (2, N'Conference')
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (13, N'OrgStudio')
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (19, NULL)
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (24, N'Org++')
GO
SET IDENTITY_INSERT [dbo].[Events] ON 

GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (6, N'Конференция по блокчейну', CAST(N'2017-05-12 12:30:00.000' AS DateTime), 2, 12, N'bc.png', N'Конференция', 3)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (14, N'ЧТО?ГДЕ?КОГДА?', CAST(N'2017-12-16 15:30:00.000' AS DateTime), 13, 10, N'www.jpg', N'Полуфинал', 2)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (16, N'Checkpoint Epam', CAST(N'2017-12-21 18:00:00.000' AS DateTime), 2, 13, N'web1.png', N'1', 2)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (17, N'Some very interesting event', CAST(N'2017-12-19 00:00:00.000' AS DateTime), 13, NULL, N'web2.png', N'1', 2)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (18, N'Some interesting event', CAST(N'2017-12-19 11:00:00.000' AS DateTime), 13, NULL, NULL, N'0', 2)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (19, N'Что?Где?Когда?', CAST(N'2018-01-16 16:30:00.000' AS DateTime), 13, 10, N'www.jpg', N'Что?Где?Когда?', 5)
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
SET IDENTITY_INSERT [dbo].[Emails] ON 

GO
INSERT [dbo].[Emails] ([id], [OrganizerId], [Email]) VALUES (2, 2, N'conference.minsk@yandex.by')
GO
INSERT [dbo].[Emails] ([id], [OrganizerId], [Email]) VALUES (9, 2, N'conference.mogilev@yandex.by')
GO
INSERT [dbo].[Emails] ([id], [OrganizerId], [Email]) VALUES (10, 13, N'max@gg.cc')
GO
SET IDENTITY_INSERT [dbo].[Emails] OFF
GO
SET IDENTITY_INSERT [dbo].[Phones] ON 

GO
INSERT [dbo].[Phones] ([id], [OrganizerId], [PhoneNumber]) VALUES (6, 2, N'+375-29-909-01-14')
GO
INSERT [dbo].[Phones] ([id], [OrganizerId], [PhoneNumber]) VALUES (7, 13, N'+375-29-888-77-66')
GO
SET IDENTITY_INSERT [dbo].[Phones] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Editor')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (3, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (1, 13)
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (2, 2)
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (2, 13)
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (2, 24)
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (3, 19)