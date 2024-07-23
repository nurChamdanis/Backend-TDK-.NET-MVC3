using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.Approval; 
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


namespace ASPNETMVC3TDK.Controllers.Pages
{
    public class ApprovalApiController : PageController
    {
        ApprovalRepo repo = new ApprovalRepo();


        #region GetRotationHistory
        [HttpPost]
        public JsonResult getApproval(Approval m)
        {
            User user = Lookup.Get<User>();
            try
            {

                IList<Approval> result = repo.getAllAprove(m.NOREG, "DESC", "4", m);

                var response = new
                {
                    status = 200,
                    data = result,
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