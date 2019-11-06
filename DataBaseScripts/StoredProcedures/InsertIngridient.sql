CREATE PROC InsertIngridient
@Name nvarchar(50)
AS
INSERT INTO Ingridient(Name)
VALUES (@Name)