DECLARE
	@APP_ID varchar(50),
	@MENU_ID varchar(50),
    @MENU_PARENT varchar(50);

SET @APP_ID = 'MyApp';
SET @MENU_ID = 'Menu001';
SET @MENU_PARENT = 'Parent001';

SELECT * FROM TB_M_MENU
WHERE APP_ID = @APP_ID
AND MENU_ID = @MENU_ID
AND MENU_PARENT = @MENU_PARENT;