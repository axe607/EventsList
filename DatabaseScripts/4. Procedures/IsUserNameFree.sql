USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsUserNameFree]
(@name nvarchar(20)
)
AS
IF EXISTS (SELECT Users.Name
  FROM [dbo].Users WHERE Name=@name)
  return 0
  ELSE
  return 1