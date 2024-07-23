using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.ProjectAssignment;
using System.Linq.Expressions;
using System.Collections.Generic;
using Toyota.Common;

namespace ASPNETMVC3TDK.Controllers
{
    public class ProjectAsistantManagerApiController : PageController
    {
        ProjectAssignmentRepo repo = new ProjectAssignmentRepo();
        DataTables datatable = new DataTables();


        #region GetRotationHistory
        [HttpPost]
        public JsonResult GetProjectAssignment(M_ProjectAssignment m, bool MODEMASTER)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = repo.getProjectAssignment(m.NOREG, "DESC", "4", MODEMASTER, m);
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
                    status = 500 ,
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
        public JsonResult updateProjectAssignment(M_ProjectAssignment m)
        {
            //User user = Lookup.Get<User>();
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.PROJECT_NAME))
                    cek = "Project name, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }

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
        #region deleteProjectAssignment
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult deleteProjectAssignment(M_ProjectAssignment m)
        {
            //User user = Lookup.Get<User>();
            return Json(repo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion




    }
}