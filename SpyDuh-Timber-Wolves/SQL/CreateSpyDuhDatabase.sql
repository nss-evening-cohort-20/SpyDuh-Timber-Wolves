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
  [id] int PRIMARY KEY,
  [name] nvarchar(255),
  [skills] int,
  [services] int,
  [bio] nvarchar(255)
)
GO

CREATE TABLE [spySkills] (
  [id] int PRIMARY KEY,
  [skillId] int,
  [spyId] int
)
GO

CREATE TABLE [spyServices] (
  [id] int PRIMARY KEY,
  [serviceId] int,
  [spyId] int
)
GO

CREATE TABLE [skill] (
  [id] int PRIMARY KEY,
  [skillName] nvarchar(255),
  [skillLevel] int
)
GO

CREATE TABLE [service] (
  [id] int PRIMARY KEY,
  [serviceName] nvarchar(255),
  [price] int
)
GO

CREATE TABLE [friend] (
  [Id] int PRIMARY KEY,
  [spyId] int,
  [friendId] int
)
GO

CREATE TABLE [enemy] (
  [Id] int PRIMARY KEY,
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

ALTER TABLE [spySkills] ADD FOREIGN KEY ([skillId]) REFERENCES [skill] ([id])
GO

ALTER TABLE [spyServices] ADD FOREIGN KEY ([serviceId]) REFERENCES [service] ([id])
GO

ALTER TABLE [spy] ADD FOREIGN KEY ([skills]) REFERENCES [spySkills] ([id])
GO

ALTER TABLE [spy] ADD FOREIGN KEY ([services]) REFERENCES [spyServices] ([id])
GO
