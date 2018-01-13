USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUserInfo](
@userId int,
@name nvarchar(20) = NULL,
@password nvarchar(max) = NULL,
@email nvarchar(30) = NULL
)
AS
UPDATE dbo.Users
SET Name = isnull(@name,Name),
[Password] = isnull(@password,[Password]),
Email = isnull(@email,Email)
WHERE id = @userId