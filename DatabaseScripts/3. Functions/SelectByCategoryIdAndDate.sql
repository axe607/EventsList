USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SelectByCategoryIdAndDate]
(
@categoryId int = null,
@date date = NULL
)
RETURNS TABLE 
AS
RETURN 
(
with Recursion
as
(
select id, pid, name
from Categories
where  id = isnull(@categoryId,id)
union all
select c.id, c.pid, c.name
FROM Categories c INNER JOIN Recursion r ON c.pid = r.id
)
SELECT distinct dbo.[Events].*
FROM  (SELECT r.id
From Recursion r) r1 INNER JOIN [Events] ON r1.id = [Events].CategoryId 
WHERE CONVERT(DATE,dbo.[Events].Date) = isnull(@date,CONVERT(DATE,dbo.[Events].Date))
)