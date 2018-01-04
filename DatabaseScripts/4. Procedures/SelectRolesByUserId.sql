USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectRolesByUserId]
(@userId int)
AS
SELECT Roles.Id, Roles.Name
  FROM [dbo].Roles inner join dbo.UsersRoles ON dbo.UsersRoles.RoleId=dbo.Roles.Id WHERE dbo.UsersRoles.UserId=@userId