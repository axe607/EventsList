USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectRoleById]
(@roleId int)
AS
SELECT Roles.Id, Roles.Name
  FROM [dbo].Roles WHERE Id= @roleId