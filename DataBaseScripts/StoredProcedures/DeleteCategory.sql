CREATE PROC DeleteCategory
@CategoryId int
AS
DELETE FROM Category
WHERE [Category].CategoryId=@CategoryId