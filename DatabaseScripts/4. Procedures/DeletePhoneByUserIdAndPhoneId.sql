USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeletePhoneByUserIdAndPhoneId](
@userId int,
@phoneId int
)
AS
DELETE FROM dbo.Phones WHERE OrganizerId = @userId AND id = @phoneId