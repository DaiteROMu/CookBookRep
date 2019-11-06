CREATE PROC DeleteRecipe
@RecipeId int
AS
DELETE FROM Recipe
WHERE [Recipe].RecipeId=@RecipeId