CREATE PROC GetRecipesByCategoryName
@categoryName nvarchar(50)
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId
WHERE CHARINDEX(@categoryName, [Category].Name)>0