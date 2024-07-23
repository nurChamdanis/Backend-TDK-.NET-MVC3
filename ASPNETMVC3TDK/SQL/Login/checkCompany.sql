DECLARE @@sqlstate varchar(max), @@USERNAME varchar(max)

SET @@USERNAME = @pUser;

set @@sqlstate = 'SELECT COUNT(1) AS CNT 
		FROM TB_M_USER_MAPPING WHERE USER_NM = '''+@@USERNAME+''' ';


execute(@@sqlstate);