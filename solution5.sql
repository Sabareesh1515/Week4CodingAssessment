CREATE DATABASE Week4CodingAssessment
USE Week4CodingAssessment

CREATE FUNCTION CountWords(@sentence VARCHAR(MAX))
RETURNS INT
AS
BEGIN
    DECLARE @count INT
    DECLARE @trimmed VARCHAR(MAX)
    DECLARE @lengthWithSpaces INT
    DECLARE @lengthWithoutSpaces INT

    SET @trimmed = LTRIM(RTRIM(@sentence))

    SET @lengthWithSpaces = LEN(@trimmed)

    SET @lengthWithoutSpaces = LEN(REPLACE(@trimmed, ' ', ''))

    SET @count = @lengthWithSpaces - @lengthWithoutSpaces + 1

    RETURN @count
END


SELECT dbo.CountWords('Hello World') AS WordCount;