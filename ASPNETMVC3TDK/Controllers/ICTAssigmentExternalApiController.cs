using System;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.ICTAssigmentExternal;
using Toyota.Common;

namespace ASPNETMVC3TDK.Controllers.Pages
{
    public class ICTAssigmentExternalApiController : PageController
    {

        ICTAssigmentExternalRepo repo = new ICTAssigmentExternalRepo();
        DataTables datatable = new DataTables();


        #region getICTAssigmentExternal
        [HttpPost]
        public JsonResult getICTAssigmentExternal(M_ICTAssigmentExternal m, bool MODEMASTER)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = repo.getICTAssigmentExternal(m.NOREG, "DESC", "4", MODEMASTER, m);
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



        //UpdateRotationHistory
        #region updateICTAssigmentExternal
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult updateICTAssigmentExternal(M_ICTAssigmentExternal m)
        {
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.HOST_CLASS))
                    cek += "Host class, ";
                if (string.IsNullOrWhiteSpace(m.HOST_COMPANY))
                    cek += "HOST Company, ";
                if (string.IsNullOrWhiteSpace(m.HOST_COUNTRY))
                    cek += "Host country, ";
                if (string.IsNullOrWhiteSpace(m.HOST_DEPARTMENT))
                    cek += "HOST Department, ";
                if (string.IsNullOrWhiteSpace(m.HOST_DIVISION))
                    cek += "Host division, ";
                if (string.IsNullOrWhiteSpace(m.HOST_POSITION))
                    cek += "HOST position, ";
                if (string.IsNullOrWhiteSpace(m.HOST_SECTION))
                    cek += "Host section, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }

                //User user = Lookup.Get<User>();
                return Json(repo.UpdateData(m), JsonRequestBehavior.AllowGet);

            }catch(Exception ex)
            {
                var fail = new Result();
                fail.ResultCode = false;
                fail.Message = ex.Message;
                Response.StatusCode = 400;
                return Json(fail, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion



        //UpdateRotationHistory
        #region deleteICTAssigmentExternal
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult deleteICTAssigmentExternal(M_ICTAssigmentExternal m)
        {
            //User user = Lookup.Get<User>();
            return Json(repo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion





    }
}