using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using ASPNETMVC3TDK.Models.Hangfire;
using System.Drawing;
using ASPNETMVC3TDK.Shared;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using Hangfire;
using System.Threading;
namespace ASPNETMVC3TDK.Controllers
{
    public class SimulateHangfireController : Controller
    {
        [HttpGet]
        public JsonResult Index() {
            // register queue imidietly running
            var jobId = BackgroundJob.Enqueue(
                // call method or call class object
                () => SimulateBatch.aThousandLoop()
                );

            return Json(new { 
                jobId = jobId
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult recuring() {
            RecurringJob.AddOrUpdate(
            "myrecurringjob",
            () => SimulateBatch.aThousandLoop(),
            Cron.MinuteInterval(30));
            return Json(new
            {
                message="recuring job"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
