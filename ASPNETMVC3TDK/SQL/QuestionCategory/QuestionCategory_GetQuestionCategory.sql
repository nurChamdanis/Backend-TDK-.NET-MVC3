﻿DECLARE 
    @@QUERY NVARCHAR(MAX)
    , @@CATEGORY_ID INT = @P_M_CATEGORY_ID
	, @@PERIODE_ID  INT = @P_M_PERIODE_ID
	, @@RECENT_DATA BIT = @P_RECENT_DATA
	, @@TABLE_CATEGORY NVARCHAR(128);

IF(@@RECENT_DATA = 1) BEGIN 
	SET @@TABLE_CATEGORY = 'ASP.TB_M_CATEGORY';
END ELSE BEGIN
	SET @@TABLE_CATEGORY = 'ASP.TB_M_PERIODE_CATEGORY';
END

SET @@QUERY = N'
    SELECT 
        * 
    FROM 
        ' + @@TABLE_CATEGORY + '
    WHERE 
        IS_ACTIVE = 1 ';

IF(@@RECENT_DATA = 0) BEGIN 
	SET @@QUERY = @@QUERY + N' AND M_PERIODE_ID = @@PERIODE_ID';
END

IF (@@CATEGORY_ID IS NOT NULL) BEGIN
    SET @@QUERY = @@QUERY + N' AND M_CATEGORY_ID = @@CATEGORY_ID ';
END

SET @@QUERY = @@QUERY + N' ORDER BY CATEGORY_ORDER ASC';

EXEC sp_executesql @@QUERY,  N'@@PERIODE_ID INT, @@CATEGORY_ID INT', @@PERIODE_ID = @@PERIODE_ID, @@CATEGORY_ID = @@CATEGORY_ID;