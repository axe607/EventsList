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
GO
CREATE PROCEDURE [dbo].[AddAddress](
@address nvarchar(100)
)
AS
INSERT dbo.Addresses
VALUES(
@address)


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

GO
CREATE PROCEDURE [dbo].[AddOrganizer](
@userId int,
@name nvarchar(50)
)
AS
INSERT dbo.Organizers
VALUES(
@userId,
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
CREATE PROCEDURE [dbo].[AddRole](
@name nvarchar(20)
)
AS
INSERT dbo.Roles
VALUES(
@name)

GO
CREATE PROCEDURE [dbo].[AddUser](
@name nvarchar(20),
@password nvarchar(max),
@email nvarchar(30)
)
AS
INSERT dbo.Users
VALUES(
@name,
@password,
@email)

GO
CREATE PROCEDURE [dbo].[AddUserRole](
@userName nvarchar(20),
@roleId int
)
AS
INSERT dbo.UsersRoles
VALUES(
@roleId,
(SELECT Id FROM Users WHERE Name = @userName)
)

GO
CREATE PROCEDURE [dbo].[DeleteAddressById](
@addressId int
)
AS
DELETE FROM dbo.Addresses WHERE id = @addressId
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
GO
CREATE PROCEDURE [dbo].[DeleteEmailByUserIdAndEmailId](
@userId int,
@emailId int
)
AS
DELETE FROM dbo.Emails WHERE OrganizerId = @userId AND id = @emailId


GO
CREATE PROCEDURE [dbo].[DeleteEventById](
@eventId int
)
AS
DELETE FROM dbo.[Events] WHERE id = @eventId


GO
CREATE PROCEDURE [dbo].[DeleteFutureEventByIdAndUserId](
@eventId int,
@userId int
)
AS
DELETE FROM dbo.[Events] WHERE id = @eventId AND OrganizerId = @userId AND CONVERT(DATE,Date)  > CAST(GETDATE() AS DATE)
GO
CREATE PROCEDURE [dbo].[DeletePhoneByUserIdAndPhoneId](
@userId int,
@phoneId int
)
AS
DELETE FROM dbo.Phones WHERE OrganizerId = @userId AND id = @phoneId


GO
CREATE PROCEDURE [dbo].[DeleteRoleById](
@roleId int
)
AS
DELETE FROM dbo.Roles WHERE id = @roleId


GO
CREATE PROCEDURE [dbo].[DeleteUserById](
@userId int
)
AS
DELETE FROM dbo.Users WHERE id = @userId


GO
CREATE PROCEDURE [dbo].[DeleteUserRole](
@userName nvarchar(20),
@roleId int
)
AS
DELETE FROM dbo.UsersRoles WHERE UserId = (SELECT Id FROM Users WHERE Name = @userName) AND RoleId = @roleId


GO
CREATE PROCEDURE [dbo].[IsRoleNameFree]
(@roleId int = NULL,
@name nvarchar(20)
)
AS
IF EXISTS (SELECT Roles.Name
  FROM [dbo].Roles WHERE Name=@name  and id !=isnull(@roleId,0))
  return 0
  ELSE
  return 1

GO
CREATE PROCEDURE [dbo].[IsUserNameFree]
(@userId int,
@name nvarchar(20)
)
AS
IF EXISTS (SELECT Users.Name
  FROM [dbo].Users WHERE Name=@name and id !=@userId)
  return 0
  ELSE
  return 1

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
(@addressId int)
AS
SELECT*
  FROM [dbo].Addresses WHERE id=@addressId


GO
CREATE PROCEDURE [dbo].[SelectAddresses]
AS
SELECT id,Address
  FROM [dbo].Addresses


GO
CREATE PROCEDURE [dbo].[SelectCategories]
AS
SELECT [id],
[pid]
      ,[Name]
  FROM [dbo].[Categories]


GO
CREATE PROCEDURE [dbo].[SelectCategoryById]
(
@categoryId int
)
AS
SELECT id,pid,Name
  FROM [dbo].[Categories]
  WHERE id = @categoryId
GO
CREATE PROCEDURE [dbo].[SelectEmailsByOrganizerId]
(@organizerId int)
AS
SELECT*
  FROM [dbo].Emails WHERE OrganizerId =@organizerId


GO
CREATE PROCEDURE [dbo].[SelectEventById](
@id int
)
AS
SELECT*
  FROM [dbo].[Events]
  WHERE id=@id


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
CREATE PROCEDURE [dbo].[SelectEventsByCategoryIdAndDateAndState]
(
@categoryId int = NULL,
@date date = NULL,
@state int = NULL
)
AS 

IF @state=-1
BEGIN
SELECT * FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date) WHERE CONVERT(DATE,Date) < CAST(GETDATE() AS DATE)
END
ELSE IF @state=0
BEGIN
SELECT * FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date) WHERE CONVERT(DATE,Date)  = CAST(GETDATE() AS DATE)
END
ELSE IF @state=1
BEGIN
SELECT * FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date) WHERE CONVERT(DATE,Date)  > CAST(GETDATE() AS DATE)
END
ELSE
BEGIN
SELECT * FROM dbo.SelectByCategoryIdAndDate(@categoryId,@date)
END

GO
CREATE PROCEDURE [dbo].[SelectEventsByUserId](
@userId int
)
AS
SELECT*
  FROM [dbo].[Events]
  WHERE [dbo].[Events].OrganizerId=@userId
GO
CREATE PROCEDURE [dbo].[SelectOrganizers]
AS
SELECT Id,Name
  FROM [dbo].Organizers


GO
CREATE PROCEDURE [dbo].[SelectPhonesByOrganizerId]
(@organizerId int)
AS
SELECT*
  FROM [dbo].Phones WHERE OrganizerId =@organizerId


GO
CREATE PROCEDURE [dbo].[SelectRoleById]
(@roleId int)
AS
SELECT Roles.Id, Roles.Name
  FROM [dbo].Roles WHERE Id= @roleId


GO
CREATE PROCEDURE [dbo].[SelectRoles]
AS
SELECT Roles.Id, Roles.Name
  FROM [dbo].Roles


GO
CREATE PROCEDURE [dbo].[SelectRolesByUserId]
(@userId int)
AS
SELECT Roles.Id, Roles.Name
  FROM [dbo].Roles inner join dbo.UsersRoles ON dbo.UsersRoles.RoleId=dbo.Roles.Id WHERE dbo.UsersRoles.UserId=@userId


GO
CREATE PROCEDURE [dbo].[SelectRolesNotInUser]
(@userName nvarchar(20))
AS
SELECT DISTINCT Roles.Id, Roles.Name
  FROM [dbo].Roles LEFT JOIN dbo.UsersRoles ON dbo.UsersRoles.RoleId=dbo.Roles.Id
  WHERE dbo.UsersRoles.RoleId NOT IN (SELECT RoleId FROM UsersRoles INNER JOIN Users ON UserId=Users.id WHERE Name =@userName ) OR dbo.UsersRoles.UserId is null


GO
CREATE PROCEDURE [dbo].[SelectUserByName]
(@name nvarchar(20))
AS
SELECT Users.Id, Users.Name, Users.Email, Organizers.Name as OrganizerName
  FROM [dbo].Users
  INNER JOIN dbo.Organizers ON dbo.Users.id=dbo.Organizers.id WHERE Users.Name=@name


GO
CREATE PROCEDURE [dbo].[SelectUsers]
AS
SELECT Users.Id, Users.Name, Users.Email, Organizers.Name as OrganizerName
  FROM [dbo].Users
  INNER JOIN dbo.Organizers ON dbo.Users.id=dbo.Organizers.id
GO
CREATE PROCEDURE [dbo].[UpdateAddress](
@addressId int,
@address nvarchar(100) = NULL
)
AS
UPDATE dbo.Addresses
SET Address = isnull(@address,Address)
WHERE id = @addressId
GO
CREATE PROCEDURE [dbo].[UpdateCategory](
@categoryId int,
@pid int = NULL,
@categoryName nvarchar(50) = NULL
)
AS
UPDATE dbo.Categories
SET Name = isnull(@categoryName,Name),
    pid = @pid
WHERE id = @categoryId
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

GO
CREATE PROCEDURE [dbo].[UpdateEventByUserId](
@eventId int,
@userId int,
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
WHERE id = @eventId AND OrganizerId=@userId

GO
CREATE PROCEDURE [dbo].[UpdateOrganizerInfo](
@organizerId int,
@name nvarchar(20) = NULL
)
AS
UPDATE dbo.Organizers
SET Name = isnull(@name,Name)
WHERE id = @organizerId

GO
CREATE PROCEDURE [dbo].[UpdateRole](
@roleId int,
@name nvarchar(20) = NULL
)
AS
UPDATE dbo.Roles
SET Name = isnull(@name,Name)
WHERE id = @roleId

GO
CREATE PROCEDURE [dbo].[UpdateUserInfo](
@userId int,
@name nvarchar(20) = NULL,
@email nvarchar(30) = NULL
)
AS
UPDATE dbo.Users
SET Name = isnull(@name,Name),
Email = isnull(@email,Email)
WHERE id = @userId