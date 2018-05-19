CREATE PROCEDURE [callisto].[usp_GetAssetTreeAll]
	@CompanyRefId bigint
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	assets.RefId, 
	assets.Id, 
	(SELECT parent.Id from callisto.Assets parent WITH (NOLOCK) WHERE assets.ParentRefId = parent.RefId) as ParentId,
	assets.AssetNumber, 
	assets.Name, 
	assets.Description,
	assets.CompanyRefId, 
	(SELECT COUNT(1) FROM callisto.Assets childcount WHERE assets.RefId = childcount.ParentRefId) AS Children
	FROM callisto.Assets assets WITH (NOLOCK)
	WHERE  assets.CompanyRefId = @CompanyRefId
END