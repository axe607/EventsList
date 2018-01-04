USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUserRole](
@userName nvarchar(20),
@roleId int
)
AS
DELETE FROM dbo.UsersRoles WHERE UserId = (SELECT Id FROM Users WHERE Name = @userName) AND RoleId = @roleId