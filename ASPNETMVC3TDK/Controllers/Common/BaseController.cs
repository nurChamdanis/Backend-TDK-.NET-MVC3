using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Toyota.Common.Download;
using NPOI.SS.UserModel;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;

namespace ASPNETMVC3TDK.Controllers.Common
{
    public class BaseController : PageController
    {
        public  User GetCurrentUser()
        {
            User currentUser = Lookup.Get<User>();
            if (currentUser == null)
            {
                //Dev
                //throw new InvalidOperationException("Bug: User is null");
                currentUser = new User();
                currentUser.Username = "dev";
            }

            return currentUser;
        }

        public String GetCurrentUsername()
        {
            User currentUser = GetCurrentUser();
            return currentUser.Username;
        }

        public  String GetCurrentRegistrationNumber()
        {
            User currentUser = GetCurrentUser();
            return currentUser.RegistrationNumber;
        }

        public  String GetCurrentUserFullName()
        {
            User currentUser = GetCurrentUser();
            return currentUser.Name;
        }

        public ActionResult ShowErrorMessages()
        {
            List<string> errorMessages = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
            }
            Response.StatusCode = 400;
            ViewData["Messages"] = errorMessages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
        public ActionResult ShowInfoMessages(string messages)
        {
            List<string> errorMessages = new List<string>();
            errorMessages.Add(messages);
            ViewData["Messages"] = errorMessages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
        public ActionResult ShowInfoMessages(string[] messages)
        {
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
        public ActionResult ShowErrorMessages(string messages)
        {
            List<string> errorMessages = new List<string>();
            Response.StatusCode = 400;
            errorMessages.Add(messages);
            ViewData["Messages"] = errorMessages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
        public ActionResult ShowErrorMessages(string[] messages)
        {
            Response.StatusCode = 400;
            ViewData["Messages"] = messages;
            return PartialView("~/Views/Shared/_MessagePanel.cshtml");
        }
        protected ActionResult Download(DownloadConfig downloadConfig, String downloadFileName, string sqlFiles, bool removePaging, params object[] args)
        {
            try
            {
                XlsxDownloadProcessor download = new XlsxDownloadProcessor(downloadConfig);
                IWorkbook wb = download.Download(sqlFiles, removePaging, args);
                using (var exportData = new MemoryStream())
                {
                    wb.Write(exportData);
                    string saveAsFileName = downloadFileName + string.Format("_{0:yyyyMMddHHmmss}.xlsx", DateTime.Now);
                    byte[] bytes = exportData.ToArray();
                    wb.Close();
                    return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        saveAsFileName);
                }
            }
            catch (XlsxDownloadException e)
            {
                return ShowErrorMessages(e.Message);
            }
        }
    }
}