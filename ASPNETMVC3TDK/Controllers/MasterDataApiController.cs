using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.Country;
using ASPNETMVC3TDK.Models.EducationMajor;
using ASPNETMVC3TDK.Models.Education;
using ASPNETMVC3TDK.Models.MasterLanguage;
using ASPNETMVC3TDK.Models.Skill;
using ASPNETMVC3TDK.Models.Position;
using ASPNETMVC3TDK.Models.OrgHierarchy;
using ASPNETMVC3TDK.Models.Class;

namespace ASPNETMVC3TDK.Controllers
{
	public class MasterDataApiController : PageController
	{
        private const string url = "";
        CountryRepo countryRepo = new CountryRepo();
		EducationMajorRepo educationMajorRepo = new EducationMajorRepo();
		EducationRepo educationRepo = new EducationRepo();
        MasterLanguageRepo masterLanguageRepo = new MasterLanguageRepo();
        SkillRepo masterSkillRepo = new SkillRepo();
        PositionRepo masterPositionRepo = new PositionRepo();
        OrgHierarchyRepo masterOrgHierarchy = new OrgHierarchyRepo();
        ClassRepo masterClass = new ClassRepo();
        DataTables datatable = new DataTables();

        //
        #region GetCountry
        [HttpPost]
        public JsonResult GetCountry(Country m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = countryRepo.getCountry(m.COUNTRY_CD);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        //
        #region GetEducationLevel
        [HttpPost]
        public JsonResult EducationMajor(EducationMajor m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = educationMajorRepo.getEducationMajor(m.EDUCATION_CD);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
         
        //
        #region GetEducationLevel
        [HttpPost]
        public JsonResult EducationLevel(Education m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = educationRepo.GetEducationLevel(m.EDUCATION_DESC);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region GetLanguage
        [HttpPost]
        public JsonResult Laguage(Language m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = masterLanguageRepo.GetLanguage(m.LANGUAGE_ID);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region GetLanguageSkill
        [HttpPost]
        public JsonResult LanguageSkill(LanguageSkill m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = masterLanguageRepo.GetLanguageSkill(m.SKILL_LEVEL);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region GetSkill
        [HttpPost]
        public JsonResult GetSkill(Skill m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = masterSkillRepo.getSkill(m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region GetMasterPosition
        [HttpPost]
        public JsonResult GetMasterPosition(PositionMaster m)
        {
            try
            {
                var result = masterPositionRepo.getMasterPosition(m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region GetMasterOrgHirarchy
        [HttpPost]
        public JsonResult GetMasterOrgHirarchy(OrgHierarchy m)
        {
            try
            {
                var result = masterOrgHierarchy.getMasterOrgHierarchy(m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region getOrgHirarchyTitle
        [HttpPost]
        public JsonResult getOrgHirarchyTitle(OrgHierarchy m)
        {
            try
            {
                var result = masterOrgHierarchy.getOrgHirarchyTitle(m); 
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //
        #region GetMasterClass
        [HttpPost]
        public JsonResult GetMasterClass(Class m)
        {
            try
            {
                var result = masterClass.getMasterClass(m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}