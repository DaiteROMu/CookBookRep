CREATE PROC GetRecipesByCategoryId
@CategoryId int
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId
WHERE [Recipe].CategoryId=@CategoryId