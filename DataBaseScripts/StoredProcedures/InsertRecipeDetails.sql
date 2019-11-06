CREATE PROC InsertRecipeDetails
@CookingTime nvarchar(50),
@CookingTemperature nvarchar(50),
@Description nvarchar(200),
@Sequencing nvarchar(max)
AS
INSERT INTO RecipeDetails(RecipeId,CookingTime, CookingTemperature, Description, Sequencing)
VALUES ((SELECT ident_current('Recipe')),@CookingTime,@CookingTemperature,@Description,@Sequencing)