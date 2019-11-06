CREATE PROC GetUserRolesByUserId
@UserId int
AS
SELECT [UserUserRole].UserRoleId, [UserRole].Name FROM UserUserRole inner join UserRole on [UserUserRole].UserRoleId=[UserRole].UserRoleId
WHERE [UserUserRole].UserId=@UserId