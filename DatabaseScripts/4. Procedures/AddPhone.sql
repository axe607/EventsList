USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddPhone](
@userId int,
@phoneNumber nvarchar(20)
)
AS
INSERT dbo.Phones
VALUES(
@userId,
@phoneNumber)