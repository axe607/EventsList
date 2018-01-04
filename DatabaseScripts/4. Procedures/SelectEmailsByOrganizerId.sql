USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEmailsByOrganizerId]
(@organizerId int)
AS
SELECT*
  FROM [dbo].Emails WHERE OrganizerId =@organizerId