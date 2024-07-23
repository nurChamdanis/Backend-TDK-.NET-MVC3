DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE 
@@ID VARCHAR(MAX),
@@NAME VARCHAR(MAX),
@@DESCRIPTION VARCHAR(MAX),
@@MODE VARCHAR(MAX),
@@USERNAME VARCHAR(MAX),
@@APPALIAS VARCHAR(MAX);

-- CHANGE THE @ WITH EXPECTED VALUE
SET @@ID = @P_ID;
SET @@NAME = @P_NAME;
SET @@DESCRIPTION = @P_DESCRIPTION;
SET @@MODE = @P_MODE; -- ADD OR EDIT
SET @@USERNAME = @P_USERNAME;
SET @@APPALIAS = @P_APPALIAS;

BEGIN TRY
		IF(COALESCE(@@MODE, 'ADD') = 'ADD') BEGIN

			IF ((SELECT COUNT(1) FROM TB_M_FEATURE WHERE APPLICATION = @@APPALIAS AND ID = @@ID) > 0) BEGIN
				SET @@info = 'failed';
				SET @@result = 'false';
				SET @@messages = 'Failed to save, Feature ID already exist!';
			END ELSE BEGIN

				INSERT INTO [dbo].[TB_M_FEATURE]
					(
						APPLICATION
						, ID
						, NAME
						, DESCRIPTION
						, CREATED_BY
						, CREATED_DATE
					)
				 VALUES
					(
						@@APPALIAS
						, @@ID
						, @@NAME
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

			UPDATE [dbo].[TB_M_FEATURE] SET 
				[NAME] = @@NAME, 
				[DESCRIPTION] = @@DESCRIPTION, 
				[CHANGED_BY] = @@USERNAME, 
				[CHANGED_DATE] = (SELECT GETDATE()) 
			WHERE 
				[APPLICATION] = @@APPALIAS AND [ID] = @@ID;
			
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
