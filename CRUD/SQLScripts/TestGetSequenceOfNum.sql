DECLARE @t TABLE (num INT);

INSERT @t EXEC [devAG].[GetSequenceOfNum] 123, 123141;

SELECT * FROM @t