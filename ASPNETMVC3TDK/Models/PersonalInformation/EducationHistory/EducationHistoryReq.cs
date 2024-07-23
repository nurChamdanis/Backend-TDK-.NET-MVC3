using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace ASPNETMVC3TDK.Models.EducationHistory
{
    public class M_EducationHistoryReq
    {
        public string TRANSACTION_ID { get; set; } = null;
        public string NOREG { get; set; } = null;
        public string EDUCATION_CD { get; set; } = null;
        public string EDUCATION_LEVEL { get; set; } = null;
        public string EMPLOYEE_NAME { get; set; } = null;
        public string MAJOR_CD { get; set; } = null;
        public string MAJOR_NAME { get; set; } = null;
        public string GPA { get; set; } = null;
        public string SCHOOL_NAME { get; set; } = null;
        public string COUNTRY_ID { get; set; } = null;
        public string VALID_FROM { get; set; } = null;
        public string VALID_TO { get; set; } = null;
        public string CERTIFICATE_NAME { get; set; } = null;
        public string CERTIFICATE_PATH { get; set; } = null;
        public IEnumerable<HttpPostedFileBase> CERTIFICATE { get; set; }
        public string REMARK_1 { get; set; } = null;
        public string PK_EDUCATION_CD { get; set; } = null;
        public string PK_MAJOR_CD { get; set; } = null;
    }

}