using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.Application;

namespace ASPNETMVC3TDK.Controllers
{
    public class ApplicationApiController : PageController
    {
        ApplicationRepo repo = new ApplicationRepo();
        DataTables datatable = new DataTables();

        #region Get List Application
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GetListApplications()
        {
            try
            {
                var listApplication = repo.GetListApplications();

                return Json(listApplication, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Get List Type
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult GetListType()
        {
            try
            {
                var listType = repo.GetListType();

                return Json(listType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Error: " + ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Save Data
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult SaveData(Application m)
        {
            User user = Lookup.Get<User>();

            return Json(repo.SaveData(m, user.Username), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete Data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteData(FormCollection form)
        {
            User user = Lookup.Get<User>();
            return Json(repo.DeleteData(form), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Datatable Module
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetDatatables()
        {
            User user = Lookup.Get<User>();
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            datatable.draw = draw != null ? Convert.ToInt32(draw) : 0;
            try
            {
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var order_type = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var idx_column = Request.Form.GetValues("order[0][column]").FirstOrDefault();
                var order_name = Request.Form.GetValues("columns[" + idx_column + "][name]").FirstOrDefault();

                //Param
                var NAME = Request.Form.GetValues("NAME").FirstOrDefault();
                var TYPE = Request.Form.GetValues("TYPE").FirstOrDefault();
                var DESCRIPTION = Request.Form.GetValues("DESCRIPTION").FirstOrDefault();


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                datatable.data = repo.GetDataApplication(skip, pageSize, order_name, order_type, NAME, TYPE, DESCRIPTION);
                datatable.recordsFiltered = repo.GetDataApplicationCount(NAME, TYPE, DESCRIPTION);
                datatable.recordsTotal = datatable.recordsFiltered;

                //Returning Json Data    
                return Json(datatable);
            }
            catch (Exception err)
            {
                datatable.data = new { err.InnerException, err.Message };
                datatable.status = false;
                return Json(datatable);
            }
        }
        #endregion



    }
}
