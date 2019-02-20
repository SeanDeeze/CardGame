CREATE TABLE [dbo].[Cards]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Description] NTEXT NULL, 
    [ReputationPoints] INT NOT NULL DEFAULT 0, 
    [Gold] INT NOT NULL DEFAULT 0, 
    [Image] VARCHAR(50) NULL 
)
