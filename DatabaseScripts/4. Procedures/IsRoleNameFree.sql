USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsRoleNameFree]
(@roleId int = NULL,
@name nvarchar(20)
)
AS
IF EXISTS (SELECT Roles.Name
  FROM [dbo].Roles WHERE Name=@name  and id !=isnull(@roleId,0))
  return 0
  ELSE
  return 1