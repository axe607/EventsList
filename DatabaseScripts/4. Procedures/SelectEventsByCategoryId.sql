USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEventsByCategoryId]
(
@categoryId int
)
AS
BEGIN

with Recursion
as
(
select id, pid, name
from Categories
where  id = @categoryId
union all
select c.id, c.pid, c.name
FROM Categories c INNER JOIN Recursion r ON c.pid = r.id
)
SELECT dbo.[Events].id, dbo.[Events].Name, dbo.[Events].[Date], dbo.[Events].OrganizerId, dbo.[Events].CategoryId, dbo.[Events].ImageUrl, dbo.[Events].[Description], dbo.[Events].AddressId
FROM  (SELECT r.id
From Recursion r) r1 INNER JOIN [Events] ON r1.id = [Events].CategoryId
END