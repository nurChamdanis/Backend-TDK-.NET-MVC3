DECLARE 
	  @@QUERY VARCHAR(MAX)
	, @@ID VARCHAR(MAX) = REPLACE(@P_ID, '''', '`')
	, @@NAME VARCHAR(MAX) = REPLACE(@P_NAME, '''', '`')
	, @@APPALIAS VARCHAR(MAX) = @P_APPALIAS;

	SET @@QUERY = '
		SELECT 
			COUNT(1) AS CNT
		FROM 
			TB_M_FUNCTION
		WHERE APPLICATION like ''%'+@@APPALIAS+'%''';


IF (@@ID != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND ID = ''' + @@ID + '''';
END

IF (@@NAME != '') BEGIN
	SET @@QUERY = @@QUERY + ' AND NAME = ''' + @@NAME + '''';
END

EXECUTE (@@QUERY);