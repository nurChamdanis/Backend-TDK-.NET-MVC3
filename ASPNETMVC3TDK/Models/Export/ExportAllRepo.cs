using System;
using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using ASPNETMVC3TDK.Models.LanguageSkill;
using ASPNETMVC3TDK.Models.EducationHistory;
using ASPNETMVC3TDK.Models.RotationHistory;
using ASPNETMVC3TDK.Models.ProjectAssignment;
using ASPNETMVC3TDK.Models.ICTAssigmentExternal;
using ASPNETMVC3TDK.Models.TrainingHistory;
using ASPNETMVC3TDK.Models.Eligibility;
using ASPNETMVC3TDK.Models.Achivement;

namespace ASPNETMVC3TDK.Models.Export
{
    public class ExportAllRepo
    {
        private static ExportAllRepo instance = null;
        private IDBContext db = DatabaseManager.Instance.GetContext();

        public static ExportAllRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportAllRepo();
                }
                return instance;
            }
        }

        public class ExportResults
        {
            public string NOREG { get; set; }
            public List<M_EducationHistoryReq> EDUCATION { get; set; }
            public List<M_LanguageSkillRepo> LANGUAGE_SKILL { get; set; }
            public List<M_RotationHistory> ROTATION { get; set; }
            public List<M_ProjectAssignment> PROJECT_ASSIGN { get; set; }
            public List<M_ICTAssigmentExternal> CAREER_ICT { get; set; }
            public List<M_TrainingHistory> TRAINING_HISTORY { get; set; }
            public List<M_Eligibility> ELIGILIBILITY_HISTORY { get; set; }
            public List<M_Achivement> ACHIVEMENT_HISTORY { get; set; }
            // Add other properties as needed
        }

        public JsonResult GetExport(string P_NOREG, string P_ORDER_TYPE, string P_STATUS, string P_URL)
        {
            try
            {
                dynamic args = new { P_NOREG, P_ORDER_TYPE, P_STATUS, P_URL };

                var results = new ExportResults
                {
                    NOREG = P_NOREG,
                    EDUCATION = FetchEducationData(args) , 
                    LANGUAGE_SKILL = FetchLanguageSkillData(args) ,
                    ROTATION = FetchRotationHistoryData(args) ,
                    PROJECT_ASSIGN = FetchProjectAssignment(args) ,
                    CAREER_ICT = FetchCareerICT(args) ,
                    TRAINING_HISTORY = FetchTrainingHistory(args) ,
                    ELIGILIBILITY_HISTORY = FetchTrainingEligilibility(args) ,
                    ACHIVEMENT_HISTORY = FetchTrainingAchivement(args)
                };

                // Optionally, you may want to perform additional operations here

                return new JsonResult
                {
                    Data = new { status = 200, data = results, message = "Data fetched successfully." },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors as needed
                return new JsonResult
                {
                    ContentEncoding = System.Text.Encoding.UTF8,
                    Data = new { status = 500, message = "Failed to fetch data: " + ex.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        private List<M_EducationHistoryReq> FetchEducationData(dynamic args)
        {
            try
            {
                return db.Fetch<M_EducationHistoryReq>("Export/getEducationData", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch education data: " + ex.Message);
            }
        }

        private List<M_LanguageSkillRepo> FetchLanguageSkillData(dynamic args)
        {
            try
            {
                return db.Fetch<M_LanguageSkillRepo>("Export/getLanguageSkill", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }

        private List<M_RotationHistory> FetchRotationHistoryData(dynamic args)
        {
            try
            {
                return db.Fetch<M_RotationHistory>("Export/getRotationHistory", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }

        private List<M_ProjectAssignment> FetchProjectAssignment(dynamic args)
        {
            try
            {
                return db.Fetch<M_ProjectAssignment>("Export/getProjectAsistantManager", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }

        private List<M_ICTAssigmentExternal> FetchCareerICT(dynamic args)
        {
            try
            {
                return db.Fetch<M_ICTAssigmentExternal>("Export/getICTAssigmentExternal", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }

        private List<M_TrainingHistory> FetchTrainingHistory(dynamic args)
        {
            try
            {
                return db.Fetch<M_TrainingHistory>("Export/getTrainingHistory", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }

        private List<M_Eligibility> FetchTrainingEligilibility(dynamic args)
        {
            try
            {
                return db.Fetch<M_Eligibility>("Export/getTrainingEligibility", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }

        private List<M_Achivement> FetchTrainingAchivement(dynamic args)
        {
            try
            {
                return db.Fetch<M_Achivement>("Export/getAchivement", args);
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw new Exception("Failed to fetch language skill data: " + ex.Message);
            }
        }




    }
}
