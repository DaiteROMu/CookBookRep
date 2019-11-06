CREATE PROC GetRecipeDetailsById
@RecipeId int
AS
SELECT * FROM RecipeDetails
WHERE [RecipeDetails].RecipeId=@RecipeId