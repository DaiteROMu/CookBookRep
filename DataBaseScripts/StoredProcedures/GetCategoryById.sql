CREATE PROC GetCategoryById
@CategoryId int
AS
SELECT * FROM Category
WHERE [Category].CategoryId=@CategoryId