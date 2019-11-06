CREATE PROC UpdateCategoryParentId
@CategoryId int,
@ParentId int
AS
UPDATE Category
SET ParentId=@ParentId
WHERE [Category].CategoryId=@CategoryId