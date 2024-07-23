DECLARE @@info VARCHAR(MAX);
DECLARE @@result int;
DECLARE @@messages VARCHAR(MAX);


DECLARE @@NOREG VARCHAR(MAX) = @P_NOREG;

DECLARE @@DESCRIPTION VARCHAR(MAX) = @P_DESCRIPTION;


BEGIN TRY 
	BEGIN
		UPDATE [PDI].[TB_M_PROFILE_DESCRIPTION] 
		SET 
			DESCRIPTION = @@DESCRIPTION,
			UPDATED_BY = @@NOREG,
			UPDATED_DT = CURRENT_TIMESTAMP
		WHERE NOREG = ''+@@NOREG+'';

		---
		SET @@info = 'SUCCESS';
		SET @@result = 200;
		SET @@messages = ''+@@DESCRIPTION+'';
		--

	END; 
END TRY 
BEGIN CATCH
	
		---
		SET @@info = 'FAILED !';
		SET @@result = 500;
		SET @@messages = ERROR_MESSAGE();
		--

END CATCH;




SELECT @@info AS msgInfo, @@result AS result, @@messages AS msg;