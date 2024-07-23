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
namespace ASPNETMVC3TDK.Controllers
{
    public class HangfireController : Controller
    {
        public string Update_MasterSystem()
        {
            HangfireShared.UPDATE_MasterSystem();
            return "success";
        }
    }
}
