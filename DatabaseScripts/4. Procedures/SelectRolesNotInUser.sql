USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectRolesNotInUser]
(@userName nvarchar(20))
AS
SELECT DISTINCT Roles.Id, Roles.Name
  FROM [dbo].Roles LEFT JOIN dbo.UsersRoles ON dbo.UsersRoles.RoleId=dbo.Roles.Id
  WHERE dbo.UsersRoles.RoleId NOT IN (SELECT RoleId FROM UsersRoles INNER JOIN Users ON UserId=Users.id WHERE Name =@userName ) OR dbo.UsersRoles.UserId is null