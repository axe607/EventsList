USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCategoryById]
(
@categoryId int
)
AS
SELECT id,pid,Name
  FROM [dbo].[Categories]
  WHERE id = @categoryId