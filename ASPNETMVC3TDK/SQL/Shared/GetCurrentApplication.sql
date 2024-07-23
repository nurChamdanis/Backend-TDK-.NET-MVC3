 DECLARE 
@@QUERY VARCHAR(MAX),
@@APPALIAS VARCHAR(50) = @P_APPALIAS,
@@PARAM_1 VARCHAR(50) = @PARAM_1


    SET @@QUERY = '
        SELECT DISTINCT
            ID as id,
			NAME as text
        FROM 
           TB_M_APPLICATION
        WHERE 1=1 ';

    IF (@@PARAM_1 != '') BEGIN
	    SET @@QUERY = @@QUERY + ' AND NAME LIKE ''%' + @@PARAM_1 + '%''';
    END

    EXEC (@@QUERY);

