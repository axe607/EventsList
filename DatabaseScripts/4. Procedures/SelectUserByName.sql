USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectUserByName]
(@name nvarchar(20))
AS
SELECT Users.Id, Users.Name, Users.Email, Organizers.Name as OrganizerName
  FROM [dbo].Users
  INNER JOIN dbo.Organizers ON dbo.Users.id=dbo.Organizers.id WHERE Users.Name=@name