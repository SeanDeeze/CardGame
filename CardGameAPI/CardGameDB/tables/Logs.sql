CREATE TABLE [dbo].[Logs]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Level] VARCHAR(25) NULL, 
    [Logger] VARCHAR(50) NULL, 
    [Message] VARCHAR(MAX) NULL, 
    [TimeStamp] DATETIME NULL
)
