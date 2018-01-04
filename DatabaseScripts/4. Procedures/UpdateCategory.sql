USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCategory](
@categoryId int,
@pid int = NULL,
@categoryName nvarchar(50) = NULL
)
AS
UPDATE dbo.Categories
SET Name = isnull(@categoryName,Name),
    pid = @pid
WHERE id = @categoryId