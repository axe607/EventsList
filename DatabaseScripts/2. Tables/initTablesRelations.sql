USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Categories] FOREIGN KEY([pid])
REFERENCES [dbo].[Categories] ([id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Categories]
GO
ALTER TABLE [dbo].[Emails]  WITH CHECK ADD  CONSTRAINT [FK_Emails_Organizers] FOREIGN KEY([OrganizerId])
REFERENCES [dbo].[Organizers] ([id])
GO
ALTER TABLE [dbo].[Emails] CHECK CONSTRAINT [FK_Emails_Organizers]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Addresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Addresses]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Categories]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Organizers] FOREIGN KEY([OrganizerId])
REFERENCES [dbo].[Organizers] ([id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Organizers]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_Organizers] FOREIGN KEY([OrganizerId])
REFERENCES [dbo].[Organizers] ([id])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_Organizers]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users]