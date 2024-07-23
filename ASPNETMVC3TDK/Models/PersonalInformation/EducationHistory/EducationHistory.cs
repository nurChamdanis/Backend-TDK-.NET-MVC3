namespace ASPNETMVC3TDK.Models.EducationHistory
{
    public class EducationHistory
    {
        public string TRANSACTION_ID { get; set; }
        public string NOREG { get; set; }
        public string EMPLOYEE_NAME { get; set; } = null;
        public string EDUCATION_CD { get; set; }
        public string EDUCATION_DESC { get; set; }
        public string EDUCATION_LEVEL { get; set; }
        public string SCHOOL_NAME { get; set; }
        public string MAJOR_CD { get; set; }
        public string VALID_FROM { get; set; }
        public decimal GPA { get; set; }
        public string COUNTRY_ID { get; set; }
        public string VALID_TO { get; set; }
        public string CERTIFICATE_NAME  { get; set; }
        public string CERTIFICATE_PATH { get; set; }
        public string REMARK_1 { get; set; }
        public string REMARK_2 { get; set; }
        public string MAJOR_NAME { get; set; }
        public string CREATED_DT { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DT { get; set; } 
        public string STATUS_CD { get; set; }
        public string REJECT_REASON { get; set; }
        public string APP_SH_NOREG { get; set; }
        public string APP_SH_STATUS { get; set; }
        public string APP_SH_DATE { get; set; }
        public string APP_DPH_NOREG { get; set; }
        public string APP_DPH_STATUS { get; set; }
        public string APP_DPH_DATE { get; set; }
        public string APP_DH_NOREG { get; set; }
        public string APP_DH_STATUS { get; set; }
        public string APP_DH_DATE { get; set; }
        public string APP_DIR_NOREG { get; set; }
        public string APP_DIR_STATUS { get; set; }
        public string APP_HR_ADMIN_NOREG { get; set; }
        public string APP_HR_ADMIN_STATUS { get; set; }
        public string APP_HR_ADMIN_DATE { get; set; }
        public string CANCEL_FLAG { get; set; }
        public string CANCEL_BY { get; set; }
        public string CANCEL_DATE { get; set; }
        public string APP_SH_NAME { get; set; }
        public string APP_DPH_NAME { get; set; }
        public string APP_DH_NAME { get; set; }
        public string APP_DIR_NAME { get; set; }
        public string APP_HR_NAME { get; set; }
        public string APP_SH_LABEL { get; set; }
        public string APP_DPH_LABEL { get; set; }
        public string APP_DH_LABEL { get; set; }
        public string APP_DIR_LABEL { get; set; }
        public string APP_HR_LABEL { get; set; }
    }

}