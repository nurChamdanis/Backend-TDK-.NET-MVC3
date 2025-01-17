﻿DECLARE @@info VARCHAR(MAX);
DECLARE @@result INT;
DECLARE @@messages VARCHAR(MAX);


DECLARE @@CANCEL_FLAG VARCHAR(MAX) = 1;
DECLARE @@CANCEL_BY VARCHAR(MAX) = '00111208';
DECLARE @@CANCEL_DT VARCHAR(MAX) = CURRENT_TIMESTAMP;
DECLARE @@UPDATE_BY VARCHAR(MAX) = '00111208';
DECLARE @@UPDATE_DT VARCHAR(MAX) = CURRENT_TIMESTAMP;


DECLARE  
    @@TRANSACTION_ID VARCHAR(MAX) = @P_TRANSACTION_ID; 

BEGIN TRY 
	BEGIN
		UPDATE 
				PDI.TB_R_SUBMISSION_PROJECT_ASSIGNMENT
			SET
				CANCEL_FLAG = @@CANCEL_FLAG,
				CANCEL_BY = @@CANCEL_BY,
				CANCEL_DATE = @@CANCEL_DT,
				UPDATED_BY = @@UPDATE_BY,
				UPDATED_DT = @@UPDATE_DT
			WHERE 
				TRANSACTION_ID =''+@@TRANSACTION_ID+'' ;

		SET @@info = 'success';
		SET @@result = 200;
		SET @@messages = 'success to delete';  
	END;
END TRY
BEGIN CATCH
	SET @@info = 'failed';
	SET @@result = 500;
	SET @@messages = 'Failed to delete';    
END CATCH

select @@info as msgInfo, @@result as result, @@messages as msg;