USE [EventsListDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[InsUserToOrgTrig] ON [dbo].[Users] FOR INSERT AS
INSERT dbo.Organizers
VALUES(
(SELECT id FROM inserted),
NULL
)