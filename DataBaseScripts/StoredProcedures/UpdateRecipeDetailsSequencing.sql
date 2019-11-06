CREATE PROC UpdateRecipeDetailsSequencing
@RecipeId int,
@Sequencing nvarchar(max)
AS
UPDATE RecipeDetails
SET Sequencing=@Sequencing
WHERE [RecipeDetails].RecipeId=@RecipeId