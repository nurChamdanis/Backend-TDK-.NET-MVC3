﻿DECLARE 
	  @@QUERY VARCHAR(MAX)
	, @@LENGTH INT = @P_LENGTH
	, @@ID VARCHAR(50) = REPLACE(@P_ID, '''', '`')
	, @@NAME VARCHAR(50) = REPLACE(@P_NAME, '''', '`')
	, @@ROLE VARCHAR(50) = @P_ROLE
	, @@APPALIAS VARCHAR(50) = @P_APPALIAS
	, @@FROMNUMBER INT = @P_FROMNUMBER
	, @@TONUMBER INT = @P_TONUMBER
	, @@ORDER_BY VARCHAR(MAX) = @P_ORDER_BY
	, @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`');

SET @@QUERY = 'SELECT DISTINCT
		A.APPLICATION,
		ROLE,
		[FUNCTION] as ID,
		B.NAME,
		(SELECT COUNT(1) FROM (
			SELECT DISTINCT
				C.APPLICATION,
				ROLE,
				[FEATURE] AS ID,
				D.NAME
			FROM TB_M_AUTHORIZATION C
			LEFT JOIN TB_M_FEATURE D ON C.[FEATURE] = D.ID AND C.APPLICATION = D.APPLICATION
			WHERE C.APPLICATION = '''+@@APPALIAS+'''
			AND C.ROLE = '''+@@ROLE+'''
			AND C.[FUNCTION] = A.[FUNCTION]
			AND C.FEATURE != ''''
		) AS subquery) AS TOTAL_FEATURE
 FROM TB_M_AUTHORIZATION A
 LEFT JOIN TB_M_FUNCTION B ON A.[FUNCTION] = B.ID
WHERE A.APPLICATION = '''+@@APPALIAS+'''
AND A.ROLE = '''+@@ROLE+''''


IF (@@ID != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND [FUNCTION] LIKE ''%' + @@ID + '%''';
END

IF (@@NAME != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND B.NAME LIKE ''%' + @@NAME + '%''';
END

IF (@@LENGTH > 0) BEGIN
	SET @@QUERY = @@QUERY +  ' ORDER BY '+ @@ORDER_BY+ ' '+@@ORDER_TYPE+ ' OFFSET ' + CONVERT(VARCHAR, @@FROMNUMBER) + ' ROWS FETCH NEXT ' + CONVERT(VARCHAR, @@LENGTH) + ' ROWS ONLY';
END

EXECUTE (@@QUERY);