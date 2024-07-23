DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE @@temp_criteria TABLE(APPLICATION VARCHAR(MAX), ID VARCHAR(MAX));
DECLARE @@P_IDS VARCHAR(MAX) = @P_IDS

BEGIN TRY
    BEGIN TRANSACTION;
    
    INSERT INTO @@temp_criteria (APPLICATION, ID)
    SELECT APPLICATION, ID
    FROM OPENJSON(@@P_IDS)
    WITH (
        APPLICATION VARCHAR(MAX) '$.APPLICATION',
        ID VARCHAR(MAX) '$.ID'
    );

    DELETE FROM TB_M_FEATURE
    WHERE APPLICATION IN (SELECT APPLICATION FROM @@temp_criteria)
    AND ID IN (SELECT ID FROM @@temp_criteria);

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