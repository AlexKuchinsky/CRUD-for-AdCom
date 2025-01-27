USE ECLearning
GO

CREATE PROCEDURE GetSequenceOfNum @from INT, @to INT AS
BEGIN
	WITH e1(num) AS
	(
		SELECT 0 UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL 
		SELECT 3 UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL 
		SELECT 6 UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9
	), -- 10
	e2(num) AS 
	(
		SELECT a.num*10+b.num AS num 
		FROM e1 AS a 
		CROSS JOIN e1 AS b 
	), -- 10*10
	e3(num) AS
	(
		SELECT a.num*100+b.num AS num 
		FROM e1 AS a 
		CROSS JOIN e2 AS b 
	), -- 10*100
	e4(num) AS
	(
		SELECT a.num*1000+b.num AS num 
		FROM e3 AS a 
		CROSS JOIN e3 AS b 
	) -- 1000*1000
	SELECT * FROM e4 
	ORDER BY num
	OFFSET @from ROWS
    FETCH NEXT (@to - @from + 1) ROWS ONLY
END
GO