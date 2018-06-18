CREATE TABLE [callisto].[Vendor]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
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
    CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

ALTER TABLE [callisto].[Vendor] ADD  CONSTRAINT [FK_Vendor_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Vendor] CHECK CONSTRAINT [FK_Vendor_Companies]
GO

ALTER TABLE [callisto].[Vendor] ADD  CONSTRAINT [FK_Vendor_Vendor] FOREIGN KEY([ParentRefId])
REFERENCES [callisto].[Vendor] ([RefId])
GO

ALTER TABLE [callisto].[Vendor] CHECK CONSTRAINT [FK_Vendor_Vendor]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Vendor_Id] ON [callisto].[Vendor]
(
	[Id] ASC
)
GO

