CREATE TABLE RecipeIngridient
	(
		RecipeId int NOT NULL,
		IngridientId int NOT NULL,
		Weight nvarchar(100) NOT NULL,
		PRIMARY KEY (RecipeId, IngridientId),
		FOREIGN KEY (RecipeId) REFERENCES Recipe (RecipeId) ON DELETE CASCADE,
		FOREIGN KEY (IngridientId) REFERENCES Ingridient (IngridientId) ON DELETE CASCADE
	)