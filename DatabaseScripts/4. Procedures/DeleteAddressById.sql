USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteAddressById](
@addressId int
)
AS
DELETE FROM dbo.Addresses WHERE id = @addressId