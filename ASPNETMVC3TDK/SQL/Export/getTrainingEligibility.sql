﻿DECLARE 
    @@QUERY VARCHAR(MAX),  
    @@NOREG VARCHAR(MAX) = @P_NOREG,
    @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`'),
    @@REQ_STATUS VARCHAR(MAX) = @P_STATUS;
    
DECLARE @@BASE_URL VARCHAR(MAX) = @P_URL;


SET @@QUERY = '
SELECT 
    NOREG ,
    SEQ ,
    TRAINING_TYPE ,
    INSTITUTION ,
    COUNTRY_ID ,
    YEAR ,
    TRAINING_TOPIC ,
    SKILL ,
    CERTIFICATE_NAME ,
    CONCAT('''+@@BASE_URL+''', CERTIFICATE_PATH) AS CERTIFICATE_PATH ,
    REMARK_1 ,
    REMARK_2 ,
    CREATED_DT ,
    CREATED_BY 
FROM PDI.TB_M_PROFILE_TRAINING_HISTORY
WHERE 
    1=1 
';

IF(@@REQ_STATUS != '')  BEGIN
    SET @@QUERY = @@QUERY + ' AND NOREG = '''+@@NOREG+'''  
	 and  TRAINING_TYPE = 2
 ';
END;

EXECUTE (@@QUERY)