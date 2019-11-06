CREATE TABLE Users
	(
		UserId int IDENTITY,
		Login nvarchar(50) NOT NULL,
		Password nvarchar(20) NOT NULL,
		Email nvarchar(100) NOT NULL				
		PRIMARY KEY (UserId)		
	)