﻿DECLARE @@FILE_ID VARCHAR(MAX)=@FILE_ID;
SELECT * FROM [PDI].[TB_M_PDF] where FILE_ID=@@FILE_ID;