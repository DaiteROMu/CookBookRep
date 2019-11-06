CREATE PROC GetIngridientById
@IngridientId int
AS
SELECT * FROM Ingridient
WHERE [Ingridient].IngridientId=@IngridientId