USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateOrganizerInfo](
@organizerId int,
@name nvarchar(20) = NULL
)
AS
UPDATE dbo.Organizers
SET Name = isnull(@name,Name)
WHERE id = @organizerId