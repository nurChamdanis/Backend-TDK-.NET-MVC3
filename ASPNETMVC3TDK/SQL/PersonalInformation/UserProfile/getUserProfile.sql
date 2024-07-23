DECLARE  
    @@QUERY VARCHAR(MAX),
    @@SUPERIOR VARCHAR(MAX),
    @@NOREG VARCHAR(MAX) = @P_NOREG,
    @@ORDER_TYPE VARCHAR(MAX) = REPLACE(@P_ORDER_TYPE, '''', '`'), 
    @@NOREG_TEMP VARCHAR(MAX),
    @@CALL_SP VARCHAR(MAX);

SET @@CALL_SP = 'EXEC [PDI].[sp_GetEmployeeProfile_superior] '''+@@NOREG+''' ';  
EXECUTE (@@CALL_SP);  -- Execute the stored procedure

SET @@SUPERIOR = ''; 

SET @@QUERY = '
 	SELECT 
 		DISTINCT A.NOREG, 
 		A.PERSONNEL_NAME, 
 		A.DATE_OF_BIRTH, 
 		A.Age, 
 		A.CLASS, 
 		A.POSITION, 
 		A.MAIL, 
 		A.GENDER_DESC,
 		A.ENTRY_DATE, 
 		A.TELEPHONE ,
 		A.Division ,
 		A.JOIN_BY,
 		B.superior 
 	FROM 
(
 	select
 		DISTINCT A.NOREG, 
 		A.PERSONNEL_NAME, 
 		B.DATE_OF_BIRTH, 
 		DATEDIFF (yy, B.DATE_OF_BIRTH, GETDATE()) AS Age, 
 		A.CLASS, 
 		C.POSITION, 
 		A.MAIL, 
 		D.GENDER_DESC,
 		C.ENTRY_DATE, 
 		E.TELEPHONE, 
 		CASE WHEN ISNULL(C.DEPARTMENT,'''' ) = '''' THEN C.DIVISION
 			WHEN ISNULL(C.SECTION,'''' ) = '''' THEN C.DIVISION 
 				+ '' / '' + C.DEPARTMENT
 			WHEN ISNULL(C.LINE,'''' ) = '''' THEN C.DIVISION + '' / ''
 				+ C.DEPARTMENT + '' / '' + C.SECTION
 			WHEN ISNULL(C.[GROUP],'''' ) = '''' THEN C.DIVISION + '' / '' + C.DEPARTMENT + '' / '' + C.SECTION 
 				+ '' / '' + C.LINE
 		ELSE C.DIVISION + '' / '' + C.DEPARTMENT + '' / '' + C.SECTION + '' / '' + C.LINE + '' / '' + C.[GROUP] 
 		END AS Division ,
 		B.VALID_FROM AS JOIN_BY
 	FROM DBO.vw_organization A
 	JOIN DBO.TB_M_PROFILE_MAIN B ON A.NOREG = B.NOREG AND GETDATE() BETWEEN B.VALID_FROM AND B.VALID_TO
 	JOIN DBO.TB_M_PROFILE_HEADER C ON A.NOREG = C.NOREG
 	JOIN DBO.TB_M_GENDER D ON B.GENDER = D.GENDER_ID
 	LEFT JOIN DBO.TB_M_PROFILE_ADDRESS E ON A.NOREG = E.NOREG AND E.ADDRESS_TYPE = 1 AND 
 		GETDATE() BETWEEN E.VALID_FROM AND E.VALID_TO
 	WHERE 
 		1 = 1 AND B.NOREG = '+@@NOREG+'  
 	) A
 	JOIN ( 
 		SELECT 
 		A.NOREG,
 		B.PERSONNEL_NAME as superior
 		FROM PDI.TEMP_SUPERIOR A
 		JOIN vw_Organization b on b.NOREG=a.SUPERIOR 
 	) B ON B.NOREG=A.NoReg
';



EXECUTE (@@QUERY);     -- Execute the query on ##PERSONAL_DATA table
-- get user profile

