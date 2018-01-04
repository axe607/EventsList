USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectPhonesByOrganizerId]
(@organizerId int)
AS
SELECT*
  FROM [dbo].Phones WHERE OrganizerId =@organizerId