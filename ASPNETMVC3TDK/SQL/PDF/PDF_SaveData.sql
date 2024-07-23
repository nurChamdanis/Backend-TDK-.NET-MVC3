DECLARE @@info VARCHAR(MAX);
DECLARE @@result VARCHAR(MAX);
DECLARE @@messages VARCHAR(MAX);

DECLARE 
    @@FILE_ID VARCHAR(MAX)=@FILE_ID,
    @@FILE_LOCATION VARCHAR(MAX)=@FILE_LOCATION,
    @@CREATED_BY VARCHAR(MAX)=@CREATED_BY,
    @@MODE VARCHAR(MAX)=@MODE;

BEGIN TRY
    if(@@MODE='add')
      BEGIN
        INSERT INTO [PDI].[TB_M_PDF]
            (
                FILE_ID,
                FILE_LOCATION,
                CREATED_BY,
                CREATED_DT
            )
        VALUES
            (
                @@FILE_ID,
                @@FILE_LOCATION,
                @@CREATED_BY,
                GETDATE()
            );
        SET @@info = 'success';
        SET @@result = 'true';
        SET @@messages = 'Data saved successfully';
      END
    ELSE IF(@@MODE='edit')
        BEGIN
            UPDATE [PDI].[TB_M_PDF]
            SET 
                FILE_LOCATION=@@FILE_LOCATION,
                CHANGED_BY = @@CREATED_BY,
                CHANGED_DT = GETDATE()
            WHERE
                FILE_ID = @@FILE_ID;
            SET @@info = 'success';
            SET @@result = 'true';
            SET @@messages = 'Data update successfully';
        END  
    
END TRY
BEGIN CATCH
    SET @@info = 'failed';
    SET @@result = 'false';
    SET @@messages = ERROR_MESSAGE();

END CATCH;

-- Menampilkan hasil akhir
SELECT @@info AS msgInfo, @@result AS result, @@messages AS msg;
