USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateAddress](
@addressId int,
@address nvarchar(100) = NULL
)
AS
UPDATE dbo.Addresses
SET Address = isnull(@address,Address)
WHERE id = @addressId