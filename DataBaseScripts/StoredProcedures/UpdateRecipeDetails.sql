CREATE PROC UpdateRecipeDetails
@RecipeId int,
@CookingTime nvarchar(50),
@CookingTemperature nvarchar(50),
@Description nvarchar(200),
@Sequencing nvarchar(max)
AS
UPDATE RecipeDetails
SET CookingTime=@CookingTime, CookingTemperature=@CookingTemperature, Description=@Description, Sequencing=@Sequencing
WHERE [RecipeDetails].RecipeId=@RecipeId