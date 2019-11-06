CREATE PROC GetRecipesByIngridientName
@ingridientName nvarchar(50)
AS
SELECT [Recipe].RecipeId, [Recipe].Name, [Recipe].ImageUrl, [Recipe].CategoryId, [Category].Name as CategoryName FROM (((Recipe inner join Category on [Recipe].CategoryId=[Category].CategoryId) inner join RecipeIngridient on [Recipe].RecipeId=[RecipeIngridient].RecipeId) inner join Ingridient on [RecipeIngridient].IngridientId=[Ingridient].IngridientId)
WHERE CHARINDEX(@ingridientName, [Ingridient].Name)>0