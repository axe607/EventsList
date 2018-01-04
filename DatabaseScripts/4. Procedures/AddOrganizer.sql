USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddOrganizer](
@userId int,
@name nvarchar(50)
)
AS
INSERT dbo.Organizers
VALUES(
@userId,
@name)
