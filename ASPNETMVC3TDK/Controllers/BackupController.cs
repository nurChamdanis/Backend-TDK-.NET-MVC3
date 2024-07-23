using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.UserProfile;
using ASPNETMVC3TDK.Models.UserProfileDescrip;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

using QRCoder;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http;
using Toyota.Common;
using System.Web;

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ASPNETMVC3TDK.Models.Class;
using ASPNETMVC3TDK.Models.People;
using ASPNETMVC3TDK.Models.Position;
using ASPNETMVC3TDK.Models.Skill;
using NPOI.SS.Formula.Functions;
using System.Windows.Shapes;
using System.Windows;
using Toyota.Common.Database;

namespace ASPNETMVC3TDK.Controllers
{
    public class Backup : PageController
    {
        //UserProfileRepo userProfileRepo = new UserProfileRepo();
        //UserProfileDescripRepo userProfileDescripRepo = new UserProfileDescripRepo();
        //DataTables datatable = new DataTables();
        //PdfApi_Model db_file = new PdfApi_Model();

        static IDBContext db = DatabaseManager.Instance.GetContext();
        public async Task<ActionResult> BackupData(string id = "1")
        {
            try
            {
                var result = db.Fetch<Result>("EXEC PDI.sp_getPeriodeBackup @ID;",
                new
                {
                    ID = id
                });
                db.GetSqlLoaders();
                db.Close();
                return Json(new { result = true, message = "Backup successfuly" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}