USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEventsByUserId](
@userId int
)
AS
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId
  FROM [dbo].[Events]
  WHERE [dbo].[Events].OrganizerId=@userId