CREATE TABLE [callisto].[AssetLocations](
	[AssetRefId] [bigint] NOT NULL,
	[LocationRefId] [bigint] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
 CONSTRAINT [PK_AssetLocations_1] PRIMARY KEY CLUSTERED 
(
	[AssetRefId] ASC,
	[LocationRefId] ASC
))

GO

ALTER TABLE [callisto].[AssetLocations] ADD  CONSTRAINT [FK_AssetLocations_Assets] FOREIGN KEY([AssetRefId])
REFERENCES [callisto].[Assets] ([RefId])
GO

ALTER TABLE [callisto].[AssetLocations] CHECK CONSTRAINT [FK_AssetLocations_Assets]
GO

ALTER TABLE [callisto].[AssetLocations]  ADD  CONSTRAINT [FK_AssetLocations_Locations] FOREIGN KEY([LocationRefId])
REFERENCES [callisto].[Locations] ([RefId])
GO

ALTER TABLE [callisto].[AssetLocations] CHECK CONSTRAINT [FK_AssetLocations_Locations]
GO
