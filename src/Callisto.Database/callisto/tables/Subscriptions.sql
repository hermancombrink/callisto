CREATE TABLE [callisto].[Subscriptions]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
    [CompanyRefId] BIGINT NULL
 CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Subscriptions] ON [callisto].[Subscriptions]
(
	[Id] ASC
)
GO