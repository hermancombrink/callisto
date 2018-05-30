CREATE TABLE [callisto].[Staff]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
	[FirstName] NVARCHAR(256) NULL, 
	[LastName] NVARCHAR(256) NULL, 
	[Email] NVARCHAR(256) NULL, 
	[CompanyRefId] BIGINT NOT NULL, 
    [ParentRefId] BIGINT NULL, 
    [PictureUrl] NVARCHAR(512) NULL
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

ALTER TABLE [callisto].[Staff] ADD  CONSTRAINT [FK_Staff_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Staff] CHECK CONSTRAINT [FK_Staff_Companies]
GO

ALTER TABLE [callisto].[Staff] ADD  CONSTRAINT [FK_Staff_Staff] FOREIGN KEY([ParentRefId])
REFERENCES [callisto].[Staff] ([RefId])
GO

ALTER TABLE [callisto].[Staff] CHECK CONSTRAINT [FK_Staff_Staff]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Staff_Id] ON [callisto].[Staff]
(
	[Id] ASC
)
GO

