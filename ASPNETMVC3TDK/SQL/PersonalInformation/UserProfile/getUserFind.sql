DECLARE 
    @@QUERY VARCHAR(MAX), 
    @@NOREG VARCHAR(MAX) = @P_NOREG;


 
SET @@QUERY = ' 
	SELECT * FROM vw_Organization A
	WHERE 
    1=1  
'; 

EXECUTE (@@QUERY);

