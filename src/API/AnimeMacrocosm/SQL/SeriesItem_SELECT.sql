SELECT se.SeriesId 
, se.Title
, ca.Id 'CreatorAuthorId'
, ca.FirstName
, ca.LastName
, si.Id 'SeriesItemId'
, si.SeriesId
, si.Title
, si.Description
, si.ProductionId
, ps.ProductionStudioName
, si.DistributorId
, di.DistributorName
, si.CreatorAuthorId
, si.FormatId
, fo.FormatName
FROM Series se
INNER JOIN SeriesCreators sc ON sc.SeriesId = se.SeriesId
INNER JOIN CreatorAuthors ca ON ca.Id = sc.CreatorId
INNER JOIN SeriesItems si ON si.SeriesId = se.SeriesId
INNER JOIN ProductionStudios ps ON ps.Id = si.ProductionId
INNER JOIN Distributors di ON di.Id = si.DistributorId
INNER JOIN Formats fo ON fo.FormatId = si.FormatId
ORDER BY se.Title