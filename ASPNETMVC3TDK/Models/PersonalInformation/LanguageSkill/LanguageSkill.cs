using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC3TDK.Models.LanguageSkill

{
    public class M_LanguageSkill
    {
        public string TRANSACTION_ID { get; set; } = null;
        public string LANGUAGE_ID { get; set; } = null;
        public string NOREG { get; set; } = null;
        public string LANGUAGE_NAME { get; set; } = null;
        public string SCORE { get; set; } = null;
        public string SKILL_LEVEL { get; set; } = null;
        public string SKILL_DESC { get; set; } = null;
        public IEnumerable<HttpPostedFileBase> CERTIFICATE { get; set; }
        public string CERTIFICATE_NAME { get; set; } = null;
        public string CERTIFICATE_PATH { get; set; } = null;
        public string LANGUAGE_TEST { get; set; } = null;
        public string VALID_TO { get; set; } = null;
        public string REMARK_1 { get; set; } = null;
        public string REMARK_2 { get; set; } = null; 
        public string PK_LANGUAGE_TEST { get; set; } = null; 
        public string STATUS_CD { get; set; } = null;
        public string REJECT_REASON { get; set; } = null;
        public string APP_SH_NOREG { get; set; } = null;
        public string APP_SH_STATUS { get; set; } = null;
        public string APP_SH_DATE { get; set; } = null;
        public string APP_DPH_NOREG { get; set; } = null;
        public string APP_DPH_STATUS { get; set; } = null;
        public string APP_DPH_DATE { get; set; } = null;
        public string APP_DH_NOREG { get; set; } = null;
        public string APP_DH_STATUS { get; set; } = null;
        public string APP_DH_DATE { get; set; } = null;
        public string APP_DIR_NOREG { get; set; } = null;
        public string APP_DIR_STATUS { get; set; } = null;
        public string APP_HR_ADMIN_NOREG { get; set; } = null;
        public string APP_HR_ADMIN_STATUS { get; set; } = null;
        public string APP_HR_ADMIN_DATE { get; set; } = null;
        public string CANCEL_FLAG { get; set; } = null;
        public string CANCEL_BY { get; set; } = null;
        public string CANCEL_DATE { get; set; } = null;
        public string APP_SH_NAME { get; set; } = null;
        public string APP_DPH_NAME { get; set; } = null;
        public string APP_DH_NAME { get; set; } = null;
        public string APP_DIR_NAME { get; set; } = null;
        public string APP_HR_NAME { get; set; } = null;
        public string APP_SH_LABEL { get; set; } = null;
        public string APP_DPH_LABEL { get; set; } = null;
        public string APP_DH_LABEL { get; set; } = null;
        public string APP_DIR_LABEL { get; set; } = null;
        public string APP_HR_LABEL { get; set; } = null;
    }

}