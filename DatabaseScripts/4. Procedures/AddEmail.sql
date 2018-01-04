USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEmail](
@organizerId int,
@email nvarchar(50)
)
AS
INSERT dbo.Emails
VALUES(
@organizerId,
@email)