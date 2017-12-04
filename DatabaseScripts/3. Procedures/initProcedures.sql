USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddAddress](
@city nvarchar(50),
@houseNum nvarchar(10),
@flatNum nvarchar(10)
)
AS
INSERT dbo.Addresses
VALUES(
@city,
@houseNum,
@flatNum)

GO

CREATE PROCEDURE [dbo].[AddCategory](
@categoryName nvarchar(50),
@pid int = null
)
AS
INSERT dbo.Categories
VALUES(
@categoryName,
@pid)

GO

CREATE PROCEDURE [dbo].[AddEmail](
@organizerId int,
@email nvarchar(50)
)
AS
INSERT dbo.Emails
VALUES(
@organizerId,
@email)

GO

CREATE PROCEDURE [dbo].[AddEvent](
@name nvarchar(50),
@date date,
@organizerId int,
@categoryId int,
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
GO

CREATE PROCEDURE [dbo].[AddOrganizer](
@name nvarchar(50)
)
AS
INSERT dbo.Organizers
VALUES(
@name)

GO

CREATE PROCEDURE [dbo].[AddPhone](
@organizerId int,
@phoneNumber nvarchar(20)
)
AS
INSERT dbo.Phones
VALUES(
@organizerId,
@phoneNumber)

GO

CREATE PROCEDURE [dbo].[IsValidUser]
(@name nvarchar(20),
@password nvarchar(max))
AS
IF EXISTS (SELECT Users.Id, Users.Name
  FROM [dbo].Users WHERE Name=@name and Password=@password)
  return 1
  ELSE
  return 0
GO

CREATE PROCEDURE [dbo].[SelectAddressById]
(@id int)
AS
SELECT*
  FROM [dbo].Addresses WHERE id=@id

GO

CREATE PROCEDURE [dbo].[SelectCategories]
AS
SELECT [id],
[pid]
      ,[Name]
  FROM [dbo].[Categories]

GO

CREATE PROCEDURE [dbo].[SelectEmailsByOrganizerId]
(@organizerId int)
AS
SELECT*
  FROM [dbo].Emails WHERE OrganizerId =@organizerId

GO

CREATE PROCEDURE [dbo].[SelectEventDetailInfoById](
@eventId int
)
AS
SELECT       dbo.[Events].id AS EventId,dbo.[Events].Name AS EventName,dbo.[Events].Date,dbo.[Events].ImageUrl, dbo.[Events].Description,
             dbo.Addresses.Address, dbo.Categories.Name as CategoryName,dbo.Organizers.Name AS OrganizerName,dbo.[Events].OrganizerId                       
FROM            dbo.[Events] INNER JOIN
                         dbo.Categories ON dbo.[Events].CategoryId = dbo.Categories.id INNER JOIN
                         dbo.Addresses ON dbo.[Events].AddressId = dbo.Addresses.id INNER JOIN
                         dbo.Organizers ON dbo.[Events].OrganizerId = dbo.Organizers.id
WHERE dbo.[Events].id=@eventId;


GO

CREATE PROCEDURE [dbo].[SelectEvents]
AS
SELECT*
  FROM [dbo].[Events]

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
SELECT dbo.[Events].*
FROM  (SELECT r.id
From Recursion r) r1 INNER JOIN [Events] ON r1.id = [Events].CategoryId
END
GO

CREATE PROCEDURE [dbo].[SelectOrganizerById]
(@id int)
AS
SELECT*
  FROM [dbo].Organizers WHERE id=@id

GO

CREATE PROCEDURE [dbo].[SelectOrganizers]
AS
SELECT*
  FROM [dbo].Organizers

GO

CREATE PROCEDURE [dbo].[SelectPhonesByOrganizerId]
(@organizerId int)
AS
SELECT*
  FROM [dbo].Phones WHERE OrganizerId =@organizerId

GO

CREATE PROCEDURE [dbo].[SelectRolesByUserId]
(@userId int)
AS
SELECT Roles.Name
  FROM [dbo].Roles inner join dbo.UsersRoles ON dbo.UsersRoles.RoleId=dbo.Roles.Id WHERE dbo.UsersRoles.UserId=@userId

GO

CREATE PROCEDURE [dbo].[SelectUserByName]
(@name nvarchar(20))
AS
SELECT Users.Id, Users.Name
  FROM [dbo].Users WHERE Name=@name

GO
