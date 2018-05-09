CREATE TABLE [callisto].[Locations](
	[RefId] [bigint] IDENTITY(1,1) NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
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
	[CountryCode] [nvarchar](16) NULL,
	[StateCode] [nvarchar](16) NULL,
	[UTCOffsetMinutes] [int] NULL,
	[GooglePlaceId] [nvarchar](50) NULL,
	[GoogleURL] [nvarchar](2056) NULL,

    CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Locations] ON [callisto].[Locations]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [callisto].[Locations] ADD  CONSTRAINT [FK_Locations_Companies] FOREIGN KEY([CompanyRefId])
REFERENCES [callisto].[Companies] ([RefId])
GO

ALTER TABLE [callisto].[Locations] CHECK CONSTRAINT [FK_Locations_Companies]
GO

