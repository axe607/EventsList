USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectRoles]
AS
SELECT Roles.Id, Roles.Name
  FROM [dbo].Roles