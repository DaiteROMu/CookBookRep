CREATE PROC DeleteRecipeIngridient
@RecipeId int,
@IngridientId int
AS
DELETE FROM RecipeIngridient
WHERE [RecipeIngridient].RecipeId=@RecipeId AND [RecipeIngridient].IngridientId=@IngridientId