USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEventsByCategoryIdAndDateAndState]
(
@categoryId int = NULL,
@date date = NULL,
@state int = NULL
)
AS 

IF @state=-1
BEGIN
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date) WHERE CONVERT(DATE,Date) < CAST(GETDATE() AS DATE)
END
ELSE IF @state=0
BEGIN
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date) WHERE CONVERT(DATE,Date)  = CAST(GETDATE() AS DATE)
END
ELSE IF @state=1
BEGIN
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date) WHERE CONVERT(DATE,Date)  > CAST(GETDATE() AS DATE)
END
ELSE
BEGIN
SELECT id, Name, [Date], OrganizerId, CategoryId, ImageUrl, [Description], AddressId FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date)
END