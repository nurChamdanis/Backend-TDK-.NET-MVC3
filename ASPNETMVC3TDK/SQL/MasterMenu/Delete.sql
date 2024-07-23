DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

IF OBJECT_ID('TEMPDB..#MenuDataTemp') IS NOT NULL DROP TABLE #MenuDataTemp
-- Insert menu data into temporary table
--INSERT (APP_ID, MENU_ID, MENU_PARENT)
SELECT JSON_VALUE(value, '$.APP_ID') AS APP_ID,
        JSON_VALUE(value, '$.MENU_ID') AS MENU_ID,
        JSON_VALUE(value, '$.MENU_PARENT') AS MENU_PARENT
		INTO #MenuDataTemp 
FROM OPENJSON(@MenuData)

if exists (select A.* from TB_M_AUTHORIZATION A where exists (select B.* 
from #MenuDataTemp B where B.APP_ID = A.[APPLICATION] and B.MENU_ID = A.[FUNCTION])) begin
    GOTO ALREADY_USED_AUTHORIAZATION
end

-- Delete records from TB_M_MENU based on conditions
-- Drop temporary table
DELETE FROM TB_M_MENU
WHERE EXISTS (
    SELECT 1
    FROM #MenuDataTemp
    WHERE TB_M_MENU.APP_ID = #MenuDataTemp.APP_ID
        AND TB_M_MENU.MENU_ID = #MenuDataTemp.MENU_ID
        AND (TB_M_MENU.MENU_PARENT = #MenuDataTemp.MENU_PARENT OR (TB_M_MENU.MENU_PARENT IS NULL AND #MenuDataTemp.MENU_PARENT IS NULL))
);

DELETE FROM TB_M_FUNCTION WHERE EXISTS (SELECT TOP 1 B.* FROM #MenuDataTemp B 
where B.APP_ID = [APPLICATION] and B.MENU_ID = [ID])

SET @@info = 'norm';
SET @@result = 'true';
SET @@messages = 'Data deleted successfully';
GOTO FINAL_LINE

ALREADY_USED_AUTHORIAZATION:
    SET @@info = 'warn';
    SET @@result = 'false';
    SET @@messages = 'still used on AUTHORIZATION';
    GOTO FINAL_LINE

FINAL_LINE:
    select @@info as msgInfo, @@result as result, @@messages as msg;