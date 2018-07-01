CREATE TABLE [callisto].[Subscriptions]
(
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [TS] TIMESTAMP NOT NULL, 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
    [CompanyRefId] BIGINT NULL,
	[UserId] [nvarchar](450) NULL,
    [JobRole] NVARCHAR(50) NULL, 
	[UserType] INT NOT NULL DEFAULT(0), 
    [Deactivated] BIT NOT NULL DEFAULT 0, 

 [LastLogin] DATETIME NULL, 
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

ALTER TABLE [callisto].[Subscriptions] ADD  CONSTRAINT [FK_Subscriptions_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_Companies]
GO

ALTER TABLE [callisto].[Subscriptions] ADD  CONSTRAINT [FK_Subscriptions_[AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [callisto].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_[AspNetUsers]
GO