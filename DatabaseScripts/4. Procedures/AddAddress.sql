USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAddress](
@address nvarchar(100)
)
AS
INSERT dbo.Addresses
VALUES(
@address)