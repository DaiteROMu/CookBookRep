CREATE PROC InsertUser
@Login nvarchar(50),
@Password nvarchar(20),
@Email nvarchar(100)
AS
INSERT INTO Users(Login, Password, Email)
VALUES (@Login, @Password, @Email)