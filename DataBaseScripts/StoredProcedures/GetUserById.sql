CREATE PROC GetUserById
@UserId int
AS
SELECT * FROM Users
WHERE [Users].UserId=@UserId