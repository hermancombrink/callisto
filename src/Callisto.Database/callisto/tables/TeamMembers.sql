CREATE TABLE [callisto].[TeamMembers]
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
    [IsFounder] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)) 
GO

ALTER TABLE [callisto].[TeamMembers] ADD  CONSTRAINT [FK_Team_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[TeamMembers] CHECK CONSTRAINT [FK_Team_Companies]
GO

ALTER TABLE [callisto].[TeamMembers] ADD  CONSTRAINT [FK_Team_Team] FOREIGN KEY([ParentRefId])
REFERENCES [callisto].[TeamMembers] ([RefId])
GO

ALTER TABLE [callisto].[TeamMembers] CHECK CONSTRAINT [FK_Team_Team]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Team_Id] ON [callisto].[TeamMembers]
(
	[Id] ASC
)
GO

