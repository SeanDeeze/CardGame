CREATE TABLE [dbo].[Users]
(
	[ID]            UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWID() PRIMARY KEY, 
    [UserName]      VARCHAR(50)         NOT NULL, 
    [LastActivity]  DATETIMEOFFSET      NOT NULL DEFAULT SYSDATETIMEOFFSET(), 
    [Admin]         BIT                 NOT NULL DEFAULT 0, 
    [Wins]          INT                 NOT NULL DEFAULT 0 
)
