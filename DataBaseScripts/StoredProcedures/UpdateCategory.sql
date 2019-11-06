CREATE PROC UpdateCategory
@CategoryId int,
@Name nvarchar(50),
@ParentId int
AS
UPDATE Category
SET Name=@Name, ParentId=@ParentId
WHERE [Category].CategoryId=@CategoryId