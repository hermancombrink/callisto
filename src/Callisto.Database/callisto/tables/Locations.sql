CREATE TABLE [callisto].[Locations](
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [TS] TIMESTAMP NOT NULL, 
    [CreatedAt] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedAt] DATETIME NULL DEFAULT GETDATE(), 
	[CompanyRefId] BIGINT NOT NULL, 
	[Latitude] [decimal](18, 14) NULL,
	[Longitude] [decimal](18, 14) NULL,
	[FormattedAddress] [nvarchar](2056) NULL,
	[Route] [nvarchar](256) NULL,
	[Vicinity] [nvarchar](256) NULL,
	[City] [nvarchar](256) NULL,
	[State] [nvarchar](256) NULL,
	[Country] [nvarchar](256) NULL,
	[PostCode] [nvarchar](64) NULL,
	[CountryCode] [nvarchar](32) NULL,
	[StateCode] [nvarchar](32) NULL,
	[UTCOffsetMinutes] [int] NULL,
	[GooglePlaceId] [nvarchar](100) NULL,
	[GoogleURL] [nvarchar](2056) NULL,

    [Tags] NVARCHAR(1024) NULL, 
    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
))

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Locations] ON [callisto].[Locations]
(
	[Id] ASC
)
GO

ALTER TABLE [callisto].[Locations] ADD  CONSTRAINT [FK_Locations_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Locations] CHECK CONSTRAINT [FK_Locations_Companies]
GO

