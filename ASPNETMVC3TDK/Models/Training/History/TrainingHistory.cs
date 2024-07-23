using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC3TDK.Models.TrainingHistory
{
    public class M_TrainingHistory
    {
        public string TRANSACTION_ID { get; set; } = null;
        public string NOREG { get; set; } = null;
        public string SEQ { get; set; } = null;
        public string TRAINING_TYPE { get; set; } = null;
        public string INSTITUTION { get; set; } = null;
        public string COUNTRY_ID { get; set; } = null;
        public string YEAR { get; set; } = null;
        public string TRAINING_TOPIC { get; set; } = null;
        public string SKILL { get; set; } = null;
        public string CERTIFICATE_NAME { get; set; } = null;
        public string CERTIFICATE_PATH { get; set; } = null;
        public IEnumerable<HttpPostedFileBase> CERTIFICATE { get; set; } = null;
        public string REMARK_1 { get; set; } = null;
        public string REMARK_2 { get; set; } = null;
        public string STATUS_CD { get; set; } = null;
        public string REJECT_REASON { get; set; } = null;
        public string APP_SH_NOREG { get; set; } = null;
        public string APP_SH_STATUS { get; set; } = null;
        public string APP_SH_DATE { get; set; } = null;
        public string APP_SH_NAME { get; set; } = null;
        public string APP_SH_LABEL { get; set; } = null;
        public string APP_DPH_NOREG { get; set; } = null;
        public string APP_DPH_STATUS { get; set; } = null;
        public string APP_DPH_DATE { get; set; } = null;
        public string APP_DPH_NAME { get; set; } = null;
        public string APP_DPH_LABEL { get; set; } = null;
        public string APP_DH_NOREG { get; set; } = null;
        public string APP_DH_STATUS { get; set; } = null;
        public string APP_DH_NAME { get; set; } = null;
        public string APP_DH_DATE { get; set; } = null;
        public string APP_DH_LABEL { get; set; } = null;
        public string APP_DIR_NOREG { get; set; } = null;
        public string APP_DIR_STATUS { get; set; } = null;
        public string APP_DIR_NAME { get; set; } = null;
        public string APP_DIR_DATE { get; set; } = null;
        public string APP_DIR_LABEL { get; set; } = null;
        public string APP_HR_ADMIN_NOREG { get; set; } = null;
        public string APP_HR_ADMIN_STATUS { get; set; } = null;
        public string APP_HR_ADMIN_DATE { get; set; } = null;
        public string APP_HR_ADMIN_NAME { get; set; } = null;
        public string APP_HR_ADMIN_LABEL { get; set; } = null;
        public string CANCEL_FLAG { get; set; } = null;
        public string CANCEL_BY { get; set; } = null;
        public string CANCEL_DATE { get; set; } = null;
    }
}
