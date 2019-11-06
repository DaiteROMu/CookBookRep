CREATE PROC DeleteUser
@UserId int
AS
DELETE FROM Users
WHERE [Users].UserId=@UserId