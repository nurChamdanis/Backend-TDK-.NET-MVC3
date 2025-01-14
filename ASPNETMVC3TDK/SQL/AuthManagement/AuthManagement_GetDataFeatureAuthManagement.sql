﻿DECLARE 
	  @@QUERY VARCHAR(MAX)
	, @@LENGTH INT = @P_LENGTH
	, @@ID VARCHAR(50) = REPLACE(@P_ID, '''', '`')
	, @@NAME VARCHAR(50) = REPLACE(@P_NAME, '''', '`')
	, @@ROLE VARCHAR(50) = @P_ROLE
	, @@FUNCTION VARCHAR(50) = @P_FUNCTION
	, @@APPALIAS VARCHAR(50) = @P_APPALIAS
	, @@FROMNUMBER INT = @P_FROMNUMBER
	, @@TONUMBER INT = @P_TONUMBER
	, @@ORDER_BY VARCHAR(MAX) = @P_ORDER_BY
	, @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`');

SET @@QUERY = 'SELECT DISTINCT
		A.APPLICATION,
		ROLE,
		FEATURE as ID,
		B.NAME,
		B.DESCRIPTION
 FROM TB_M_AUTHORIZATION A
JOIN TB_M_FEATURE B ON A.FEATURE = B.ID AND B.APPLICATION = A.APPLICATION
WHERE A.APPLICATION = '''+@@APPALIAS+'''
AND A.ROLE = '''+@@ROLE+'''
AND A.[FUNCTION] = '''+@@FUNCTION+'''
AND A.FEATURE != ''''';


IF (@@ID != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND ID LIKE ''%' + @@ID + '%''';
END

IF (@@NAME != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND B.NAME LIKE ''%' + @@NAME + '%''';
END


IF (@@LENGTH > 0) BEGIN
	SET @@QUERY = @@QUERY +  ' ORDER BY '+ @@ORDER_BY+ ' '+@@ORDER_TYPE+ ' OFFSET ' + CONVERT(VARCHAR, @@FROMNUMBER) + ' ROWS FETCH NEXT ' + CONVERT(VARCHAR, @@LENGTH) + ' ROWS ONLY';
END

EXECUTE (@@QUERY);