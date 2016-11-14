USE EnglishTextsTrainer
GO

EXEC spGetWords

DECLARE @Id INT
EXEC spAddNewWord 'good', @id OUTPUT, 'добре'
SELECT @id

EXEC spUpdateWord 'good1', 1, 'добре1', 1, 1

EXEC spDeleteWord 1

