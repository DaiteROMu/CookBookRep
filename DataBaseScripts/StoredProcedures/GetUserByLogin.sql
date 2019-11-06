CREATE PROC GetUserByLogin
@Login nvarchar(50)
AS
SELECT * FROM Users
WHERE [Users].Login=@Login