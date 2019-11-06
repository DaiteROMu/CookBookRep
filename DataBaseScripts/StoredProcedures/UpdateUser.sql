CREATE PROC UpdateUser
@UserId int,
@Login nvarchar(50),
@Password nvarchar(20),
@Email nvarchar(100)
AS
UPDATE Users
SET Login=@Login, Password=@Password, Email=@Email
WHERE [Users].UserId=@UserId