USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsValidUser]
(@name nvarchar(20),
@password nvarchar(max))
AS
IF EXISTS (SELECT Users.Id, Users.Name
  FROM [dbo].Users WHERE Name=@name and Password=@password)
  return 1
  ELSE
  return 0