CREATE PROC DeleteIngridient
@IngridientId int
AS
DELETE FROM Ingridient
WHERE [Ingridient].IngridientId=@IngridientId