using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.EducationHistory;
using ASPNETMVC3TDK.Models.LanguageSkill;
using ASPNETMVC3TDK.Models.ICTAssigmentExternal;
using ASPNETMVC3TDK.Models.ProjectAssignment;
using ASPNETMVC3TDK.Models.RotationHistory;
using ASPNETMVC3TDK.Models.RotationHistory;
using ASPNETMVC3TDK.Models.TrainingHistory;
using ASPNETMVC3TDK.Models.Eligibility;
using ASPNETMVC3TDK.Models.Achivement;
using System.Collections.Generic;

namespace ASPNETMVC3TDK.Models.Export
{
    public class M_ExportAll
    {
        public string NOREG { get; set; } = null;
        public List<M_EducationHistoryReq> EDUCATION { get; set; } 
        public List<M_LanguageSkill> LANGUAGE_SKILL { get; set; }
/*        public List<M_ICTAssigmentExternal> ICT { get; set; }
        public List<M_ProjectAssignment> PROJECT { get; set; }
        public List<M_RotationHistory> ROTATION { get; set; }
        public List<M_TrainingHistory> HISTORY { get; set; }
        public List<M_Eligibility> ELIGIBILITY { get; set; }
        public List<M_Achivement> ACHIEVEMENT { get; set; }*/
    }
}