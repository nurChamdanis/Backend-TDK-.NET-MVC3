DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE 
@@ROLE VARCHAR(MAX),
@@FUNCTION VARCHAR(MAX),
@@CREATED_BY VARCHAR(MAX),
@@APPALIAS VARCHAR(MAX),
@@USERNAME VARCHAR(100);

SET @@ROLE = @P_ROLE;
SET @@FUNCTION = @P_FUNCTION;
SET @@CREATED_BY = @P_USERNAME;
SET @@APPALIAS = @P_APPALIAS;

DECLARE username_cursor CURSOR FOR
SELECT DISTINCT USERNAME 
FROM [dbo].[TB_M_AUTHORIZATION] 
WHERE APPLICATION = @@APPALIAS

OPEN username_cursor

FETCH NEXT FROM username_cursor INTO @@USERNAME

WHILE @@@FETCH_STATUS = 0
BEGIN
    BEGIN TRY
        IF ((SELECT COUNT(1) FROM TB_M_AUTHORIZATION WHERE APPLICATION = @@APPALIAS AND USERNAME = @@USERNAME AND ROLE = @@ROLE AND [FUNCTION] = @@FUNCTION) > 0) BEGIN
            SET @@info = 'failed';
            SET @@result = 'false';
            SET @@messages = 'Function already exist!';
        END ELSE BEGIN
            INSERT INTO [dbo].[TB_M_AUTHORIZATION]
            (
                USERNAME
                , APPLICATION
                , ROLE
                , [FUNCTION]
                , FEATURE
                , QUALIFIER_KEY
                , QUALIFIER_VALUE
                , CREATED_BY
                , CREATED_DATE
            )
            VALUES
            (
                @@USERNAME,
                @@APPALIAS,
                @@ROLE,
                @@FUNCTION,
                '',
                '',
                '',
                @@CREATED_BY, 
                (SELECT GETDATE())
            )
            SET @@info = 'success';
			SET @@result = 'true';
			SET @@messages = 'Data assigned successfully';
        END
    END TRY
    BEGIN CATCH
        SET @@info = 'failed';
        SET @@result = 'false';
        SET @@messages = 'An error occurred while trying to insert the data.';
    END CATCH

    FETCH NEXT FROM username_cursor INTO @@USERNAME
END

CLOSE username_cursor
DEALLOCATE username_cursor

select @@info as msgInfo, @@result as result, @@messages as msg;

