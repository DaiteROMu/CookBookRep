CREATE PROC InsertRecipeIngridient
@RecipeId int,
@IngridientId int,
@Weight nvarchar(100)
AS
INSERT INTO RecipeIngridient(RecipeId,IngridientId,Weight)
VALUES (@RecipeId,@IngridientId,@Weight)