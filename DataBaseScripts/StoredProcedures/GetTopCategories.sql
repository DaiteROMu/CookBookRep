CREATE PROC GetTopCategories
AS
SELECT * FROM Category
WHERE [Category].ParentId IS NULL