USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectAddresses]
AS
SELECT id,[Address]
  FROM [dbo].Addresses