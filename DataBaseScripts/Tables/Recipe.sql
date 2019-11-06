CREATE TABLE Recipe
	(
		RecipeId int IDENTITY,
		Name nvarchar(100) NOT NULL,
		ImageUrl nvarchar(max) NULL,
		CategoryId int NOT NULL,
		PRIMARY KEY (RecipeId),
		FOREIGN KEY (CategoryId) REFERENCES Category (CategoryId) ON DELETE CASCADE
	)