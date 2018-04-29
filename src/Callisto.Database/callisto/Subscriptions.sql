﻿CREATE TABLE [callisto].[Subscriptions]
(
	[RefId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
    [CompanyRefId] BIGINT NULL)
