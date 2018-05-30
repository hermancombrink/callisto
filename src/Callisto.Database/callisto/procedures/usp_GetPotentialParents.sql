CREATE PROCEDURE callisto.usp_GetPotentialParents
	@CompanyRefId bigint, 
	@RefId bigint
AS
BEGIN
SET NOCOUNT ON;

;With Parents(RefId, ParentRefId, Level, levels) AS 
(
  SELECT 
   RefId, 
   ParentRefId, 
   0, 
   CAST(LTRIM(STR(RefId,8,0)) AS VARCHAR(MAX))
  FROM  callisto.Assets WITH (NOLOCK)
  WHERE ParentRefId is null
  AND CompanyRefId = @CompanyRefId
  UNION All
  SELECT 
   p.RefId, 
   p.ParentRefId,  
   par.Level + 1,  
   CAST( levels + ', ' + LTRIM(STR(p.RefId,8,0)) AS VARCHAR(MAX))
  FROM  callisto.Assets p WITH (NOLOCK)
  INNER JOIN Parents par ON par.RefId = p.ParentRefId
  WHERE p.CompanyRefId = @CompanyRefId
)
  SELECT 
	assets.RefId, 
	assets.Id,
	assets.AssetNumber, 
	assets.Name, 
	assets.Description,
	assets.CompanyRefId,
	parent.Id as ParentId
  FROM Parents
  INNER JOIN callisto.Assets assets WITH (NOLOCK) ON Parents.RefId = assets.RefId
  LEFT JOIN  callisto.Assets parent WITH (NOLOCK) ON assets.ParentRefId = parent.RefId
  WHERE levels NOT LIKE CAST(@RefId AS VARCHAR(20)) +',%'
  AND Parents.RefId != @RefId
  ORDER BY Level
END
GO