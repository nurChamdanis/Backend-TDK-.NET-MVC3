﻿DECLARE 
    @@QUERY VARCHAR(MAX), 
    @@QUERY_REQ VARCHAR(MAX),
    @@QUERY_CTE VARCHAR(MAX),
    @@NOREG VARCHAR(MAX) = @P_NOREG,
    @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`'),
    @@REQ_STATUS VARCHAR(MAX) = @P_STATUS;

SET @@QUERY = '
SELECT
NOREG ,
CLASS ,
ORG_ID ,
POSITION_LEVEL ,
POSITION_ABBR ,
DIRECTORATE_ID ,
DIRECTORATE_NAME ,
DIVISION_ID ,
DIVISION_NAME ,
DEPARTMENT_ID ,
DEPARTMENT_NAME ,
SECTION_ID ,
SECTION_NAME ,
LINE_ID ,
LINE_NAME ,
GROUP_ID ,
GROUP_NAME ,
RESPONSIBILITY ,
ACCOMPLISHMENT ,
VALID_FROM ,
VALID_TO ,
REMARK_1 ,
REMARK_2 ,
POSITION_PERCENT ,
CREATED_DT ,
CREATED_BY 
FROM PDI.TB_M_ROTATION_HISTORY
WHERE 1=1 '; 

IF(@@REQ_STATUS != '')  BEGIN
    SET @@QUERY = @@QUERY + ' AND NOREG = '''+@@NOREG+''' -- ORDER BY A.VALID_TO DESC';
END;

EXECUTE (@@QUERY)