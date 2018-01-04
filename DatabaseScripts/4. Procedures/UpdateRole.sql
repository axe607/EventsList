USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRole](
@roleId int,
@name nvarchar(20) = NULL
)
AS
UPDATE dbo.Roles
SET Name = isnull(@name,Name)
WHERE id = @roleId