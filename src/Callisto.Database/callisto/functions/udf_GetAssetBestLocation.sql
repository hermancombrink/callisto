
CREATE FUNCTION [callisto].[udf_GetAssetBestLocation]
(	
	@RefId bigint
)
RETURNS @BestLocation TABLE
    (
	[RefId] [bigint] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
	[CompanyRefId] [bigint] NOT NULL,
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
	[GoogleURL] [nvarchar](2056) NULL
    )
AS
BEGIN

	if(EXISTS(select 1  
	from callisto.Assets a
	inner join callisto.AssetLocations al on a.refid = al.assetrefid
	inner join callisto.Locations l on al.LocationRefId = l.refId
	where a.RefId = @RefId))
	BEGIN
	insert into @BestLocation
	select 
		l.[RefId],
		l.[Id],
		l.[CreatedAt],
		l.[ModifiedAt],
		l.[CompanyRefId],
		l.[Latitude],
		l.[Longitude],
		l.[FormattedAddress],
		l.[Route],
		l.[Vicinity],
		l.[City],
		l.[State],
		l.[Country],
		l.[PostCode],
		l.[CountryCode],
		l.[StateCode],
		l.[UTCOffsetMinutes],
		l.[GooglePlaceId],
		l.[GoogleURL] 
	from callisto.Assets a
	inner join callisto.AssetLocations al on a.refid = al.assetrefid
	inner join callisto.Locations l on al.LocationRefId = l.refId
	where a.RefId = @RefId
	END
	ELSE BEGIN
	declare @parentRefId bigint = (
		select a.ParentRefId
		from callisto.Assets a
		where a.RefId = @RefId
	)
	    if(Coalesce(@parentRefId, 0) > 0)
		insert into @BestLocation
		select * from [callisto].[udf_GetAssetBestLocation](@parentRefId)
	END
	RETURN 
END
