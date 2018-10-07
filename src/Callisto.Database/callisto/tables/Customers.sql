﻿CREATE TABLE [callisto].[Customers]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [TS] TIMESTAMP NOT NULL, 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
	[Deactivated] BIT NOT NULL DEFAULT 0, 
	[FirstName] NVARCHAR(256) NULL, 
	[LastName] NVARCHAR(256) NULL, 
	[Email] NVARCHAR(256) NULL, 
	[CompanyRefId] BIGINT NOT NULL, 
    [ParentRefId] BIGINT NULL, 
    [PictureUrl] NVARCHAR(512) NULL,
	[UserId] NVARCHAR(450) NULL,
    [Tags] NVARCHAR(1024) NULL, 
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

ALTER TABLE [callisto].[Customers] ADD  CONSTRAINT [FK_Customer_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Customers] CHECK CONSTRAINT [FK_Customer_Companies]
GO

ALTER TABLE [callisto].[Customers] ADD  CONSTRAINT [FK_Customer_Customer] FOREIGN KEY([ParentRefId])
REFERENCES [callisto].[Customers] ([RefId])
GO

ALTER TABLE [callisto].[Customers] CHECK CONSTRAINT [FK_Customer_Customer]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer_Id] ON [callisto].[Customers]
(
	[Id] ASC
)
GO
