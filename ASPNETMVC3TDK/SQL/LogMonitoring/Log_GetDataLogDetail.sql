﻿DECLARE @@PROCESS_ID VARCHAR(25);
DECLARE @@USERNAME VARCHAR(MAX);

SET @@PROCESS_ID = @PROCESS_ID;

SELECT * FROM TB_R_LOG_D WHERE PROCESS_ID = @@PROCESS_ID ORDER BY SEQ_NO ASC;