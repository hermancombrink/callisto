CREATE TABLE [callisto].[Assets]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
	[AssetNumber] NVARCHAR(256) NULL, 
	[Name] NVARCHAR(256) NULL, 
    [Description] NVARCHAR(512) NULL,
	[CompanyRefId] BIGINT NOT NULL, 
    [ParentRefId] BIGINT NULL, 
    [PictureUrl] NVARCHAR(512) NULL
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

ALTER TABLE [callisto].[Assets] ADD  CONSTRAINT [FK_Assets_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Assets] CHECK CONSTRAINT [FK_Assets_Companies]
GO

ALTER TABLE [callisto].[Assets] ADD  CONSTRAINT [FK_Assets_Assets] FOREIGN KEY([ParentRefId])
REFERENCES [callisto].[Assets] ([RefId])
GO

ALTER TABLE [callisto].[Assets] CHECK CONSTRAINT [FK_Assets_Assets]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Asset_Id] ON [callisto].[Assets]
(
	[Id] ASC
)
GO

