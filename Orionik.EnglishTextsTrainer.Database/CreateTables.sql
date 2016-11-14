﻿USE EnglishTextsTrainer
GO

CREATE TABLE tblWord
(
	Id INT NOT NULL IDENTITY(1, 1),
	Name NVARCHAR(50) NOT NULL,
	Meanings NVARCHAR(128) NOT NULL,
	Ignored BIT NOT NULL,
	Know BIT NOT NULL,
	CONSTRAINT PK_tblWord_Id PRIMARY KEY(Id)
)
GO