CREATE PROC InsertCategory
@Name nvarchar(50),
@ParentId int
AS
INSERT INTO Category(Name, ParentId)
VALUES (@Name, @ParentId)