﻿
SELECT 
	TB_R_MELTING_SHIFT_ADJ.FURNACE_NO
	, TB_R_MELTING_SHIFT_ADJ.C
	, TB_R_MELTING_SHIFT_ADJ.SI
	, TB_R_MELTING_SHIFT_ADJ.MN
	, TB_R_MELTING_SHIFT_ADJ.ZN
FROM
	TB_R_MELTING_SHIFT_ADJ
WHERE 
	TB_R_MELTING_SHIFT_ADJ.MELTING_SUM_ID = @_MELTING_SUM_ID