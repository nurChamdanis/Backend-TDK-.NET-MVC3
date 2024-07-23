﻿DECLARE @@NOREG VARCHAR(MAX) = @P_NOREG;

DECLARE @@QUERY VARCHAR(MAX);

SET @@QUERY = ' 
SELECT TOP 3
SQ.*
FROM (
    SELECT
		A.TRANSACTION_ID, A.CREATED_DT, B.SYSTEM_VALUE, C.PERSONNEL_NAME, A.NOREG, A.STATUS_CD, A.CANCEL_FLAG
    FROM PDI.TB_R_SUBMISSION_EDUCATION A
    JOIN DBO.TB_M_SYSTEM B ON LEFT(A.TRANSACTION_ID, 2) = B.SYSTEM_CD AND B.FUNCTION_ID = ''MODUL_PDI''
    JOIN (SELECT DISTINCT(SUB_A.NOREG), SUB_A.PERSONNEL_NAME from VW_ORGANIZATION SUB_A) C ON C.NOREG = A.NOREG
		UNION
		SELECT
		A.TRANSACTION_ID, A.CREATED_DT, B.SYSTEM_VALUE, C.PERSONNEL_NAME, A.NOREG, A.STATUS_CD, A.CANCEL_FLAG
    FROM PDI.TB_R_SUBMISSION_LANGUAGE A
    JOIN DBO.TB_M_SYSTEM B ON LEFT(A.TRANSACTION_ID, 2) = B.SYSTEM_CD AND B.FUNCTION_ID = ''MODUL_PDI''
    JOIN (SELECT DISTINCT(SUB_A.NOREG), SUB_A.PERSONNEL_NAME from VW_ORGANIZATION SUB_A) C ON C.NOREG = A.NOREG
		UNION
		SELECT
		A.TRANSACTION_ID, A.CREATED_DT, B.SYSTEM_VALUE, C.PERSONNEL_NAME, A.NOREG, A.STATUS_CD, A.CANCEL_FLAG
    FROM PDI.TB_R_SUBMISSION_ROTATION_HISTORY A
    JOIN DBO.TB_M_SYSTEM B ON LEFT(A.TRANSACTION_ID, 2) = B.SYSTEM_CD AND B.FUNCTION_ID = ''MODUL_PDI''
    JOIN (SELECT DISTINCT(SUB_A.NOREG), SUB_A.PERSONNEL_NAME from VW_ORGANIZATION SUB_A) C ON C.NOREG = A.NOREG
		UNION
		SELECT
		A.TRANSACTION_ID, A.CREATED_DT, B.SYSTEM_VALUE, C.PERSONNEL_NAME, A.NOREG, A.STATUS_CD, A.CANCEL_FLAG
    FROM PDI.TB_R_SUBMISSION_PROJECT_ASSIGNMENT A
    JOIN DBO.TB_M_SYSTEM B ON LEFT(A.TRANSACTION_ID, 2) = B.SYSTEM_CD AND B.FUNCTION_ID = ''MODUL_PDI''
    JOIN (SELECT DISTINCT(SUB_A.NOREG), SUB_A.PERSONNEL_NAME from VW_ORGANIZATION SUB_A) C ON C.NOREG = A.NOREG
		UNION
		SELECT
		A.TRANSACTION_ID, A.CREATED_DT, B.SYSTEM_VALUE, C.PERSONNEL_NAME, A.NOREG, A.STATUS_CD, A.CANCEL_FLAG
    FROM PDI.TB_R_SUBMISSION_ICT_HISTORY A
    JOIN DBO.TB_M_SYSTEM B ON LEFT(A.TRANSACTION_ID, 2) = B.SYSTEM_CD AND B.FUNCTION_ID = ''MODUL_PDI''
    JOIN (SELECT DISTINCT(SUB_A.NOREG), SUB_A.PERSONNEL_NAME from VW_ORGANIZATION SUB_A) C ON C.NOREG = A.NOREG
		UNION
		SELECT
		A.TRANSACTION_ID, A.CREATED_DT, B.SYSTEM_VALUE, C.PERSONNEL_NAME, A.NOREG, A.STATUS_CD, A.CANCEL_FLAG
    FROM PDI.TB_R_SUBMISSION_TRAINING_HISTORY A
    JOIN DBO.TB_M_SYSTEM B ON LEFT(A.TRANSACTION_ID, 2) = B.SYSTEM_CD AND B.FUNCTION_ID = ''MODUL_PDI''
    JOIN (SELECT DISTINCT(SUB_A.NOREG), SUB_A.PERSONNEL_NAME from VW_ORGANIZATION SUB_A) C ON C.NOREG = A.NOREG
) SQ
WHERE SQ.NOREG = '''+@@NOREG+'''
AND SQ.CANCEL_FLAG IS NULL
AND SQ.STATUS_CD != 99
ORDER BY SQ.CREATED_DT ASC
';


EXEC (@@QUERY);