DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);


DECLARE 
@@APP_ID VARCHAR(MAX),
@@MENU_ID VARCHAR(MAX),
@@MENU_PARENT VARCHAR(MAX),
@@MENU_TEXT VARCHAR(MAX),
@@MENU_TIPS VARCHAR(MAX),
@@IS_ACTIVE VARCHAR(MAX),
@@VISIBILITY VARCHAR(MAX),
@@URL VARCHAR(MAX),
@@GLYPH VARCHAR(MAX),
@@CHANGED_BY VARCHAR(MAX),
@@CHANGED_DT VARCHAR(MAX),
@@SEPARATOR VARCHAR(MAX),
@@TARGET VARCHAR(MAX);

BEGIN TRY
    SET @@APP_ID = @APP_ID;
    SET @@MENU_ID = @MENU_ID;
    SET @@MENU_PARENT = @MENU_PARENT;
    SET @@MENU_TEXT = @MENU_TEXT;
    SET @@MENU_TIPS = @MENU_TIPS;
    SET @@IS_ACTIVE = @IS_ACTIVE;
    SET @@VISIBILITY = @VISIBILITY;
    SET @@URL = @URL;
    SET @@GLYPH = @GLYPH;
    SET @@CHANGED_BY = @CHANGED_BY;
    SET @@CHANGED_DT = getdate();
    SET @@SEPARATOR = @SEPARATOR;
    SET @@TARGET = @TARGET;

    -- Lakukan proses UPDATE di sini
    UPDATE TB_M_MENU
    SET MENU_TEXT = @@MENU_TEXT,
        MENU_TIPS = @@MENU_TIPS,
        IS_ACTIVE = @@IS_ACTIVE,
        VISIBILITY = @@VISIBILITY,
        URL = @@URL,
        GLYPH = @@GLYPH,
        CHANGED_BY = @@CHANGED_BY,
        CHANGED_DT = @@CHANGED_DT,
        SEPARATOR = @@SEPARATOR,
        TARGET = @@TARGET
    WHERE APP_ID = @@APP_ID
    AND MENU_ID = @@MENU_ID;

    if exists (select * from TB_M_FUNCTION WHERE [APPLICATION] = @@APP_ID and ID = @@MENU_ID)begin
        UPDATE TB_M_FUNCTION
        SET [NAME] = @@MENU_TEXT,[DESCRIPTION]=@@MENU_TIPS,CHANGED_BY = @@CHANGED_BY,CHANGED_DATE = @@CHANGED_DT
        WHERE [APPLICATION] = @@APP_ID and ID = @@MENU_ID
    end
    else begin
        INSERT INTO TB_M_FUNCTION([APPLICATION],ID,[NAME],[DESCRIPTION],CREATED_BY,CREATED_DATE)
        VALUES(@@APP_ID, @@MENU_ID,@@MENU_TEXT,@@MENU_TIPS,@@CHANGED_DT, GETDATE())
    end

    SET @@info = 'success';
    SET @@result = 'true';
    SET @@messages = 'Data updated successfully';
END TRY
BEGIN CATCH
    -- Tangani kesalahan di sini
    SET @@info = 'error';
    SET @@result = 'false';
    SET @@messages = ERROR_MESSAGE();
END CATCH;
select @@info as info, @@result as result, @@messages as messages;