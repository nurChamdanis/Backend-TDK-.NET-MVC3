﻿DECLARE 
	  @@QUERY VARCHAR(MAX)
	, @@LENGTH INT = @P_LENGTH
	, @@ID VARCHAR(50) = REPLACE(@P_ID, '''', '`')
	, @@NAME VARCHAR(50) = REPLACE(@P_NAME, '''', '`')
	, @@APPALIAS VARCHAR(50) = REPLACE(@P_APPALIAS, '''', '`')
	, @@FROMNUMBER INT = @P_FROMNUMBER
	, @@TONUMBER INT = @P_TONUMBER
	, @@ORDER_BY VARCHAR(MAX) = @P_ORDER_BY
	, @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`');

SET @@QUERY = 'SELECT 
	APPLICATION,
	ID,
	NAME,
	(SELECT COUNT(1) FROM (
        SELECT DISTINCT
            A.APPLICATION,
            ROLE,
            [FUNCTION] AS ID,
            B.NAME,
            B.DESCRIPTION
        FROM TB_M_AUTHORIZATION A
        LEFT JOIN TB_M_FUNCTION B ON A.[FUNCTION] = B.ID
        WHERE A.APPLICATION = '''+@@APPALIAS+'''
        AND A.ROLE = outer_query.ID
    ) AS subquery) AS TOTAL_FUNCTION
FROM TB_M_ROLE AS outer_query WHERE APPLICATION='''+@@APPALIAS+''''


IF (@@ID != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND ID LIKE ''%' + @@ID + '%''';
END

IF (@@NAME != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND NAME LIKE ''%' + @@NAME + '%''';
END


IF (@@LENGTH > 0) BEGIN
	SET @@QUERY = @@QUERY +  ' ORDER BY '+ @@ORDER_BY+ ' '+@@ORDER_TYPE+ ' OFFSET ' + CONVERT(VARCHAR, @@FROMNUMBER) + ' ROWS FETCH NEXT ' + CONVERT(VARCHAR, @@LENGTH) + ' ROWS ONLY';
END

EXECUTE (@@QUERY);