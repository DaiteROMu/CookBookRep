CREATE PROC GetRecipeById
@RecipeId int
AS
SELECT * FROM Recipe
WHERE [Recipe].RecipeId=@RecipeId