CREATE TABLE [dbo].[CardRoles]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [DiceNumber] INT NOT NULL DEFAULT 0
)
