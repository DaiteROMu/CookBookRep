CREATE PROC UpdateRecipe
@RecipeId int,
@Name nvarchar(100),
@ImageUrl nvarchar(max),
@CategoryId int
AS
UPDATE Recipe
SET Name=@Name, ImageUrl=@ImageUrl, CategoryId=@CategoryId
WHERE [Recipe].RecipeId=@RecipeId