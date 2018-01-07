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
SELECT distinct dbo.[Events].id, dbo.[Events].Name, dbo.[Events].[Date], dbo.[Events].OrganizerId, dbo.[Events].CategoryId, dbo.[Events].ImageUrl, dbo.[Events].[Description], dbo.[Events].AddressId
FROM dbo.[Events] INNER JOIN  (SELECT r.id
From Recursion r) categoryRecursion ON CASE WHEN @categoryId IS NULL THEN categoryRecursion.id ELSE [Events].CategoryId END = categoryRecursion.id   
WHERE CONVERT(DATE,dbo.[Events].Date) = isnull(@date,CONVERT(DATE,dbo.[Events].Date))
)