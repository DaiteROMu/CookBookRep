CREATE TABLE RecipeDetails
	(
		RecipeId int NOT NULL,
		CookingTime nvarchar(50) NULL,
		CookingTemperature nvarchar(50) NULL,
		Description nvarchar(200) NULL,
		Sequencing nvarchar(max) NULL,
		PRIMARY KEY (RecipeId),
		FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId) ON DELETE CASCADE
	)