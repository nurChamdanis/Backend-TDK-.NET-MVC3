DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE @@temp_criteria TABLE( ID VARCHAR(MAX));
DECLARE @@P_IDS VARCHAR(MAX) = @P_IDS

BEGIN TRY
    BEGIN TRANSACTION;
    
    INSERT INTO @@temp_criteria (ID)
    SELECT ID
    FROM OPENJSON(@@P_IDS)
    WITH (
        ID VARCHAR(MAX) '$.ID'
    );

    DELETE FROM TB_M_APPLICATION
    WHERE  ID IN (SELECT ID FROM @@temp_criteria);

    SET @@info = 'success';
	SET @@result = 'true';
	SET @@messages = 'Data deleted successfully';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;

    -- Handle the error appropriately, whether it's logging, raising, or printing the error message.
    SET @@info = 'failed';
	SET @@result = 'false';
	SET @@messages = 'Failed to delete';
END CATCH;


SELECT @@info as msgInfo, @@result as result, @@messages as msg;