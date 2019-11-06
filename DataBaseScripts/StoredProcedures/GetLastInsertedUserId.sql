CREATE PROC GetLastInsertedUserId
AS
SELECT ident_current('Users')