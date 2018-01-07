USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEventById](
@id int
)
AS
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId
  FROM [dbo].[Events]
  WHERE id=@id