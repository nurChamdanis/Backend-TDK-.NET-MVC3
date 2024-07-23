using System;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.LanguageSkill;
using ASPNETMVC3TDK.Models.UserProfile;
using Newtonsoft.Json;
using Toyota.Common;


namespace ASPNETMVC3TDK.Controllers.Pages
{
    public class LanguageSkillApiController : PageController
    {

        LanguageSkillRepo languageSkillRepo = new LanguageSkillRepo(); 
        UserProfileRepo userProfileRepo = new UserProfileRepo();
        DataTables datatable = new DataTables();


        #region getLanguageHistory
        [HttpPost]
        public JsonResult getLanguageHistory(M_LanguageSkill m, bool MODEMASTER)
        {
            //User user = Lookup.Get<User>();
            try
            { 
                var result = languageSkillRepo.GetLanguageSkills(m.NOREG, "DESC", "4", MODEMASTER, m); 

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
                    status = 400,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response , JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //UpdateEducationData
        #region UpdateLanguageSkill
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateLanguageSkill(M_LanguageSkill m)
        {
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.LANGUAGE_TEST) || string.IsNullOrWhiteSpace(m.PK_LANGUAGE_TEST))
                    cek += "Laguage test, ";
                if (string.IsNullOrWhiteSpace(m.SCORE))
                    cek += "Score, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }

                var files = m.CERTIFICATE;
            }
            catch (Exception ex)
            {
                var fail = new Result();
                fail.ResultCode = false;
                fail.Message = ex.Message;
                Response.StatusCode = 400;
                return Json(fail, JsonRequestBehavior.AllowGet);
                // Handle exception s
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            var result = languageSkillRepo.UpdateData(m);
            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //UpdateEducationData
        #region DeleteLanguageSkill
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteLanguageSkill(M_LanguageSkill m)
        {
            User user = Lookup.Get<User>();
            return Json(languageSkillRepo.DeleteData(m.TRANSACTION_ID), JsonRequestBehavior.AllowGet);
        }
        #endregion

        //INSERT 
        #region InsertLanguageSkill
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult InsertLanguageSkill(M_LanguageSkill m)
        {
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.LANGUAGE_TEST) || string.IsNullOrWhiteSpace(m.PK_LANGUAGE_TEST))
                    cek += "Laguage test, ";
                if (string.IsNullOrWhiteSpace(m.SCORE))
                    cek += "Score, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }
                var files = m.CERTIFICATE;
            }
            catch (Exception ex)
            {
                var fail = new Result();
                fail.ResultCode = false;
                fail.Message = ex.Message;
                Response.StatusCode = 400;
                return Json(fail, JsonRequestBehavior.AllowGet);
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            var result = languageSkillRepo.SaveData(m);
            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }

}