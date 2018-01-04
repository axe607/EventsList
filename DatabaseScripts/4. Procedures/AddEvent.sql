USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEvent](
@name nvarchar(50),
@date datetime,
@organizerId int,
@categoryId int = NULL,
@imageURL nvarchar(MAX) = null,
@description nvarchar(100),
@addressId int
)
AS
INSERT dbo.[Events]
VALUES(
@name,
@date,
@organizerId,
@categoryId,
@imageURL,
@description,
@addressId
)