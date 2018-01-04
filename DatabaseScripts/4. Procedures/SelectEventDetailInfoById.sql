USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectEventDetailInfoById](
@eventId int
)
AS
SELECT        dbo.Events.id AS EventId, dbo.Events.Name AS EventName, dbo.Events.Date, dbo.Events.ImageUrl, dbo.Events.Description, dbo.Addresses.Address, dbo.Categories.Name AS CategoryName, 
                         dbo.Organizers.Name AS OrganizerName, dbo.Events.OrganizerId, dbo.Users.Name
FROM            dbo.Events  LEFT JOIN
                         dbo.Categories ON dbo.Events.CategoryId = dbo.Categories.id LEFT JOIN
                         dbo.Addresses ON dbo.Events.AddressId = dbo.Addresses.id LEFT JOIN
                         dbo.Organizers ON dbo.Events.OrganizerId = dbo.Organizers.id LEFT JOIN
                         dbo.Users ON dbo.Organizers.id = dbo.Users.id
WHERE dbo.[Events].id=@eventId;