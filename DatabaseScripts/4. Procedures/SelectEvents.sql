USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEvents]
AS
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId
  FROM [dbo].[Events]