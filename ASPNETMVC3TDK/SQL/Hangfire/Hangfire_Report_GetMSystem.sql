DECLARE @@sql  as varchar(max) = 'SELECT SYSTEM_CD, SYSTEM_VALUE FROM TB_M_SYSTEM WHERE 1 = 1';

IF(@_SYSTEM_TYPE <> '')
BEGIN
	SET @@sql = @@sql + ' AND SYSTEM_TYPE = '''+@_SYSTEM_TYPE+'''';
END

IF(@_SYSTEM_CD <> '')
BEGIN
	SET @@sql = @@sql + ' AND SYSTEM_CD = '''+@_SYSTEM_CD+'''';
END

exec(@@sql);