USE ECLearning;
GO

CREATE PROCEDURE GetSequenceOfNum @from INT, @to INT AS
BEGIN
	DECLARE @count INT = @from;
	DECLARE @numTable TABLE ( Number INT NOT NULL);

	WHILE @count <= @to
	BEGIN
		INSERT INTO @numTable (Number) SELECT @count;
		SET @count = @count + 1;
	END

	SELECT * FROM @numTable
END
GO