CREATE TABLE [callisto].[Assets]
(
	[RefId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(),
	[AssetNumber] NVARCHAR(256) NULL, 
	[Name] NVARCHAR(256) NULL, 
    [Description] NVARCHAR(512) NULL,
	[CompanyRefId] BIGINT NOT NULL
)
GO

ALTER TABLE [callisto].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_Assets_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Assets] CHECK CONSTRAINT [FK_Assets_Companies]
GO