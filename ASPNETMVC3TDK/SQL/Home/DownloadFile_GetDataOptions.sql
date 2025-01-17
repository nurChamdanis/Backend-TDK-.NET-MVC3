﻿DECLARE @@QUERY VARCHAR(MAX);
DECLARE
	@@SYSTEM_CD VARCHAR(MAX)
	, @@SYSTEM_TYPE VARCHAR(MAX)
	, @@PARAM_3 VARCHAR(MAX)
	, @@PARAM_4 VARCHAR(MAX)
	, @@PARAM_5 VARCHAR(MAX);

SET @@SYSTEM_TYPE = REPLACE(@p_SYSTEM_TYPE, '''', '`');
SET @@SYSTEM_CD = REPLACE(@p_SYSTEM_CD, '''', '`');

IF(@@SYSTEM_TYPE = 'SHIFT_CD') BEGIN
	SELECT SYSTEM_CD AS id, SYSTEM_VALUE_TXT AS text FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'SHIFT_CD' AND IS_ACTIVE = '1'
END ELSE
IF(@@SYSTEM_TYPE = 'REPORT_LIST') BEGIN
	SELECT SYSTEM_CD AS id, SYSTEM_VALUE_TXT AS text, SYSTEM_REMARK AS attr_1 FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'REPORT_LIST' AND IS_ACTIVE = '1' AND SYSTEM_CD NOT IN ('ANALYST_ASPNETMVC3TDKS23', 'REALTIME_ASPNETMVC3TDKS24')
END ELSE 
IF(@@SYSTEM_TYPE = 'PRODUCT_TYPE') BEGIN
	SELECT SYSTEM_CD AS id, SYSTEM_VALUE_TXT AS text FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'PRODUCT_TYPE' AND IS_ACTIVE = '1'
END ELSE 
IF(@@SYSTEM_TYPE = 'CRON_SCHEDULE') BEGIN
	SELECT SYSTEM_CD AS id, SYSTEM_VALUE_TXT AS text, IS_ACTIVE as attr_1 FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'CRON_SCHEDULE' AND IS_ACTIVE = '1'
END ELSE 
BEGIN
	SELECT '' AS id, '' AS text;
END