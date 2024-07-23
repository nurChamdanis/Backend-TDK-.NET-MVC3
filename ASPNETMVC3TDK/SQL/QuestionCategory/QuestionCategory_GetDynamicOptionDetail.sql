﻿DECLARE 
	@@QUERY NVARCHAR(MAX)
	, @@DYNAMIC_QUERY NVARCHAR(MAX)
	, @@REFERENCE_SEARCH_DYNAMIC_QUERY VARCHAR(MAX)
	, @@REFERENCE_ID_DYNAMIC_QUERY VARCHAR(MAX)
	, @@QUESTION_ID INT = @P_QUESTION_ID
	, @@VALUE INT = @P_VALUE
	, @@ID_REFERENCE INT = @P_ID_REFERENCE
	, @@SEARCH_VALUE VARCHAR(MAX) = @P_SEARCH_VALUE;

DECLARE @@DATA TABLE (ID INT, [DESC] VARCHAR(MAX));

SET @@QUERY = N'
	SELECT 
		@@DYNAMIC_QUERY = OPTION_QUERY,
		@@REFERENCE_SEARCH_DYNAMIC_QUERY = OPTION_SEARCH_REFERENCE,
		@@REFERENCE_ID_DYNAMIC_QUERY = OPTION_ID_REFERENCE
	FROM 
		ASP.TB_M_OPTION A
	WHERE
		A.M_QUESTION_ID = @@QUESTION_ID
';

EXEC sp_executesql @@QUERY
	, N' @@DYNAMIC_QUERY NVARCHAR(MAX) OUTPUT, @@REFERENCE_SEARCH_DYNAMIC_QUERY VARCHAR(MAX) OUTPUT, @@REFERENCE_ID_DYNAMIC_QUERY VARCHAR(MAX) OUTPUT, @@QUESTION_ID INT'
	, @@DYNAMIC_QUERY OUTPUT
	, @@REFERENCE_SEARCH_DYNAMIC_QUERY OUTPUT
	, @@REFERENCE_ID_DYNAMIC_QUERY OUTPUT
	, @@QUESTION_ID = @@QUESTION_ID;

IF(@@DYNAMIC_QUERY IS NOT NULL) BEGIN 
	IF (@@ID_REFERENCE IS NOT NULL) BEGIN 
		SET @@DYNAMIC_QUERY = @@DYNAMIC_QUERY + ' AND ' + @@REFERENCE_ID_DYNAMIC_QUERY + ' = @@ID_REFERENCE';
	END
	
	IF (@@SEARCH_VALUE IS NOT NULL) BEGIN 
		SET @@SEARCH_VALUE = '%' + @@SEARCH_VALUE + '%'
		SET @@DYNAMIC_QUERY = @@DYNAMIC_QUERY + ' AND ' + @@REFERENCE_SEARCH_DYNAMIC_QUERY + ' LIKE @@SEARCH_VALUE';
	END
	
    INSERT INTO @@DATA (ID, [DESC])
    EXEC sp_executesql @@DYNAMIC_QUERY
		, N' @@VALUE INT, @@ID_REFERENCE INT, @@SEARCH_VALUE VARCHAR(MAX)'
		, @@VALUE = @@VALUE
		, @@ID_REFERENCE = @@ID_REFERENCE
		, @@SEARCH_VALUE = @@SEARCH_VALUE;
END 

SELECT * FROM @@DATA