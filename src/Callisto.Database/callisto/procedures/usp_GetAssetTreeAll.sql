CREATE PROCEDURE [callisto].[usp_GetAssetTreeAll]
	@CompanyRefId bigint
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
       assets.RefId, 
       assets.Id, 
       parent.Id as ParentId,
       assets.AssetNumber, 
       assets.Name, 
       assets.Description,
       assets.CompanyRefId, 
       (SELECT COUNT(1) FROM callisto.Assets childcount WHERE assets.RefId = childcount.ParentRefId) AS Children
	FROM callisto.Assets assets WITH (NOLOCK)
	LEFT JOIN  callisto.Assets parent WITH (NOLOCK) ON assets.ParentRefId = parent.RefId
	WHERE assets.CompanyRefId = @CompanyRefId
END