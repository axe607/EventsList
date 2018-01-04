USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEmailByUserIdAndEmailId](
@userId int,
@emailId int
)
AS
DELETE FROM dbo.Emails WHERE OrganizerId = @userId AND id = @emailId