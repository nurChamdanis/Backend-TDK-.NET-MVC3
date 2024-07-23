DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE 
@@ID VARCHAR(MAX),
@@NAME VARCHAR(MAX),
@@TYPE VARCHAR(MAX),
@@RUNTIME VARCHAR(MAX),
@@DESCRIPTION VARCHAR(MAX),
@@MODE VARCHAR(MAX),
@@USERNAME VARCHAR(MAX);

-- CHANGE THE @ WITH EXPECTED VALUE
SET @@ID = @P_ID;
SET @@NAME = @P_NAME;
SET @@TYPE = @P_TYPE;
SET @@RUNTIME = @P_RUNTIME;
SET @@DESCRIPTION = @P_DESCRIPTION;
SET @@MODE = @P_MODE; -- ADD OR EDIT
SET @@USERNAME = @P_USERNAME;

BEGIN TRY
		IF(COALESCE(@@MODE, 'ADD') = 'ADD') BEGIN

			IF ((SELECT COUNT(1) FROM TB_M_APPLICATION WHERE ID = @@ID) > 0) BEGIN
				SET @@info = 'failed';
				SET @@result = 'false';
				SET @@messages = 'Failed to save, Feature ID already exist!';
			END ELSE BEGIN

				INSERT INTO [dbo].[TB_M_APPLICATION]
					(
						 ID
						, NAME
						, TYPE
						, RUNTIME
						, DESCRIPTION
						, CREATED_BY
						, CREATED_DATE
					)
				 VALUES
					(
						@@ID
						, @@NAME
						, @@TYPE
						, @@RUNTIME
						, @@DESCRIPTION
						, @@USERNAME
						, (SELECT GETDATE())
					);
					SET @@info = 'success';
					SET @@result = 'true';
					SET @@messages = 'Data saved successfully';
				END

				
		END ELSE 
		BEGIN

			UPDATE [dbo].[TB_M_APPLICATION] SET 
				
				[NAME] = @@NAME, 
				[TYPE] = @@TYPE,
				[RUNTIME] = @@RUNTIME,
				[DESCRIPTION] = @@DESCRIPTION, 
				[CHANGED_BY] = @@USERNAME, 
				[CHANGED_DATE] = (SELECT GETDATE()) 
			WHERE 
               [ID] = @@ID;
			
			
			SET @@info = 'success';
			SET @@result = 'true';
			SET @@messages = 'Data updated successfully';
		END
	END TRY
BEGIN CATCH
	SET @@info = 'failed';
	SET @@result = 'false';
	SET @@messages = 'Failed to save';
END CATCH
select @@info as msgInfo, @@result as result, @@messages as msg;
