CREATE PROC InsertRecipe
@Name nvarchar(100),
@ImageUrl nvarchar(max),
@CategoryId int
AS
INSERT INTO Recipe(Name,ImageUrl,CategoryId)
VALUES (@Name,@ImageUrl,@CategoryId)