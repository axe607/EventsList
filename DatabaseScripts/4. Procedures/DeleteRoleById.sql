USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteRoleById](
@roleId int
)
AS
DELETE FROM dbo.Roles WHERE id = @roleId