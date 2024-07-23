﻿ SELECT DISTINCT
	TB_R_MELTING_SHIFT_SUM.MELTING_SUM_ID
	--, 'FC' AS KWH_TYPE
	, COALESCE(
	(SELECT TOP 1 CASE WHEN TB_R_MELTING.PRODUCT_TYPE = 'TR' THEN 'FC' WHEN TB_R_MELTING.PRODUCT_TYPE = 'Crank' OR TB_R_MELTING.PRODUCT_TYPE = 'Camshaft' THEN 'FCD' END FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD AND FURNACE_CD = TB_R_MELTING_SHIFT_KWH.FURNACE_NO ORDER BY MELTING_ID),
	(SELECT TOP 1 CASE WHEN TB_R_MELTING.PRODUCT_TYPE = 'TR' THEN 'FC' WHEN TB_R_MELTING.PRODUCT_TYPE = 'Crank' OR TB_R_MELTING.PRODUCT_TYPE = 'Camshaft' THEN 'FCD' END FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD ORDER BY MELTING_ID)
	) AS KWH_TYPE
	, CONCAT(COALESCE((SELECT SUM(TAPPING_WEIGHT) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD AND FURNACE_CD = TB_R_MELTING_SHIFT_KWH.FURNACE_NO), 0), '/1000') AS TONAGE_FORMULA
	, CONCAT(COALESCE((SELECT CAST(SUM(CASE WHEN TB_R_MELTING.PRODUCT_TYPE = 'TR' THEN TAPPING_WEIGHT END)/1000 AS DECIMAL(18, 2)) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD), 0), '/1000') AS TONAGE_FC_FORMULA
	, CONCAT(COALESCE((SELECT CAST(SUM(CASE WHEN TB_R_MELTING.PRODUCT_TYPE = 'Crank' OR TB_R_MELTING.PRODUCT_TYPE = 'Camshaft' THEN TAPPING_WEIGHT END)/1000 AS DECIMAL(18, 2)) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD), 0), '/1000') AS TONAGE_FCD_FORMULA
	, COALESCE((SELECT SUM(TAPPING_WEIGHT) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD AND FURNACE_CD = TB_R_MELTING_SHIFT_KWH.FURNACE_NO), 0) AS TAPPING_WEIGHT
	, (SELECT CAST(SUM(TAPPING_WEIGHT)/1000 AS DECIMAL(18, 2)) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD AND FURNACE_CD = TB_R_MELTING_SHIFT_KWH.FURNACE_NO) AS TONAGE
	, CAST(DATEDIFF(MINUTE
		, (SELECT TOP 1 MIN(TAPPING_TIME) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD AND FURNACE_CD = TB_R_MELTING_SHIFT_KWH.FURNACE_NO)
		, (SELECT TOP 1 MAX(TAPPING_TIME) FROM TB_R_MELTING WHERE PRODUCTION_DT = TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT AND SHIFT_CD = TB_R_MELTING_SHIFT_SUM.SHIFT_CD AND FURNACE_CD = TB_R_MELTING_SHIFT_KWH.FURNACE_NO)
	)/60.0 AS DECIMAL(18, 2))  AS RUN_HOUR
	, TB_R_MELTING_SHIFT_SUM.NO_OF_FURNACE
	, (SELECT COUNT(1) FROM TB_M_SYSTEM WHERE SYSTEM_TYPE = 'NO_OF_FURNACE') AS NO_OF_FURNACE_MAX
	, TB_R_MELTING_SHIFT_KWH.FURNACE_NO
	, TB_R_MELTING_SHIFT_KWH.KWH_PRODUCTION
	, TB_R_MELTING_SHIFT_KWH.KWH_AH
	, TB_R_MELTING_SHIFT_KWH.KWH_START
	, TB_M_USER.NAME
-- 		, TB_R_MELTING_SHIFT_OPR.OPR_NM
	, TB_R_MELTING_SHIFT_SUM.FORKLIFT
	, U.NAME AS FORKLIFT_NM
	, TB_R_MELTING_SHIFT_SUM.CRANE
	, UU.NAME AS CRANE_NM
	, TB_R_MELTING_SHIFT_SUM.BALING_MC
	, UUU.NAME AS BALING_MC_NM
FROM TB_R_MELTING_SHIFT_SUM
LEFT JOIN TB_R_MELTING_SHIFT_KWH ON TB_R_MELTING_SHIFT_SUM.MELTING_SUM_ID = TB_R_MELTING_SHIFT_KWH.MELTING_SUM_ID
LEFT JOIN TB_R_MELTING_SHIFT_OPR ON TB_R_MELTING_SHIFT_OPR.MELTING_SUM_ID = TB_R_MELTING_SHIFT_SUM.MELTING_SUM_ID AND TB_R_MELTING_SHIFT_OPR.FURNACE_NO = TB_R_MELTING_SHIFT_KWH.FURNACE_NO
LEFT JOIN TB_M_USER ON TB_M_USER.USER_ID = TB_R_MELTING_SHIFT_OPR.OPR_NM
LEFT JOIN TB_M_USER U ON U.USER_ID = TB_R_MELTING_SHIFT_SUM.FORKLIFT
LEFT JOIN TB_M_USER UU ON UU.USER_ID = TB_R_MELTING_SHIFT_SUM.CRANE
LEFT JOIN TB_M_USER UUU ON UUU.USER_ID = TB_R_MELTING_SHIFT_SUM.BALING_MC
WHERE TB_R_MELTING_SHIFT_SUM.PRODUCTION_DT = @_PRODUCTION_DT AND TB_R_MELTING_SHIFT_SUM.SHIFT_CD = @_SHIFT_CD
and TB_R_MELTING_SHIFT_KWH.FURNACE_NO = 1
ORDER BY TB_R_MELTING_SHIFT_KWH.FURNACE_NO;