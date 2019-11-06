CREATE PROC GetUsers
AS
SELECT [Users].UserId, [Users].Login, [Users].Password, [Users].Email FROM Users