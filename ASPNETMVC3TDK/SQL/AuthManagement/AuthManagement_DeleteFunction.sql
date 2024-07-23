DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE @@temp_criteria TABLE(APPLICATION VARCHAR(MAX), ROLE VARCHAR(MAX), [FUNCTION] VARCHAR(MAX));
DECLARE @@P_FUNCTIONS VARCHAR(MAX) = @P_FUNCTIONS

BEGIN TRY
    BEGIN TRANSACTION;
    
   INSERT INTO @@temp_criteria (APPLICATION, ROLE, [FUNCTION])
    SELECT APPLICATION, ROLE, [FUNCTION]
    FROM OPENJSON(@@P_FUNCTIONS)
    WITH (
        APPLICATION VARCHAR(MAX) '$.APPLICATION',
        ROLE VARCHAR(MAX) '$.ROLE',
        [FUNCTION] VARCHAR(MAX) '$.FUNCTION'
    );

    DELETE FROM TB_M_AUTHORIZATION
    WHERE APPLICATION IN (SELECT APPLICATION FROM @@temp_criteria)
    AND ROLE IN (SELECT ROLE FROM @@temp_criteria)
    AND [FUNCTION] IN (SELECT [FUNCTION] FROM @@temp_criteria);

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