CREATE TABLE Category
	(
		CategoryId int IDENTITY,
		Name nvarchar(50) NOT NULL,
		ParentId int NULL,
		PRIMARY KEY (CategoryId)
	)