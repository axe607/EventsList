USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectCategories]
AS
SELECT [id],[pid],[Name]
  FROM [dbo].[Categories]