DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE  
    @@TRANSACTION_ID VARCHAR(MAX) = @P_TRANSACTION_ID; 

BEGIN TRY 
	BEGIN
		UPDATE 
			PDI.TB_R_SUBMISSION_EDUCATION
			SET
				CANCEL_FLAG = 1
				, CANCEL_BY = '00111208'
				, CANCEL_DATE = GETDATE()
				, UPDATED_DT = GETDATE()
				, UPDATED_BY = '00111208'
			WHERE 
				TRANSACTION_ID =''+@@TRANSACTION_ID+'' ;

		SET @@info = 'success';
		SET @@result = 200;
		SET @@messages = 'success to delete';  
	END;
END TRY
BEGIN CATCH
	SET @@info = 'failed';
	SET @@result = 0;
	SET @@messages = 'Failed to delete';    
END CATCH

select @@info as msgInfo, @@result as result, @@messages as msg;