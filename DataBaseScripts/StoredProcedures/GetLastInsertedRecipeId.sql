CREATE PROC GetLastInsertedRecipeId
AS
SELECT ident_current('Recipe')