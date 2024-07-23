DECLARE
    @@INFO VARCHAR(MAX)
    , @@RESULT VARCHAR(MAX)
    , @@MESSAGES VARCHAR(MAX)
    , @@SUBMISSION_FOUND BIT = 0
    , @@NOREG VARCHAR(50) = @P_NOREG
    , @@M_PERIODE_ID INT = @P_M_PERIODE_ID
    , @@M_CATEGORY_ID INT = @P_M_CATEGORY_ID
    , @@R_SUBMISSION_ID INT
    , @@UPDATE_SUBMISSION_DATE BIT = @P_UPDATE_SUBMISSION_DATE
    , @@DATA_ANSWER XML = @P_DATA_ANSWER
    , @@DATA_ANSWER_DETAIL XML = @P_DATA_ANSWER_DETAIL
    , @@QUERY_SEARCH_SUBMISSION NVARCHAR(MAX)
    , @@QUERY_IDS_ANSWER NVARCHAR(MAX);

DECLARE @@TEMP_ANSWER_ID TABLE (R_ANSWER_ID VARCHAR(MAX));

BEGIN TRY
    BEGIN TRANSACTION;

    -- Melakukan pengecekan apakah data submission user dengan periode tertentu ada
    SET @@QUERY_SEARCH_SUBMISSION = N'
        SELECT 
            @@SUBMISSION_FOUND = 1, 
            @@R_SUBMISSION_ID = R_SUBMISSION_ID
        FROM 
            ASP.TB_R_SUBMISSION_ANSWER
        WHERE 
                NOREG = @@NOREG
            AND 
                M_PERIODE_ID = @@PERIODE_ID
    ';

    EXEC sp_executesql @@QUERY_SEARCH_SUBMISSION, N'@@SUBMISSION_FOUND BIT OUTPUT, @@R_SUBMISSION_ID INT OUTPUT, @@NOREG VARCHAR(20), @@PERIODE_ID INT', @@SUBMISSION_FOUND OUTPUT, @@R_SUBMISSION_ID OUTPUT, @@NOREG = @@NOREG, @@PERIODE_ID = @@M_PERIODE_ID;

    IF @@SUBMISSION_FOUND != 1
    BEGIN
        DECLARE @@TEMP_NEW_SUBMISSION TABLE (R_SUBMISSION_ID INT);

        INSERT INTO ASP.TB_R_SUBMISSION_ANSWER
	        ( 
                SUBMISSION_DATE
                , CREATED_DT
                , CREATED_BY
                , M_PERIODE_ID
                , NOREG
            )
        OUTPUT INSERTED.R_SUBMISSION_ID INTO @@TEMP_NEW_SUBMISSION
	    VALUES
	        ( 
                NULL
                , CURRENT_TIMESTAMP
                , @@NOREG
                , @@M_PERIODE_ID
                , @@NOREG
            );

            
        SELECT 
            @@R_SUBMISSION_ID = CAST(R_SUBMISSION_ID AS INT)
        FROM 
            @@TEMP_NEW_SUBMISSION;
    END

    IF @@UPDATE_SUBMISSION_DATE = 1
    BEGIN
        -- Data ditemukan
        UPDATE ASP.TB_R_SUBMISSION_ANSWER
		SET  
			SUBMISSION_DATE = CURRENT_TIMESTAMP
		WHERE 
			NOREG = @@NOREG AND M_PERIODE_ID = @@M_PERIODE_ID; 
    END

    SET @@QUERY_IDS_ANSWER = N'
        SELECT 
            R_ANSWER_ID 
        FROM
            ASP.TB_R_ANSWER
        WHERE
            NOREG = @@NOREG
        AND 
            M_PERIODE_ID = @@M_PERIODE_ID
        AND 
            M_CATEGORY_ID = @@M_CATEGORY_ID
    ';

    INSERT INTO @@TEMP_ANSWER_ID (R_ANSWER_ID)
    EXEC sp_executesql @@QUERY_IDS_ANSWER, 
                       N'@@NOREG VARCHAR(50), @@M_PERIODE_ID INT, @@M_CATEGORY_ID INT', 
                       @@NOREG, @@M_PERIODE_ID, @@M_CATEGORY_ID;
    
    -- Menghapus data yang tersedia pada TB_R_ANSWER
    DELETE FROM ASP.TB_R_ANSWER
    WHERE R_ANSWER_ID IN (SELECT R_ANSWER_ID FROM @@TEMP_ANSWER_ID);
    
    -- Menghapus data yang tersedia pada TB_R_ANSWER_DETAIL
    DELETE FROM ASP.TB_R_ANSWER_DETAIL
    WHERE R_ANSWER_ID IN (SELECT R_ANSWER_ID FROM @@TEMP_ANSWER_ID);
    
    -- Menambahkan data baru pada TB_R_ANSWER
    INSERT INTO ASP.TB_R_ANSWER (
        R_ANSWER_ID,
        M_PERIODE_ID,
        M_CATEGORY_ID,
        NOREG,
        M_QUESTION_ID,
        ANSWER_SUBMITTED_DT,
        CREATED_DT,
        CREATED_BY,
        R_SUBMISSION_ID
    )
    SELECT
        Tbl.Col.value('(R_ANSWER_ID/text())[1]', 'VARCHAR(50)') AS R_ANSWER_ID,
        Tbl.Col.value('(M_PERIODE_ID/text())[1]', 'INT') AS M_PERIODE_ID,
        Tbl.Col.value('(M_CATEGORY_ID/text())[1]', 'INT') AS M_CATEGORY_ID,
        Tbl.Col.value('(NOREG/text())[1]', 'VARCHAR(50)') AS NOREG,
        Tbl.Col.value('(M_QUESTION_ID/text())[1]', 'INT') AS M_QUESTION_ID,
        Tbl.Col.value('(ANSWER_SUBMITTED_DT/text())[1]', 'VARCHAR(50)') AS ANSWER_SUBMITTED_DT,
        Tbl.Col.value('(CREATED_DT/text())[1]', 'VARCHAR(50)') AS CREATED_DT,
        Tbl.Col.value('(CREATED_BY/text())[1]', 'VARCHAR(50)') AS CREATED_BY,
        @@R_SUBMISSION_ID AS R_SUBMISSION_ID
    FROM
        @@DATA_ANSWER.nodes('/ArrayOfFieldTableAnswer/FieldTableAnswer') AS Tbl(Col);
        
   -- Menambahkan data baru pada TB_R_ANSWER_DETAIL
   INSERT INTO ASP.TB_R_ANSWER_DETAIL (
	    R_ANSWER_DETAIL_ID,
	    R_ANSWER_ID,
	    ANSWER,
	    CREATED_DT,
	    CREATED_BY,
	    ANSWER_TEXT
	)
	SELECT
	    Tbl.Col.value('(R_ANSWER_DETAIL_ID/text())[1]', 'VARCHAR(50)') AS R_ANSWER_DETAIL_ID,
	    Tbl.Col.value('(R_ANSWER_ID/text())[1]', 'VARCHAR(50)') AS R_ANSWER_ID,
	    Tbl.Col.value('(ANSWER/text())[1]', 'VARCHAR(50)') AS ANSWER,
	    Tbl.Col.value('(CREATED_DT/text())[1]', 'VARCHAR(50)') AS CREATED_DT,
	    Tbl.Col.value('(CREATED_BY/text())[1]', 'VARCHAR(50)') AS CREATED_BY,
	    Tbl.Col.value('(ANSWER_TEXT/text())[1]', 'NVARCHAR(MAX)') AS ANSWER_TEXT
	FROM
	    @@DATA_ANSWER_DETAIL.nodes('/ArrayOfFieldTableAnswerDetail/FieldTableAnswerDetail') AS Tbl(Col);

    
    SET @@INFO = 'success';
	SET @@RESULT = 'true';
	SET @@MESSAGES = 'Data insert successfully';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    
    SET @@INFO = 'failed';
	SET @@RESULT = 'false';
	SET @@MESSAGES = ERROR_MESSAGE();
END CATCH;

SELECT @@INFO as msgInfo, @@RESULT as result, @@MESSAGES as msg;