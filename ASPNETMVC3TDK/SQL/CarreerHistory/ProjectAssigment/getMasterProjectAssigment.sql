﻿DECLARE 
    @@QUERY VARCHAR(MAX), 
    @@NOREG VARCHAR(MAX) = @P_NOREG
    ;

    
     

SET @@QUERY = ' 
select 
A.*,
CONVERT(varchar(10), A.VALID_FROM, 120) AS VALID_FROM,
CONVERT(varchar(10), A.VALID_TO, 120) AS VALID_TO,
    PH.NAME AS EMPLOYEE_NAME,
B.POSITION_DESC,
B.GROUP_LEVEL_ID,
B.LEVEL_ID
FROM PDI.TB_M_PROJECT_ASSIGNMENT A
LEFT JOIN dbo.TB_M_ORG_POSITION B ON A.POSITION_ABBR = B.POSITION_ABBR 
LEFT JOIN DBO.TB_M_PROFILE_HEADER PH ON A.NOREG=PH.NOREG
WHERE 1=1 
AND A.NOREG IN ( '+@@NOREG+' ) 
ORDER BY A.NOREG ASC, A.VALID_FROM DESC  
';

EXECUTE (@@QUERY)