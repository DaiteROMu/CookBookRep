CREATE PROC UpdateRecipeIngridient
@RecipeId int,
@IngridientId int,
@Weight nvarchar(100)
AS
UPDATE RecipeIngridient
SET Weight=@Weight
WHERE [RecipeIngridient].RecipeId=@RecipeId AND [RecipeIngridient].IngridientId=@IngridientId