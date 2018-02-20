DECLARE @t TABLE (num INT);

INSERT @t EXEC [devAG].[GetSequenceOfNum] 123, 120315;

SELECT * FROM @t