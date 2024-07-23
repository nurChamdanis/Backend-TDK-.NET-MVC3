﻿SELECT 
	TB_R_MELTING_SHIFT_LINE.MELTING_SUM_ID
	, SUM(TB_R_MELTING_SHIFT_LINE.POURING) AS POURING
	, SUM(TB_R_MELTING_SHIFT_LINE.MOULDING) AS MOULDING
	, SUM(TB_R_MELTING_SHIFT_LINE.ANALYSIS) AS ANALYSIS
	, SUM(TB_R_MELTING_SHIFT_LINE.CORE_MAKING) AS CORE_MAKING
FROM TB_R_MELTING_SHIFT_LINE
WHERE MELTING_SUM_ID = @_MELTING_SUM_ID
GROUP BY
	TB_R_MELTING_SHIFT_LINE.MELTING_SUM_ID
	, TB_R_MELTING_SHIFT_LINE.POURING
	, TB_R_MELTING_SHIFT_LINE.MOULDING
	, TB_R_MELTING_SHIFT_LINE.ANALYSIS
	, TB_R_MELTING_SHIFT_LINE.CORE_MAKING