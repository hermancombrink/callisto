﻿CREATE TABLE [callisto].[Companies]
(
	[RefId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(),
	[Name] NVARCHAR(256) NULL, 
    [Description] NVARCHAR(512) NULL,
)
