USE [master]
GO
IF db_id('SpyDuh') IS NULL
  CREATE DATABASE [SpyDuh]
GO
USE [SpyDuh]
GO

DROP TABLE IF EXISTS [Spy];
DROP TABLE IF EXISTS [Enemy];
DROP TABLE IF EXISTS [Services];
DROP TABLE IF EXISTS [Skills];
DROP TABLE IF EXISTS [Friend];

CREATE TABLE [spy] (
  [id] int IDENTIFY PRIMARY KEY,
  [name] nvarchar(255),
  [bio] nvarchar(255)
)
GO

CREATE TABLE [spySkills] (
  [id] int IDENTIFY PRIMARY KEY,
  [skillName] nvarchar(255),
  [skillLevel] int,
  [spyId] int
)
GO

CREATE TABLE [spyServices] (
  [id] int IDENTIFY PRIMARY KEY,
  [serviceName] nvarchar(255),
  [price] int,
  [spyId] int
)
GO

CREATE TABLE [friend] (
  [Id] int IDENTIFY PRIMARY KEY,
  [spyId] int,
  [friendId] int
)
GO

CREATE TABLE [enemy] (
  [Id] int IDENTIFY PRIMARY KEY,
  [spyId] int,
  [enemyId] int
)
GO

ALTER TABLE [enemy] ADD FOREIGN KEY ([spyId]) REFERENCES [spy] ([id])
GO

ALTER TABLE [friend] ADD FOREIGN KEY ([spyId]) REFERENCES [spy] ([id])
GO

ALTER TABLE [enemy] ADD FOREIGN KEY ([enemyId]) REFERENCES [spy] ([id])
GO

ALTER TABLE [friend] ADD FOREIGN KEY ([friendId]) REFERENCES [spy] ([id])
GO

ALTER TABLE [spyServices] ADD FOREIGN KEY ([spyId]) REFERENCES [spy] ([id])
GO

ALTER TABLE [spySkills] ADD FOREIGN KEY ([spyId]) REFERENCES [spy] ([id])
GO
