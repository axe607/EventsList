USE [EventsListDB]
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
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (1, N'Maxii', N'123456', N'2@1.co')
GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (2, N'Kostik', N'98935500', N'1@2.com')
GO
INSERT [dbo].[Users] ([id], [Name], [Password], [Email]) VALUES (3, N'Zhenya', N'123456', N'2@2.com')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (1, 1)
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (2, 2)
GO
INSERT [dbo].[UsersRoles] ([RoleId], [UserId]) VALUES (3, 3)
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (1, N'ШоуМастер')
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (2, N'Conference')
GO
INSERT [dbo].[Organizers] ([id], [Name]) VALUES (3, NULL)
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
SET IDENTITY_INSERT [dbo].[Addresses] ON 

GO
INSERT [dbo].[Addresses] ([id], [Address]) VALUES (1, N'Кафе "Огёнёк"')
GO
INSERT [dbo].[Addresses] ([id], [Address]) VALUES (2, N'Беларусь, Могилёвская область, Могилёв, Пушкинский просп., 7')
GO
INSERT [dbo].[Addresses] ([id], [Address]) VALUES (3, N'Беларусь, Минск, просп. Победителей, 7А')
GO
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Events] ON 

GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (1, N'Творческий вечер', CAST(N'2017-11-23 15:45:00.000' AS DateTime), 1, 7, NULL, N'Творческая встреча творческих людей', 1)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (3, N'Творческий проект', CAST(N'2017-11-25 00:00:00.000' AS DateTime), 2, 8, NULL, N'Творческая встреча творческих людей', 3)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (4, N'Что?Где?Когда?', CAST(N'2017-12-11 00:00:00.000' AS DateTime), 1, 10, N'15056501883900.jpg', N'Интелектуальная игра Что?Где?Когда?', 2)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (5, N'Что?Где?Когда?', CAST(N'2017-12-21 00:00:00.000' AS DateTime), 1, 10, N'15056501883900.jpg', N'Что?Где?Когда?', 2)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (6, N'Конференция по блокчейну', CAST(N'2017-12-05 12:30:00.000' AS DateTime), 2, 12, N'baf3edbdfd6d719febed5.png', N'Конференция', 3)
GO
INSERT [dbo].[Events] ([id], [Name], [Date], [OrganizerId], [CategoryId], [ImageUrl], [Description], [AddressId]) VALUES (13, N'Test3', CAST(N'2018-01-01 01:00:00.000' AS DateTime), 3, 17, NULL, N'2018', 3)
GO
SET IDENTITY_INSERT [dbo].[Events] OFF
GO
SET IDENTITY_INSERT [dbo].[Phones] ON 

GO
INSERT [dbo].[Phones] ([id], [OrganizerId], [PhoneNumber]) VALUES (1, 1, N'+375-29-111-11-11')
GO
INSERT [dbo].[Phones] ([id], [OrganizerId], [PhoneNumber]) VALUES (3, 2, N'+375-29-909-01-14')
GO
INSERT [dbo].[Phones] ([id], [OrganizerId], [PhoneNumber]) VALUES (4, 1, N'+375-29-123-11-23')
GO
INSERT [dbo].[Phones] ([id], [OrganizerId], [PhoneNumber]) VALUES (6, 1, N'+375-29-123-22-33')
GO
SET IDENTITY_INSERT [dbo].[Phones] OFF
GO
SET IDENTITY_INSERT [dbo].[Emails] ON 

GO
INSERT [dbo].[Emails] ([id], [OrganizerId], [Email]) VALUES (2, 2, N'conference.minsk@yandex.by')
GO
INSERT [dbo].[Emails] ([id], [OrganizerId], [Email]) VALUES (3, 2, N'conference.mogilev@yandex.by')
GO
INSERT [dbo].[Emails] ([id], [OrganizerId], [Email]) VALUES (4, 1, N'max@outlook.com')
GO
SET IDENTITY_INSERT [dbo].[Emails] OFF
GO
