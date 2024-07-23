﻿DECLARE 
    @@QUERY VARCHAR(MAX),
    @@NOREG VARCHAR(MAX) = @P_NOREG,
    @@SEARCH VARCHAR(MAX) = @P_SEARCH,
    @@DIVISION VARCHAR(MAX) = @P_DIVISION,
    @@DEPARTMENT VARCHAR(MAX) = @P_DEPARTMENT,
    @@SORT VARCHAR(MAX) = @P_SORT,
    @@OFFSET VARCHAR(MAX) = @P_OFFSET,
    @@FETCH VARCHAR(MAX) = @P_FETCH,
    @@ALL VARCHAR(MAX) = @P_ALL,
    @@SECTION VARCHAR(MAX) = @P_SECTION,
    @@CALL_SP VARCHAR(MAX);

-- true
IF(@@ALL = 'TRUE')
BEGIN
    SET @@QUERY = '
        SELECT DISTINCT A.R_SUBMISSION_ID AS SUBMISSION_ID
			, A.M_PERIODE_ID
			, A.NOREG
			, A.SUBMISSION_DATE
			, O.PERSONNEL_NAME AS NAME
			, O.DIVISION_NAME AS DIVISION
			, O.DEPARTMENT_NAME AS DEPARTMENT
			, O.SECTION_NAME AS SECTION
			, O.POSITION_LEVEL
        FROM ASP.TB_R_SUBMISSION_ANSWER A
        JOIN vw_Organization O ON A.NOREG = O.NOREG
        JOIN PDI.TB_M_ORG_USER_POSITION OSP ON O.NOREG = OSP.NOREG 
            AND (GETDATE() BETWEEN OSP.VALID_FROM AND OSP.VALID_TO) 
            AND OSP.POSITION_PERCENT = 100
            AND O.ORG_ID = OSP.ORG_ID
    ';
END ELSE
BEGIN
    SET @@CALL_SP = 'EXEC PDI.sp_GetUserPosition_NEW ''' + @@NOREG + ''', @@SUPER = 0'
    EXECUTE (@@CALL_SP)

    SET @@QUERY = ' 
        WITH CTE AS (
            SELECT *,
                ROW_NUMBER() OVER (PARTITION BY NOREG ORDER BY (SELECT NULL)) AS RowNum
            FROM ##TEMP_ORGPOSITION
        )

        SELECT 
            DISTINCT
            A.R_SUBMISSION_ID AS SUBMISSION_ID
            , A.M_PERIODE_ID
            , A.NOREG
            , A.SUBMISSION_DATE
            , O.PERSONNEL_NAME AS NAME
            , O.DIVISION_NAME AS DIVISION
            , O.DEPARTMENT_NAME AS DEPARTMENT
            , O.SECTION_NAME AS SECTION
            , O.POSITION_LEVEL
        FROM CTE CT
        JOIN ASP.TB_R_SUBMISSION_ANSWER A ON A.NOREG = CT.NOREG
        JOIN DBO.vw_Organization O ON O.NOREG = CT.NOREG
        JOIN PDI.TB_M_ORG_USER_POSITION OSP ON O.NOREG = OSP.NOREG 
            AND (GETDATE() BETWEEN OSP.VALID_FROM AND OSP.VALID_TO) 
            AND OSP.POSITION_PERCENT = 100
            AND O.ORG_ID = OSP.ORG_ID
        WHERE RowNum = 1
    ';
END;

IF (@@DIVISION != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND O.DIVISION_NAME = ''' + @@DIVISION + '''';
END

IF (@@DEPARTMENT != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND O.DEPARTMENT_NAME = ''' + @@DEPARTMENT + '''';
END

IF (@@SECTION != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND O.SECTION_NAME = ''' + @@SECTION + '''';
END

IF (@@SEARCH != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND (
            A.NOREG LIKE ''%' + @@SEARCH + '%''
        OR
            O.PERSONNEL_NAME LIKE ''%' + @@SEARCH + '%''
    )';
END

SET @@QUERY = @@QUERY + ' ORDER BY A.SUBMISSION_DATE, O.POSITION_LEVEL '+@@SORT; 

IF (@@OFFSET IS NOT NULL AND @@FETCH IS NOT NULL) BEGIN 
    SET @@QUERY = @@QUERY +' OFFSET ' + @@OFFSET + ' ROWS FETCH NEXT ' + @@FETCH + ' ROWS ONLY ';
END;

EXECUTE (@@QUERY);