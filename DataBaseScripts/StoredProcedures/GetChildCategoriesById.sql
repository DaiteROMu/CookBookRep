CREATE PROC GetChildCategoriesById
@CategoryId int
AS
SELECT * FROM Category
WHERE [Category].ParentId=@CategoryId