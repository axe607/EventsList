USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserRole](
@userName nvarchar(20),
@roleId int
)
AS
INSERT dbo.UsersRoles
VALUES(
@roleId,
(SELECT Id FROM Users WHERE Name = @userName)
)