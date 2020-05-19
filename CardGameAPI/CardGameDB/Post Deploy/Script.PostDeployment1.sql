/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[CardRoles] ON 
GO
INSERT [dbo].[CardRoles] ([Id], [Name], [DiceNumber]) VALUES (1, N'Warrior', 1)
GO
INSERT [dbo].[CardRoles] ([Id], [Name], [DiceNumber]) VALUES (2, N'Thief', 2)
GO
INSERT [dbo].[CardRoles] ([Id], [Name], [DiceNumber]) VALUES (4, N'Hunter', 3)
GO
INSERT [dbo].[CardRoles] ([Id], [Name], [DiceNumber]) VALUES (5, N'Cleric', 4)
GO
INSERT [dbo].[CardRoles] ([Id], [Name], [DiceNumber]) VALUES (6, N'Mage', 5)
GO
INSERT [dbo].[CardRoles] ([Id], [Name], [DiceNumber]) VALUES (7, N'Bard', 6)
GO
SET IDENTITY_INSERT [dbo].[CardRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Cards] ON 
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (8, N'Orc Warlord', N'Jade Dagger', 9, 900, N'orcwarlord.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (13, N'Vampire Coven', N'Vorpal Sword', 8, 900, N'vampirecoven.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (14, N'Unicorn', N'Singing Sword', 7, 700, N'unicorn.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (15, N'Mind Flayers', N'Staff of Binding', 9, 900, N'mindflayers.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (16, N'Frost Dragon', N'Helm of Vanishing', 9, 900, N'frostdragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (17, N'Fire Demon', N'Dagger of Death', 9, 900, N'firedemon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (18, N'Necromancer', N'Staff of Darkness', 11, 1000, N'necromancer.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (19, N'Lich', N'Staff of Earthquakes', 11, 900, N'lich.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (20, N'Giant Wyrm', N'Ruby Armor', 9, 900, N'giantwyrm.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (21, N'Death Knight', N'Sword of Souls', 8, 800, N'deathknight.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (22, N'Red Dragon', N'Helm of Lightning', 9, 900, N'reddragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (23, N'Black Dragon', N'Armor of Light', 9, 900, N'blackdragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (24, N'Silver Dragon', N'Quake Gauntlets', 9, 900, N'silverdragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (25, N'Sphinx', N'Gauntlets of Magic', 8, 800, N'sphinx.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (26, N'Gold Dragon', N'Boots of Soaring', 9, 1000, N'golddragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (27, N'Swamp Ent', N'Boots of Silence', 8, 800, N'swampent.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (28, N'Hydra Dragon', N'Pearson''s Drum', 13, 1000, N'hydradragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (29, N'Platinum Dragon', N'Garcin''s Lute', 13, 1000, N'platinumdragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (30, N'The Celestial', N'Martuk''s Flute', 14, 1500, N'thecelestial.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (31, N'Shire Dragon', N'Maida''s Chalice', 11, 1000, N'shiredragon.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (32, N'Ghoul Parade', NULL, 8, 800, N'ghoulparade.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (33, N'Stone Giant', NULL, 7, 1000, N'stonegiant.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (34, N'Dark Wizard', NULL, 6, 800, N'darkwizard.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (35, N'Frost Giant', NULL, 8, 1000, N'frostgiant.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (36, N'Merman Raiders', NULL, 4, 500, N'mermanraiders.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (37, N'Orc Raiders', NULL, 5, 600, N'orcraiders.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (38, N'Griffin Nest', NULL, 7, 800, N'griffennest.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (39, N'Goblin Horde', NULL, 5, 600, N'goblinhorde.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (40, N'Spider Queen', NULL, 8, 1500, N'spiderqueen.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (41, N'Ancient Cult', NULL, 8, 2000, N'ancientcult.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (42, N'Bog Witch', NULL, 6, 800, N'bogwitch.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (43, N'Fire Giant', NULL, 8, 1000, N'firegiant.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (44, N'Werewolf Tribe', NULL, 8, 800, N'werewolftribe.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (45, N'Halfling Assassin', NULL, 4, 600, N'halflingassassin.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (46, N'Hydra', NULL, 7, 700, N'hydra.png')
GO
INSERT [dbo].[Cards] ([Id], [Name], [Description], [ReputationPoints], [Gold], [Image]) VALUES (47, N'Dwarf Warlord', NULL, 8, 1000, N'dwarfwarlord.png')
GO
SET IDENTITY_INSERT [dbo].[Cards] OFF
GO
SET IDENTITY_INSERT [dbo].[CardsWithRoles] ON 
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (3, 1, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (4, 1, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (5, 1, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (6, 1, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (7, 1, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (8, 1, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (9, 1, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (10, 1, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (11, 1, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (12, 1, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (13, 1, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (14, 2, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (15, 2, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (16, 2, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (17, 2, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (18, 5, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (19, 5, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (20, 5, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (21, 5, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (22, 12, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (23, 12, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (24, 12, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (25, 12, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (26, 12, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (27, 12, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (99, 8, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (100, 8, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (101, 8, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (102, 8, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (103, 8, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (104, 8, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (105, 13, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (106, 13, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (107, 13, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (108, 13, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (109, 13, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (110, 14, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (111, 14, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (112, 14, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (113, 14, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (114, 14, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (115, 14, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (116, 15, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (117, 15, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (118, 15, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (119, 15, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (120, 15, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (121, 15, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (122, 16, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (123, 16, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (124, 16, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (125, 16, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (126, 16, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (127, 16, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (128, 17, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (129, 17, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (130, 17, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (131, 17, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (132, 17, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (133, 17, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (134, 18, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (135, 18, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (136, 18, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (137, 18, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (138, 18, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (139, 18, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (140, 19, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (141, 19, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (142, 19, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (143, 19, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (144, 19, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (145, 19, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (146, 20, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (147, 20, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (148, 20, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (149, 20, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (150, 20, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (151, 20, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (152, 21, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (153, 21, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (154, 21, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (155, 21, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (156, 21, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (157, 22, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (158, 22, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (159, 22, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (160, 22, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (161, 22, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (162, 22, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (163, 23, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (164, 23, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (165, 23, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (166, 23, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (167, 23, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (168, 23, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (169, 24, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (170, 24, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (171, 24, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (172, 24, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (173, 24, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (174, 24, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (175, 25, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (176, 25, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (177, 25, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (178, 25, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (179, 25, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (180, 25, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (181, 26, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (182, 26, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (183, 26, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (184, 26, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (185, 26, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (186, 26, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (187, 27, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (188, 27, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (189, 27, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (190, 27, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (191, 27, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (192, 27, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (193, 28, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (194, 28, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (195, 28, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (196, 28, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (197, 28, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (198, 28, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (199, 29, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (200, 29, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (201, 29, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (202, 29, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (203, 29, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (204, 29, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (205, 30, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (206, 30, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (207, 30, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (208, 30, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (209, 30, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (210, 30, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (211, 31, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (212, 31, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (213, 31, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (214, 31, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (215, 31, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (216, 31, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (217, 32, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (218, 32, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (219, 32, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (220, 32, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (221, 32, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (222, 32, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (223, 33, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (224, 33, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (225, 33, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (226, 33, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (227, 33, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (228, 34, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (229, 34, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (230, 34, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (231, 34, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (232, 34, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (233, 35, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (234, 35, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (235, 35, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (236, 35, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (237, 35, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (238, 35, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (243, 36, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (244, 36, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (245, 36, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (246, 36, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (247, 37, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (248, 37, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (249, 37, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (250, 37, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (251, 37, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (264, 38, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (265, 38, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (266, 38, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (267, 38, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (268, 38, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (269, 38, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (270, 39, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (271, 39, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (272, 39, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (273, 39, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (274, 40, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (275, 40, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (276, 40, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (277, 40, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (278, 40, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (279, 40, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (280, 41, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (281, 41, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (282, 41, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (283, 41, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (284, 41, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (285, 41, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (286, 42, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (287, 42, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (288, 42, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (289, 42, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (290, 42, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (291, 43, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (292, 43, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (293, 43, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (294, 43, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (295, 43, 6)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (296, 43, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (297, 44, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (298, 44, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (299, 44, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (300, 44, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (301, 44, 5)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (302, 44, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (303, 45, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (304, 45, 2)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (305, 45, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (306, 45, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (307, 46, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (308, 46, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (309, 46, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (310, 46, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (311, 46, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (312, 47, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (313, 47, 1)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (314, 47, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (315, 47, 4)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (316, 47, 7)
GO
INSERT [dbo].[CardsWithRoles] ([Id], [CardId], [CardRoleId]) VALUES (317, 47, 7)
GO
SET IDENTITY_INSERT [dbo].[CardsWithRoles] OFF
GO

INSERT [dbo].[Players] (UserName, Admin, Wins) VALUES ('SDEEZE', 1, 0);
INSERT [dbo].[Players] (UserName, Admin, Wins) VALUES ('Grayve', 1, 0);
GO
