CREATE PROC GetRecipeIngridient
@RecipeId int,
@IngridientId int
AS
SELECT [RecipeIngridient].RecipeId, [RecipeIngridient].IngridientId, [Ingridient].Name, [RecipeIngridient].Weight FROM RecipeIngridient inner join Ingridient on [RecipeIngridient].IngridientId=[Ingridient].IngridientId
WHERE [RecipeIngridient].RecipeId=@RecipeId AND [RecipeIngridient].IngridientId=@IngridientId