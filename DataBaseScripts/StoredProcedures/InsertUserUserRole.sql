CREATE PROC InsertUserUserRole
@UserId int,
@UserRoleId int
AS
INSERT INTO UserUserRole(UserId, UserRoleId)
VALUES (@UserId, @UserRoleId)