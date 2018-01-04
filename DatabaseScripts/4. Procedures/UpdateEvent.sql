USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateEvent](
@eventId int,
@name nvarchar(50) = NULL,
@date datetime = NULL,
@categoryId int = NULL,
@imageURL nvarchar(MAX) = null,
@description nvarchar(100) = NULL,
@addressId int = NULL
)
AS
UPDATE dbo.[Events]
SET Name = isnull(@name,Name),
    [Date] = isnull(@date,[Date]),
	CategoryId = ISNULL(@categoryId,CategoryId),
	ImageUrl = @imageURL,
	[Description] = isnull(@description,[Description]),
	AddressId = ISNULL(@addressId,AddressId)
WHERE id = @eventId