USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUser](
@name nvarchar(20),
@password nvarchar(max),
@email nvarchar(30)
)
AS
INSERT dbo.Users
VALUES(
@name,
@password,
@email)