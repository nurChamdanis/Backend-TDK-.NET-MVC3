DECLARE  
    @@QUERY VARCHAR(MAX),
    @@NOREG VARCHAR(MAX) = @P_NOREG,
    @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`'), 
    @@NOREG_TEMP VARCHAR(MAX),
    @@CALL_SP VARCHAR(MAX);

-- SET @@CALL_SP = 'EXEC [PDI].[sp_GetUserPositionforapi] '''+@@NOREG+''' ';  

-- SET @@QUERY = ' SELECT * FROM ##TEMP_FINAL_POSITION';

-- EXECUTE (@@CALL_SP);  
-- EXECUTE (@@QUERY); 

SET @@CALL_SP = 'EXEC [PDI].[sp_getEmployeeSuperior] '''+@@NOREG+''' ';  
EXECUTE (@@CALL_SP);  
