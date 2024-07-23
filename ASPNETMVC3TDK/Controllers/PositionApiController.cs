using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.Position; 
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ASPNETMVC3TDK.Controllers
{
    public class PositionApiController : PageController
    {

        PositionRepo repo = new PositionRepo();
        DataTables datatable = new DataTables();

        #region getPosition
        [HttpPost]
        public JsonResult getPosition(Position m)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = repo.getPosition(m.NOREG, "DESC", "4", m);
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