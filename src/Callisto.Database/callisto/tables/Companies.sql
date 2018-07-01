CREATE TABLE [callisto].[Companies](
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [TS] TIMESTAMP NOT NULL, 
	[CreatedAt] [datetime] NULL DEFAULT (GETDATE()),
	[ModifiedAt] [datetime] NULL DEFAULT (GETDATE()),
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](512) NULL,
    [Website] NVARCHAR(200) NULL, 
    [Employees] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
))

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Companies] ON [callisto].[Companies]
(
	[Id] ASC
)
GO
