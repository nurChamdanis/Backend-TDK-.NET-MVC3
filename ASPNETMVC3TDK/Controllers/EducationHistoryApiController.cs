using System;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.EducationHistory;
using System.IO;
using System.Threading.Tasks;
using Toyota.Common;

namespace ASPNETMVC3TDK.Controllers.Pages
{
    public class EducationHistoryApiController : PageController
    {

        EducationHistoryRepo repo = new EducationHistoryRepo();
        DataTables datatable = new DataTables();

        #region getEducationHistory
        [HttpPost]
        public JsonResult getEducationHistory(string NOREG, bool MODEMASTER)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = repo.getEducationHistory(NOREG, MODEMASTER);

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
                    status = 500,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //InsertEducationData
        #region InsertEducationHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult InsertEducationHistory(M_EducationHistoryReq m)
        {
            // Example usage within an asynchronous method
            JsonResult results = new JsonResult();
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.SCHOOL_NAME))
                    cek += "School name, ";
                if (string.IsNullOrWhiteSpace(m.GPA))
                    cek += "GPA, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }

                var files = m.CERTIFICATE;
                var result = repo.InsertData(m);
                results = Json(result.Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var fail = new Result();
                fail.ResultCode = false;
                fail.Message = ex.Message;
                Response.StatusCode = 400;
                return Json(fail, JsonRequestBehavior.AllowGet);
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
            //User user = Lookup.Get<User>();
            return results;
        }
        #endregion


        //UpdateEducationData
        #region UpdateEducationHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateEducationHistory(M_EducationHistoryReq m)
        {
            // Example usage within an asynchronous method
            try
            {
                var files = m.CERTIFICATE;
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.SCHOOL_NAME))
                    cek += "School name, ";
                if (string.IsNullOrWhiteSpace(m.GPA))
                    cek += "GPA, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }

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
            var result = repo.UpdateData(m);
            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        //
        #region Delete Data
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteEducationHistory(EducationHistory m)
        {
            //User user = Lookup.Get<User>();
            return Json(repo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion



        //
    }

}