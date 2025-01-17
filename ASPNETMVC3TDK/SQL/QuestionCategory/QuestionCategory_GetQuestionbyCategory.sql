﻿DECLARE 
	@@QUERY NVARCHAR(MAX)
	, @@CATEGORY_ID INT = @P_M_CATEGORY_ID
	, @@QUESTION_ID INT = @P_M_QUESTION_ID
	, @@PERIODE_ID  INT = @P_M_PERIODE_ID
	, @@RECENT_DATA BIT = @P_RECENT_DATA
	, @@TABLE_QUESTION NVARCHAR(128)
	, @@TABLE_OPTION NVARCHAR(128)
	, @@TABLE_TYPE_OPTION NVARCHAR(128)
	, @@CONDITIONAL_JOIN_TB_OPTION_TO_TB_QUESTION NVARCHAR(128)
	, @@CONDITIONAL_JOIN_TB_TYPE_OPTION_TO_TB_QUESTION NVARCHAR(128);

IF(@@RECENT_DATA = 1) BEGIN 
	SET @@TABLE_QUESTION = 'ASP.TB_M_QUESTION';
	SET @@TABLE_OPTION = 'ASP.TB_M_OPTION';
	SET @@TABLE_TYPE_OPTION = 'ASP.TB_M_TYPE_OPTION';
	SET @@CONDITIONAL_JOIN_TB_OPTION_TO_TB_QUESTION = 'A.M_QUESTION_ID = B.M_QUESTION_ID';
	SET @@CONDITIONAL_JOIN_TB_TYPE_OPTION_TO_TB_QUESTION = 'C.M_TYPE_OPTION_ID = B.M_TYPE_OPTION_ID';
END ELSE BEGIN
	SET @@TABLE_QUESTION = 'ASP.TB_M_PERIODE_QUESTION';
	SET @@TABLE_OPTION = 'ASP.TB_M_PERIODE_OPTION';
	SET @@TABLE_TYPE_OPTION = 'ASP.TB_M_PERIODE_TYPE_OPTION';
	SET @@CONDITIONAL_JOIN_TB_OPTION_TO_TB_QUESTION = 'B.M_QUESTION_ID = A.M_QUESTION_ID AND B.M_PERIODE_ID = A.M_PERIODE_ID';
	SET @@CONDITIONAL_JOIN_TB_TYPE_OPTION_TO_TB_QUESTION = 'C.M_TYPE_OPTION_ID = B.M_TYPE_OPTION_ID AND C.M_PERIODE_ID = A.M_PERIODE_ID';
END

SET @@QUERY = N'
SELECT 
	A.M_QUESTION_ID ,
	A.QUESTION_DESC,
	A.QUESTION_MANDATORY,
	A.QUESTION_SEPARATOR,
	B.M_OPTION_ID,
	B.OPTION_LIST,
	B.OPTION_PARENT,
	B.OPTION_PLACEHOLDER,
	C.TYPE_OPTION_NAME
FROM 
	' + @@TABLE_QUESTION + ' A
JOIN 
		' + @@TABLE_OPTION + ' B 
	ON 
		' + @@CONDITIONAL_JOIN_TB_OPTION_TO_TB_QUESTION + '
JOIN 
		' + @@TABLE_TYPE_OPTION + ' C 
	ON 
		' + @@CONDITIONAL_JOIN_TB_TYPE_OPTION_TO_TB_QUESTION + '
WHERE
	1=1
';

IF(@@RECENT_DATA = 0) BEGIN 
	SET @@QUERY = @@QUERY + N' AND A.M_PERIODE_ID = @@PERIODE_ID';
END

IF(@@CATEGORY_ID IS NOT NULL) BEGIN 
	SET @@QUERY = @@QUERY + N' AND A.M_CATEGORY_ID = @@CATEGORY_ID';
END 

IF(@@QUESTION_ID IS NOT NULL) BEGIN 
	SET @@QUERY = @@QUERY + N' AND A.M_QUESTION_ID = @@QUESTION_ID';
END 

SET @@QUERY = @@QUERY + N' ORDER BY A.QUESTION_ORDER, B.OPTION_ORDER';

EXEC sp_executesql @@QUERY,  N'@@PERIODE_ID INT, @@CATEGORY_ID INT, @@QUESTION_ID INT', @@PERIODE_ID = @@PERIODE_ID, @@CATEGORY_ID = @@CATEGORY_ID, @@QUESTION_ID = @@QUESTION_ID;