USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCategory](
@categoryName nvarchar(50),
@pid int = null
)
AS
INSERT dbo.Categories
VALUES(
@categoryName,
@pid)