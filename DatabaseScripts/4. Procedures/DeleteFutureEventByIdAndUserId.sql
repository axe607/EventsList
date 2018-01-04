USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteFutureEventByIdAndUserId](
@eventId int,
@userId int
)
AS
DELETE FROM dbo.[Events] WHERE id = @eventId AND OrganizerId = @userId AND CONVERT(DATE,Date)  > CAST(GETDATE() AS DATE)