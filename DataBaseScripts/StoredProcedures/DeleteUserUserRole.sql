CREATE PROC DeleteUserUserRole
@UserId int,
@UserRoleId int
AS
DELETE FROM UserUserRole
WHERE [UserUserRole].UserId=@UserId and [UserUserRole].UserRoleId=@UserRoleId