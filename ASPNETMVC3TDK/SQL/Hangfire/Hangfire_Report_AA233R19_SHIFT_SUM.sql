﻿SELECT 
	TB_R_QC_SHIFT_SUM.QC_SHIFT_ID
	, TB_R_QC_SHIFT_SUM.LEAVE
	, TB_R_QC_SHIFT_SUM.SICK
	, TB_R_QC_SHIFT_SUM.IZIN_TRAINING
	, TB_R_QC_SHIFT_SUM.TD_PC
	, TB_R_QC_SHIFT_SUM.OUTLINE
	, TB_R_QC_SHIFT_SUM.A1
	, TB_R_QC_SHIFT_SUM.B1
	, TB_R_QC_SHIFT_SUM.C1
	, TB_R_QC_SHIFT_SUM.D1
	, TB_R_QC_SHIFT_SUM.E1
	, TB_R_QC_SHIFT_SUM.F1
	, TB_R_QC_SHIFT_SUM.G1
	, TB_R_QC_SHIFT_SUM.H1
	, TB_R_QC_SHIFT_SUM.I1
	, TB_R_QC_SHIFT_SUM.J1
	, TB_R_QC_SHIFT_SUM.A2
	, TB_R_QC_SHIFT_SUM.B2
	, TB_R_QC_SHIFT_SUM.C2
	, TB_R_QC_SHIFT_SUM.D2
	, TB_R_QC_SHIFT_SUM.E2
	, TB_R_QC_SHIFT_SUM.F2
	, TB_R_QC_SHIFT_SUM.G2
	, TB_R_QC_SHIFT_SUM.H2
	, TB_R_QC_SHIFT_SUM.I2
	, TB_R_QC_SHIFT_SUM.J2																	
	, TB_R_QC_SHIFT_SUM.KWH_START_PD																	
	, TB_R_QC_SHIFT_SUM.KWH_START_MDB																	
	, TB_R_QC_SHIFT_SUM.KWH_START_IDC																	
	, TB_R_QC_SHIFT_SUM.KWH_FINISH_PD																	
	, TB_R_QC_SHIFT_SUM.KWH_FINISH_MDB																	
	, TB_R_QC_SHIFT_SUM.KWH_FINISH_IDC																	
FROM TB_R_QC_SHIFT_SUM
WHERE TB_R_QC_SHIFT_SUM.PRODUCTION_DT = @_PRODUCTION_DT AND TB_R_QC_SHIFT_SUM.SHIFT_CD = @_SHIFT_CD;