DECLARE  
    @@QUERY VARCHAR(MAX),
    @@RECENT VARCHAR(MAX) = @P_RECENT,
    @@SEARCH VARCHAR(MAX) = @P_SEARCH,
    @@NOREG VARCHAR(MAX) = @P_NOREG,
    @@DIVISION VARCHAR(MAX) = @P_DIVISION,
    @@DEPARTMENT VARCHAR(MAX) = @P_DEPARTEMENT,
    @@SECTION VARCHAR(MAX) = @P_SECTION,
    @@LINE VARCHAR(MAX) = @P_LINE,
    @@GROUP VARCHAR(MAX) = @P_GROUP, 
    @@CLASS VARCHAR(MAX) = @P_CLASS, 
    @@POSITION VARCHAR(MAX) = @P_POSITION, 
    @@SKILLS VARCHAR(MAX) = @P_SKILLS, 
    @@SUPER VARCHAR(MAX) = @P_SUPER,
    @@ALL VARCHAR(MAX) = @P_ALL,
    @@CALL_SP VARCHAR(MAX),
    @@OFFSET VARCHAR(MAX) = @P_OFFSET,
    @@FETCH VARCHAR(MAX) = @P_FETCH,
	@@URL VARCHAR(MAX) = '';  

SET @@URL = (
    SELECT 
        SYSTEM_VALUE
    FROM 
        DBO.TB_M_SYSTEM
    WHERE 
            FUNCTION_ID = 'ENVIRONMENT_PDI'
        AND 
            SYSTEM_CD = 'URL_LINK'
);

IF OBJECT_ID('tempdb..##TABLE_TEMP_SQL') IS NOT NULL
BEGIN
    DROP TABLE ##TABLE_TEMP_SQL;
END;


DECLARE @@UserPositionTable TABLE (
	SEQ VARCHAR(50), 
	STEP VARCHAR(50), 
	ORG_PARENT VARCHAR(50), 
	ORG_ID VARCHAR(50), 
	ORG_TITLE VARCHAR(50), 
	LEVEL_ID VARCHAR(50), 
	NOREG VARCHAR(50), 
	EDITNAME VARCHAR(50), 
	JOB_CODE VARCHAR(50),
    JOB_TITLE VARCHAR(50),
    POSITION_ABBR VARCHAR(50),
    POSITION_CODE VARCHAR(50),
    POSITION_TITLE VARCHAR(50),
    POSITION_LEVEL VARCHAR(50),
    POSITION_LEVEL_ID VARCHAR(50),
    CLASS VARCHAR(50),
    CLASS_LEVEL VARCHAR(50),
    USERNAME VARCHAR(50),
    VALID_FROM VARCHAR(50),
    VALID_TO VARCHAR(50),
    DELIMIT_DATE VARCHAR(50),
    STATUS VARCHAR(50),
    PRIORITY VARCHAR(50),
    POSITION_PERCENT VARCHAR(50),
    CHIEF_TAG VARCHAR(50)
);
IF(@@ALL = 'TRUE')
BEGIN
    SET @@QUERY = '
        SELECT 
            DISTINCT
            A.NOREG,
            A.JOB_TITLE,
            A.PERSONNEL_NAME AS NAME,
            CONCAT('''+@@URL+''', C.PHOTO_DIRECTORY, C.PHOTO_FILENAME) AS PHOTO
        FROM vw_Organization A
        JOIN DBO.TB_M_PROFILE_HEADER C on A.NOREG = C.NOREG
        JOIN PDI.TB_M_ROTATION_HISTORY D ON D.NOREG = A.NOREG AND D.ORG_ID = A.ORG_ID
        LEFT JOIN PDI.TB_M_PROJECT_ASSIGNMENT E ON E.NOREG = A.NOREG
        LEFT JOIN PDI.TB_M_PROFILE_ICT_HISTORY F ON F.NOREG = A.NOREG
        LEFT JOIN PDI.TB_M_PROFILE_TRAINING_HISTORY G ON G.NOREG = A.NOREG
        WHERE 1=1
    ';
END ELSE
BEGIN
    SET @@CALL_SP = 'EXEC PDI.sp_GetUserPosition_NEW ''' + @@NOREG + ''', @@SUPER = ''' + @@SUPER + ''''
    EXECUTE (@@CALL_SP)

    SET @@QUERY = '
        WITH CTE AS (
            SELECT *,
                ROW_NUMBER() OVER (PARTITION BY NOREG ORDER BY (SELECT NULL)) AS RowNum
            FROM ##TEMP_ORGPOSITION
        )

        SELECT 
            DISTINCT 
            A.NOREG, 
            A.JOB_TITLE,
            B.NAME,
            CONCAT('''+@@URL+''', B.PHOTO_DIRECTORY, B.PHOTO_FILENAME) AS PHOTO
        FROM CTE A
        JOIN TB_M_PROFILE_HEADER B ON B.NOREG = A.NOREG
        JOIN vw_Organization O ON B.NOREG = O.NOREG
        JOIN PDI.TB_M_ROTATION_HISTORY D ON D.NOREG = O.NOREG AND D.ORG_ID = O.ORG_ID
        LEFT JOIN PDI.TB_M_PROJECT_ASSIGNMENT E ON E.NOREG = O.NOREG
        LEFT JOIN PDI.TB_M_PROFILE_ICT_HISTORY F ON F.NOREG = O.NOREG
        LEFT JOIN PDI.TB_M_PROFILE_TRAINING_HISTORY G ON G.NOREG = O.NOREG
        WHERE RowNum = 1 
        AND A.NOREG != '+@@NOREG+' ';
    END

    IF (@@RECENT = 'RECENT') BEGIN
        SET @@QUERY = @@QUERY + ' AND GETDATE() <= D.VALID_TO AND D.POSITION_PERCENT = 100 ';
    END ELSE
    BEGIN
        SET @@QUERY = @@QUERY + ' AND GETDATE() > D.VALID_TO ';
    END

    IF (@@DIVISION != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.DIVISION_NAME = ''' + @@DIVISION + '''';
    END

    IF (@@DEPARTMENT != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.DEPARTMENT_NAME = ''' + @@DEPARTMENT + '''';
    END

    IF (@@SECTION != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.SECTION_NAME = ''' + @@SECTION + '''';
    END

    IF (@@LINE != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.LINE_NAME = ''' + @@LINE + '''';
    END

    IF (@@GROUP != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.GROUP_NAME = ''' + @@GROUP + '''';
    END

    IF (@@CLASS != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.CLASS = ''' + @@CLASS + '''';
    END

    IF (@@POSITION != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND D.POSITION_LEVEL = ''' + @@POSITION + '''';
    END

    IF (@@SEARCH != '') BEGIN
        IF(@@ALL = 'TRUE') 
        BEGIN
            SET @@QUERY = @@QUERY + ' AND (
                    A.NOREG LIKE ''%' + @@SEARCH + '%''
                OR
                    A.PERSONNEL_NAME LIKE ''%' + @@SEARCH + '%''
            )';
        END ELSE
        BEGIN
            SET @@QUERY = @@QUERY + ' AND (
                    A.NOREG LIKE ''%' + @@SEARCH + '%''
                OR
                    B.NAME LIKE ''%' + @@SEARCH + '%''
            )';
        END
    END

    IF (@@SKILLS != '') BEGIN
        SET @@QUERY = @@QUERY + ' AND (
                D.ACCOMPLISHMENT LIKE ''%' + @@SKILLS + '%''
            OR
                E.ACCOMPLISHMENT LIKE ''%' + @@SKILLS + '%''
            OR
                F.ACCOMPLISHMENT LIKE ''%' + @@SKILLS + '%''
            OR
                G.SKILL LIKE ''%' + @@SKILLS + '%''
        )';
    END

    IF(@@ALL = 'TRUE') BEGIN
        SET @@QUERY = @@QUERY + ' ORDER BY A.PERSONNEL_NAME';
    END ELSE
    BEGIN
        SET @@QUERY = @@QUERY + ' ORDER BY B.NAME';
    END

    IF (@@OFFSET is not null AND @@FETCH is not null) BEGIN
        SET @@QUERY = @@QUERY + ' OFFSET '+@@OFFSET+' ROWS FETCH NEXT '+@@FETCH+' ROWS ONLY;';
    END

    -- Optionally save the SQL query into a temp table for later reference
    SELECT @@QUERY AS SQL_QUERY INTO ##TABLE_TEMP_SQL;

EXECUTE (@@QUERY);

