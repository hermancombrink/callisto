CREATE TABLE [callisto].[Locations](
	[RefId] [bigint] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[Latitude] [decimal](18, 14) NULL,
	[Longitude] [decimal](18, 14) NULL,
	[Address] [nvarchar](1024) NULL,
	[City] [nvarchar](256) NULL,
	[Country] [nvarchar](256) NULL,
	[State] [nvarchar](256) NULL,
	[PostCode] [nvarchar](64) NULL,
	[CountryCode] [nvarchar](16) NULL,
	[StateCode] [nvarchar](16) NULL,
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


