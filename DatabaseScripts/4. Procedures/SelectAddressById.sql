USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectAddressById]
(@addressId int)
AS
SELECT*
  FROM [dbo].Addresses WHERE id=@addressId