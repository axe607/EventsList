USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCategoryById](
@categoryId int
)
AS 
BEGIN 
with Recursion 
as 
( 
select id, pid, name 
from Categories 
where id = @categoryId 
union all 
select c.id, c.pid,c.name 
FROM Categories c INNER JOIN Recursion r ON c.pid = r.id 
) 
delete from Categories 
where id in (select id from Recursion) 
end