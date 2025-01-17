﻿DECLARE @@DATE VARCHAR(20)
, @@SHIFT_CD VARCHAR(1)
, @@SHIFT_TYPE VARCHAR(5)
, @@DATE_FROM DATETIME
, @@DATE_TO DATETIME;

SET @@SHIFT_CD = @_SHIFT_CD;
SET @@DATE = @_PRODUCTION_DT;

-- Set Shift Type
SELECT @@SHIFT_TYPE = SHIFT_TYPE FROM TB_R_QC_SHIFT_SUM WHERE SHIFT_CD = @@SHIFT_CD AND PRODUCTION_DT = @@DATE;

IF(@@SHIFT_TYPE = 'DAY') BEGIN
	SET @@DATE_FROM = CONVERT(DATETIME, @@DATE + ' 07:20:00');
	SET @@DATE_TO = CONVERT(DATETIME, @@DATE + ' 19:00:00');
END
ELSE BEGIN
	SET @@DATE_FROM = CONVERT(DATETIME, @@DATE + ' 20:00:00');
	SET @@DATE_TO = DATEADD(DAY, 1, CONVERT(DATETIME, @@DATE + ' 08:00:00'));
END


IF OBJECT_ID(N'tempdb..#TB_T_PRODUCT') IS NOT NULL BEGIN
	DROP TABLE #TB_T_PRODUCT
END

IF OBJECT_ID(N'tempdb..#TB_T_COUNT') IS NOT NULL BEGIN
	DROP TABLE #TB_T_COUNT
END

IF OBJECT_ID(N'tempdb..#TB_T_TIME') IS NOT NULL BEGIN
	DROP TABLE #TB_T_TIME
END

IF OBJECT_ID(N'tempdb..#TB_T_FINAL_RESULT') IS NOT NULL BEGIN
	DROP TABLE #TB_T_FINAL_RESULT
END

CREATE TABLE #TB_T_PRODUCT(
	QC_GATE_ID INT
	, PRODUCT_ID VARCHAR(20)
	, PRODUCT_TYPE VARCHAR(10)
	, FINAL_JUDGEMENT VARCHAR(10)
	, CHANGED_BY VARCHAR(100)
	, CHANGED_DT DATETIME
	, SHIFT_CD VARCHAR(1)
	, DATE_UPDATE DATE
	, TIME_UPDATE TIME
	, TIME_CD VARCHAR(30)
	, TIME_NM VARCHAR(30)
	, TOTAL_DEFECT INT
)

CREATE TABLE #TB_T_COUNT(
	PRODUCT_TYPE VARCHAR(10)
	, SHIFT_CD VARCHAR(1)
	, FINAL_JUDGEMENT VARCHAR(10)
	, TIME_CD VARCHAR(30)
	, TIME_NM VARCHAR(30)
	, CNT INT
)


CREATE TABLE #TB_T_TIME(
	TIME_CD VARCHAR(20)
	, TIME_NM VARCHAR(20)
	, TIME_FROM VARCHAR(20)
	, TIME_TO VARCHAR(20)
)

CREATE TABLE #TB_T_FINAL_RESULT(
	QC_SHIFT_ID INT
	, PRODUCTION_DT DATE
	, SHIFT_CD VARCHAR(20)
	, SHIFT_NM VARCHAR(20)
	, SHIFT_TYPE VARCHAR(20)
	, SHIFT_TYPE_NM VARCHAR(20)
	, TIME_CD VARCHAR(50)
	, TIME_NM VARCHAR(50)
	, AV DECIMAL(20, 2)
	, OK_1_CNT INT DEFAULT 0
	, OK_MTL_1_CNT INT DEFAULT 0
	, NG_1_CNT INT DEFAULT 0
	, REP_1_CNT INT DEFAULT 0
	, OK_2_CNT INT DEFAULT 0
	, OK_MTL_2_CNT INT DEFAULT 0
	, NG_2_CNT INT DEFAULT 0
	, REP_2_CNT INT DEFAULT 0
	, CAMS DECIMAL(20, 2)
	, ALL_PRODUCT DECIMAL(20, 2)
	, SECT_NAME VARCHAR(MAX)
	, LH_NAME VARCHAR(MAX)
	, GH_NAME VARCHAR(MAX)
	, PIC_NAME VARCHAR(MAX)
	, REPORT_NAME VARCHAR(MAX)
)

BEGIN TRY

	-- Insert temp Time
	INSERT INTO #TB_T_TIME
	SELECT SYSTEM_CD, SYSTEM_VALUE, REPLACE(LEFT(SYSTEM_VALUE, 5), '.', ':') AS TIME_FROM, REPLACE(RIGHT(SYSTEM_VALUE, 5), '.', ':') AS TIME_TO FROM TB_M_SYSTEM
	WHERE SYSTEM_TYPE = 'FINISHING_TIME' AND SYSTEM_CD LIKE (CASE @@SHIFT_TYPE WHEN 'DAY' THEN '%D%' ELSE '%N%' END)

	--SELECT * FROM #TB_T_TIME

	-- Insert temp product OK & NG
	INSERT INTO #TB_T_PRODUCT
	SELECT 
		T1.QC_GATE_ID
		, T1.PRODUCT_ID
		, T3.SYSTEM_VALUE AS PRODUCT_TYPE
		, T4.SYSTEM_VALUE AS FINAL_JUDGEMENT
		, T1.CHANGED_BY
		, T1.CHANGED_DT 
		, T2.SHIFT_CD
		, CONVERT(DATE, T1.CHANGED_DT) AS DATE_UPDATE
		, CONVERT(TIME, T1.CHANGED_DT) AS TIME_UPDATE
		, NULL AS TIME_CD
		, NULL AS TIME_NM
		, 0 AS TOTAL_DEFECT
	FROM TB_R_QC_GATE T1 
	JOIN TB_M_USER T2 ON T1.CHANGED_BY = T2.USERNAME
	JOIN TB_M_SYSTEM T3 ON T1.PRODUCT_TYPE = T3.SYSTEM_CD AND T3.SYSTEM_TYPE = 'PRODUCT_TYPE'
	JOIN TB_M_SYSTEM T4 ON T1.FINAL_JUDGEMENT = T4.SYSTEM_CD AND T4.SYSTEM_TYPE = 'JUDGEMENT_STS'
	WHERE T1.FINAL_JUDGEMENT IN ('1', '2')
	AND T2.SHIFT_CD = @@SHIFT_CD
	AND (T1.CHANGED_DT >=  @@DATE_FROM AND T1.CHANGED_DT <=  @@DATE_TO)
	ORDER BY T1.PRODUCT_TYPE, T1.FINAL_JUDGEMENT ASC;


	--  temp product Repair
	INSERT INTO #TB_T_PRODUCT
	SELECT DISTINCT
		T1.QC_GATE_ID
		, T1.PRODUCT_ID
		, T4.SYSTEM_VALUE AS PRODUCT_TYPE
		, 'REPAIR' AS FINAL_JUDGEMENT
		, T2.CREATED_BY
		, T2.CREATED_DT 
		, T3.SHIFT_CD
		, CONVERT(DATE, T2.CREATED_DT) AS DATE_UPDATE
		, CONVERT(TIME, T2.CREATED_DT) AS TIME_UPDATE
		, NULL AS TIME_CD
		, NULL AS TIME_NM
		, 0 AS TOTAL_DEFECT
	FROM TB_R_QC_GATE T1 
	JOIN TB_R_QC_GATE_DEFECT T2 ON T1.QC_GATE_ID = T2.QC_GATE_ID
	JOIN TB_M_USER T3 ON T2.CREATED_BY = T3.USERNAME
	JOIN TB_M_SYSTEM T4 ON T1.PRODUCT_TYPE = T4.SYSTEM_CD AND T4.SYSTEM_TYPE = 'PRODUCT_TYPE'
	WHERE T2.PRODUCT_STS = '1'
	AND T3.SHIFT_CD = @@SHIFT_CD
	AND (T2.CREATED_DT >=  @@DATE_FROM AND T2.CREATED_DT <=  @@DATE_TO);

	-- Update Total Defect
	;WITH CTE AS(
		SELECT 
			T1.QC_GATE_ID
			, COUNT(1) TOTAL_DEFECT
		FROM #TB_T_PRODUCT T1
		JOIN TB_R_QC_GATE_DEFECT T2 ON T1.QC_GATE_ID = T2.QC_GATE_ID
		GROUP BY T1.QC_GATE_ID
	)
	UPDATE T1
	SET 
		T1.TOTAL_DEFECT = T2.TOTAL_DEFECT
		, T1.FINAL_JUDGEMENT = (CASE WHEN T1.FINAL_JUDGEMENT = 'OK' AND ISNULL(T2.TOTAL_DEFECT, 0) > 0 THEN CONCAT(T1.FINAL_JUDGEMENT, ' MTL') ELSE T1.FINAL_JUDGEMENT END)
	FROM #TB_T_PRODUCT T1
	LEFT JOIN CTE T2 ON T1.QC_GATE_ID = T2.QC_GATE_ID


	-- Update Time Range
	UPDATE T1
	SET 
		T1.TIME_CD = T2.TIME_CD
		, T1.TIME_NM = T2.TIME_NM
	FROM #TB_T_PRODUCT T1
	--JOIN #TB_T_TIME T2 ON T1.TIME_UPDATE >= T2.TIME_FROM AND T1.TIME_UPDATE <= T2.TIME_TO
	JOIN #TB_T_TIME T2 ON T1.TIME_UPDATE >= CAST(CASE WHEN T2.TIME_FROM = '24:00' THEN '23:59' ELSE T2.TIME_FROM END AS TIME) AND T1.TIME_UPDATE <= CAST(CASE WHEN T2.TIME_TO = '24:00' THEN '23:59' ELSE T2.TIME_TO END AS TIME)
	--SELECT * FROM #TB_T_PRODUCT ORDER BY QC_GATE_ID;

	-- Insert summary 1
	INSERT INTO #TB_T_COUNT
	SELECT PRODUCT_TYPE, SHIFT_CD, FINAL_JUDGEMENT, TIME_CD, TIME_NM, COUNT(1) AS CNT FROM #TB_T_PRODUCT
	GROUP BY PRODUCT_TYPE, SHIFT_CD, FINAL_JUDGEMENT, TIME_CD, TIME_NM;

	--SELECT * FROM #TB_T_COUNT
	--ORDER BY PRODUCT_TYPE, FINAL_JUDGEMENT, TIME_NM
	--;


	DECLARE 
		@@SECT_NAME VARCHAR(MAX)
		, @@LH_NAME VARCHAR(MAX)
		, @@PIC_NAME VARCHAR(MAX)
		, @@GH_NAME VARCHAR(MAX)
		, @@REPORT_NAME VARCHAR(MAX);

	SELECT @@SECT_NAME = SYSTEM_VALUE FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'REPORT_FINISHING' AND SYSTEM_CD = 'SECT_NAME';
	SELECT @@LH_NAME = SYSTEM_VALUE FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'REPORT_FINISHING' AND SYSTEM_CD = 'LH_NAME';
	SELECT @@PIC_NAME = SYSTEM_VALUE FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'REPORT_FINISHING' AND SYSTEM_CD = 'PIC_NAME';
	SELECT @@GH_NAME = SYSTEM_VALUE FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'REPORT_FINISHING' AND SYSTEM_CD = 'GH_NAME';
	SELECT @@REPORT_NAME = REPLACE(REPLACE(REPLACE(SYSTEM_REMARK, '{shift}', CASE @@SHIFT_CD WHEN 'R' THEN 'Red' WHEN 'W' THEN 'White' END ), '{month}'
			, CASE MONTH(@@DATE) 
				WHEN 1 THEN 'Januari' 
				WHEN 2 THEN 'Februari' 
				WHEN 3 THEN 'Maret' 
				WHEN 4 THEN 'April' 
				WHEN 5 THEN 'Mei' 
				WHEN 6 THEN 'Juni' 
				WHEN 7 THEN 'Juli' 
				WHEN 8 THEN 'Agustus' 
				WHEN 9 THEN 'September' 
				WHEN 10 THEN 'Oktober' 
				WHEN 11 THEN 'November' 
				WHEN 12 THEN 'Desember' 
				ELSE '' END 
			), '{year}', YEAR(@@DATE)) 
	FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'REPORT_LIST' AND SYSTEM_CD = 'QCGATE_ASPNETMVC3TDKR19'

	-- Insert final result
	INSERT INTO #TB_T_FINAL_RESULT(
		QC_SHIFT_ID
		, PRODUCTION_DT
		, SHIFT_CD
		, SHIFT_NM
		, SHIFT_TYPE
		, SHIFT_TYPE_NM
		, TIME_CD
		, TIME_NM
		, AV
		, CAMS
		, ALL_PRODUCT
		, SECT_NAME
		, LH_NAME
		, GH_NAME
		, PIC_NAME
		, REPORT_NAME
	)
	SELECT
		QSS.QC_SHIFT_ID
		, QSS.PRODUCTION_DT
		, QSS.SHIFT_CD
		, SYS1.SYSTEM_VALUE AS SHIFT_NM
		, QSS.SHIFT_TYPE
		, SYS2.SYSTEM_VALUE AS SHIFT_TYPE_NM
		, QSL.TIME_CD
		, SYS3.SYSTEM_VALUE AS TIME_NM
		, QSL.AV
		, QSW.CAMS
		, QSW.ALL_PRODUCT
		, @@SECT_NAME AS SECT_NAME
		, @@LH_NAME AS LH_NAME
		, @@GH_NAME AS GH_NAME
		, @@PIC_NAME AS PIC_NAME
		, @@REPORT_NAME AS REPORT_NAME
	FROM TB_R_QC_SHIFT_SUM QSS
	JOIN TB_R_QC_SHIFT_LINE QSL ON QSS.QC_SHIFT_ID = QSL.QC_SHIFT_ID
	JOIN TB_R_QC_SHIFT_WH QSW ON QSS.QC_SHIFT_ID = QSW.QC_SHIFT_ID AND QSL.TIME_CD = QSW.TIME_CD
	JOIN TB_M_SYSTEM SYS1 ON SYS1.SYSTEM_TYPE = 'SHIFT_CD' AND SYS1.SYSTEM_CD = QSS.SHIFT_CD
	JOIN TB_M_SYSTEM SYS2 ON SYS2.SYSTEM_TYPE = 'SHIFT_TYPE' AND SYS2.SYSTEM_CD = QSS.SHIFT_TYPE
	JOIN TB_M_SYSTEM SYS3 ON SYS3.SYSTEM_TYPE = 'FINISHING_TIME' AND SYS3.SYSTEM_CD = QSL.TIME_CD
	WHERE QSS.PRODUCTION_DT = @@DATE
	AND QSS.SHIFT_CD = @@SHIFT_CD;

	-- Update Summary Status
	DECLARE @@PRODUCT_TYPE VARCHAR(MAX), @@FINAL_JUDGEMENT VARCHAR(MAX), @@TIME_CD VARCHAR(MAX), @@UPDATE_SQL VARCHAR(MAX), @@CNT VARCHAR(MAX);

	DECLARE @@COL_NAME VARCHAR(MAX);
	DECLARE _CURR_SUMMARY_STATUS CURSOR LOCAL FOR
	SELECT PRODUCT_TYPE, FINAL_JUDGEMENT, TIME_CD, ISNULL(CNT, 0) FROM #TB_T_COUNT ;

	OPEN _CURR_SUMMARY_STATUS  
		FETCH NEXT FROM _CURR_SUMMARY_STATUS INTO @@PRODUCT_TYPE, @@FINAL_JUDGEMENT, @@TIME_CD, @@CNT;

		WHILE @@@FETCH_STATUS = 0  
		BEGIN  

			IF(@@FINAL_JUDGEMENT = 'OK') BEGIN
				SET @@COL_NAME = 'OK_1_CNT';
			END 
			ELSE IF(@@FINAL_JUDGEMENT = 'OK MTL') BEGIN
				SET @@COL_NAME = 'OK_MTL_1_CNT';
			END
			ELSE IF(@@FINAL_JUDGEMENT = 'REPAIR') BEGIN
				SET @@COL_NAME = 'REP_1_CNT';
			END
			ELSE IF(@@FINAL_JUDGEMENT = 'NG') BEGIN
				SET @@COL_NAME = 'NG_1_CNT';
			END
			IF(@@PRODUCT_TYPE = '2TR') BEGIN
				SET @@COL_NAME = REPLACE(@@COL_NAME, '1', '2');
			END

			PRINT @@CNT;
			SET @@UPDATE_SQL = 'UPDATE #TB_T_FINAL_RESULT SET ' + @@COL_NAME +' = '''+ @@CNT +''' WHERE TIME_CD = '''+ @@TIME_CD +'''';
			EXEC (@@UPDATE_SQL);

			FETCH NEXT FROM _CURR_SUMMARY_STATUS INTO @@PRODUCT_TYPE, @@FINAL_JUDGEMENT, @@TIME_CD, @@CNT;
		END 

		CLOSE _CURR_SUMMARY_STATUS  
	DEALLOCATE _CURR_SUMMARY_STATUS

END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE()
END CATCH

SELECT * FROM #TB_T_FINAL_RESULT 
ORDER BY CAST(SUBSTRING(TIME_CD, 0, LEN(TIME_CD)) AS INT);