using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC3TDK.Models.LanguageSkill

{
    public class M_LanguageSkillRepo
    {
        public string NO { get; set; }
        public string NOREG { get; set; }
        public string LANGUAGE_ID { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public string LANGUAGE_NAME { get; set; }
        public string SKILL_DESC { get; set; }
        public string SKILL_LEVEL { get; set; }
        public string SCORE { get; set; }
        public string LANGUAGE_TEST { get; set; }
        public string CERTIFICATE_NAME { get; set; }
        public string CERTIFICATE_PATH { get; set; }
        public string VALID_TO { get; set; }
        public string REMARK_1 { get; set; }
        public string CREATED_DT { get; set; }
        public string CREATED_BY { get; set; }
    }
}