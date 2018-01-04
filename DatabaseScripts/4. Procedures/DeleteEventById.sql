USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEventById](
@eventId int
)
AS
DELETE FROM dbo.[Events] WHERE id = @eventId