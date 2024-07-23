using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.RotationHistory;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ASPNETMVC3TDK.Controllers
{
    public class RotationHistoryApiController : PageController
    {
        RotationHistoryRepo repo = new RotationHistoryRepo();
        DataTables datatable = new DataTables();


        #region GetRotationHistory
        [HttpPost]
        public JsonResult GetRotationHistory(M_RotationHistory m, bool MODEMASTER)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = repo.getRotationHistory(m.NOREG, "DESC", "4", MODEMASTER, m);
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


        //InsertRotationHistory
        /*        #region InsertEducationHistory
                [HttpPost]
                //[ValidateAntiForgeryToken]
                public JsonResult InsertRotationHistory(RotationHistory m)
                {
                    //User user = Lookup.Get<User>();
                    return Json(repo.InsertData(m), JsonRequestBehavior.AllowGet);
                }
                #endregion*/



        //UpdateRotationHistory
        #region UpdateRotationHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateRotationHistory(M_RotationHistory m)
        {
            //User user = Lookup.Get<User>();
            return Json(repo.UpdateData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion



        //DeleteRotationHistory
        #region DeleteRotationHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteRotationHistory(M_RotationHistory m)
        {
            //User user = Lookup.Get<User>();
            return Json(repo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}