CREATE PROCEDURE [callisto].[usp_GetAssetTree]
	@CompanyRefId bigint,
	@RefId bigint = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
	assets.RefId, 
	assets.Id, 
	assets.AssetNumber, 
	assets.Name, 
	assets.Description,
	assets.CompanyRefId, 
	(SELECT COUNT(1) FROM callisto.Assets childcount WHERE assets.RefId = childcount.ParentRefId) AS Children
	FROM callisto.Assets assets WITH (NOLOCK)
	WHERE ISNULL(ParentRefId, 0) = ISNULL(@RefId, 0)
	AND assets.CompanyRefId = @CompanyRefId
END	
