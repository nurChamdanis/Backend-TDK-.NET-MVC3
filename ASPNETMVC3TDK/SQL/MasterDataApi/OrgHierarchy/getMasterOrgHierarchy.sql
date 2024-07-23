DECLARE 
    @@QUERY VARCHAR(MAX),
    @@ORG_ID VARCHAR(MAX) = REPLACE(@P_ORG_ID, '''', '`'),
    @@ORG_PARENT VARCHAR(MAX) =  REPLACE(@P_ORG_PARENT, '''', '`'),
    @@ORG_TITLE VARCHAR(MAX) = REPLACE(@P_ORG_TITLE, '''', '`'),
    @@LEVEL_ID VARCHAR(MAX) =  REPLACE(@P_LEVEL_ID, '''', '`')
    ;

SET @@QUERY = '
    SELECT 
        TOP 50 * 
    FROM 
        PDI.TB_M_ORG_HIERARCHY 
    WHERE 
        GETDATE() BETWEEN VALID_FROM AND VALID_TO
';

IF(@@LEVEL_ID != '')  BEGIN
    SET @@QUERY = @@QUERY + ' AND LEVEL_ID  = ''' + @@LEVEL_ID + '''';
END;

IF(@@ORG_ID != '')  BEGIN
    SET @@QUERY = @@QUERY + ' AND ORG_ID  = ''' + @@ORG_ID + '''';
END;

IF(@@ORG_PARENT != '')  BEGIN
    SET @@QUERY = @@QUERY + ' AND ORG_PARENT  = ''' + @@ORG_PARENT + '''';
END;

IF(@@ORG_TITLE != '')  BEGIN
    SET @@QUERY = @@QUERY + ' AND ORG_TITLE LIKE ''%' + @@ORG_TITLE + '%''';
END;

EXECUTE (@@QUERY);