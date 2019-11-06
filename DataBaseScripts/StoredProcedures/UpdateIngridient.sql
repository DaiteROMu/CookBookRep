CREATE PROC UpdateIngridient
@IngridientId int,
@Name nvarchar(50)
AS
UPDATE Ingridient
SET Name=@Name
WHERE [Ingridient].IngridientId=@IngridientId