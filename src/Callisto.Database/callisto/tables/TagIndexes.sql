CREATE TABLE [callisto].[TagIndexes]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [TS] TIMESTAMP NOT NULL, 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
	[Name] NVARCHAR(1024),
    [CompanyRefId] BIGINT NULL
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Tag] ON [callisto].[TagIndexes]
(
	[Id] ASC
)
GO

ALTER TABLE [callisto].[TagIndexes] ADD  CONSTRAINT [FK_TagIndex_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[TagIndexes] CHECK CONSTRAINT [FK_TagIndex_Companies]
GO
