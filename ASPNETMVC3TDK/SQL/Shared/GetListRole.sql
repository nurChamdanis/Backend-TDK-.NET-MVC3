DECLARE 
  @@QUERY VARCHAR(MAX),
  @@APPALIAS VARCHAR(50) = @P_APPALIAS,
  @@PARAM_1 VARCHAR(50) = @PARAM_1,
  @@PARAM_2 VARCHAR(50) = @PARAM_2,
  @@PARAM_3 VARCHAR(50) = @PARAM_3

SET @@QUERY = '
    SELECT 
        ID as id,
        NAME as text
    FROM 
        TB_M_ROLE
    WHERE APPLICATION = '''+@@APPALIAS+'''
    AND 1=1  ';

IF(@@PARAM_1 = 'ROLE') 
BEGIN
    SET @@QUERY = '
        SELECT 
            ID as id, 
            NAME as text 
        FROM 
            TB_M_ROLE 
            WHERE 1=1  ';

    IF(@@PARAM_2 <> '') 
    BEGIN
        SET @@QUERY = @@QUERY + ' AND REPLACE(APPLICATION , '''''''', ''`'') = '''  + @@PARAM_2  + '''';
    END

    IF(@@PARAM_3 <> '') 
    BEGIN
        SET @@QUERY = @@QUERY + ' AND REPLACE(NAME, '''''''', ''`'') LIKE ''%'  + @@PARAM_3  + '%''';
    END
END;


 EXEC (@@QUERY);
	