using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK;
using ASPNETMVC3TDK.Models.DownloadFile;
using ASPNETMVC3TDK.Models;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ASPNETMVC3TDK.Controllers
{
    public class DownloadFileController : PageController
    {

        DownloadFileRepo repo = new DownloadFileRepo();
        DownloadFile system = new DownloadFile();

        #region Save Data
        [HttpPost]
        public JsonResult download(string REPORT_TYPE, string SHIFT_CD, string REPORT_PERIOD, string PRODUCT_TYPE)
        {
            try
            {
                string[] monthNames = new string[] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
                string pathFile = "";
                string pathString = "";
                string fileName = "";
                string shiftName = (SHIFT_CD == "R") ? "Red" : "White";
                string productTypeNm = "";
                string[] splitPeriod = REPORT_PERIOD.Split('-');
                int indexMonth = Convert.ToInt32(splitPeriod[0]) - 1;
                List <ResponseSelect2> reportData = CommonHelper.getCmbOptions("REPORT_LIST");

                if (PRODUCT_TYPE == "1"){productTypeNm = "1TR";}
                else if (PRODUCT_TYPE == "2"){productTypeNm = "2TR";}
                else if (PRODUCT_TYPE == "3"){productTypeNm = "Camshaft";}
                else if (PRODUCT_TYPE == "4"){productTypeNm = "Crank";}

                if(REPORT_TYPE == "MELT_ASPNETMVC3TDKRO3")
                {
                    pathFile = HttpContext.Request.MapPath("~/Content/download/Report_ASPNETMVC3TDKR03/" + shiftName + "/");
                    pathString = "/Content/download/Report_ASPNETMVC3TDKR03/" + shiftName + "/";
                    
                    fileName = reportData.Where(s => s.id == "MELT_ASPNETMVC3TDKRO3").Select(s => s.attr_1).FirstOrDefault();
                    fileName = fileName.Replace("{shift}", shiftName);
                    fileName = fileName.Replace("{month}", monthNames[indexMonth]);
                    fileName = fileName.Replace("{year}", splitPeriod[1]);
                }else if(REPORT_TYPE == "MOULD_ASPNETMVC3TDKR08")
                {
                    pathFile = HttpContext.Request.MapPath("~/Content/download/Report_ASPNETMVC3TDKR08/" + shiftName + "/");
                    pathString = "/Content/download/Report_ASPNETMVC3TDKR08/" + shiftName + "/";
                    
                    fileName = reportData.Where(s => s.id == "MOULD_ASPNETMVC3TDKR08").Select(s => s.attr_1).FirstOrDefault();
                    fileName = fileName.Replace("{shift}", shiftName);
                    fileName = fileName.Replace("{month}", monthNames[indexMonth]);
                    fileName = fileName.Replace("{year}", splitPeriod[1]);
                }
                else if(REPORT_TYPE == "POUR_ASPNETMVC3TDKR12")
                {
                    pathFile = HttpContext.Request.MapPath("~/Content/download/Report_ASPNETMVC3TDKR12/" + shiftName + "/");
                    pathString = "/Content/download/Report_ASPNETMVC3TDKR12/" + shiftName + "/";

                    fileName = reportData.Where(s => s.id == "POUR_ASPNETMVC3TDKR12").Select(s => s.attr_1).FirstOrDefault();
                    fileName = fileName.Replace("{shift}", shiftName);
                    fileName = fileName.Replace("{month}", monthNames[indexMonth]);
                    fileName = fileName.Replace("{year}", splitPeriod[1]);
                }
                else if(REPORT_TYPE == "QCGATE_ASPNETMVC3TDKR17")
                {
                    pathFile = HttpContext.Request.MapPath("~/Content/download/Report_ASPNETMVC3TDKR17" + "/" + productTypeNm + "/" + shiftName + "/");
                    pathString = "/Content/download/Report_ASPNETMVC3TDKR17" + "/" + productTypeNm + "/" + shiftName + "/";

                    fileName = reportData.Where(s => s.id == "QCGATE_ASPNETMVC3TDKR17").Select(s => s.attr_1).FirstOrDefault();
                    fileName = fileName.Replace("{product_type}", productTypeNm);
                    fileName = fileName.Replace("{shift}", shiftName);
                    fileName = fileName.Replace("{month}", monthNames[indexMonth]);
                    fileName = fileName.Replace("{year}", splitPeriod[1]);
                }
                else if(REPORT_TYPE == "QCGATE_ASPNETMVC3TDKR19")
                {
                    pathFile = HttpContext.Request.MapPath("~/Content/download/Report_ASPNETMVC3TDKR19" + "/" + shiftName + "/");
                    pathString = "/Content/download/Report_ASPNETMVC3TDKR19" + "/" + shiftName + "/";

                    fileName = reportData.Where(s => s.id == "QCGATE_ASPNETMVC3TDKR19").Select(s => s.attr_1).FirstOrDefault();
                    fileName = fileName.Replace("{shift}", shiftName);
                    fileName = fileName.Replace("{month}", monthNames[indexMonth]);
                    fileName = fileName.Replace("{year}", splitPeriod[1]);
                } 
                else if(REPORT_TYPE == "SAND_ASPNETMVC3TDKR21")
                {
                    pathFile = HttpContext.Request.MapPath("~/Content/download/Report_ASPNETMVC3TDKR21" + "/" + shiftName + "/");
                    pathString = "/Content/download/Report_ASPNETMVC3TDKR21" + "/" + shiftName + "/";

                    fileName = reportData.Where(s => s.id == "SAND_ASPNETMVC3TDKR21").Select(s => s.attr_1).FirstOrDefault();
                    fileName = fileName.Replace("{shift}", shiftName);
                    fileName = fileName.Replace("{month}", monthNames[indexMonth]);
                    fileName = fileName.Replace("{year}", splitPeriod[1]);
                }

                ResultMessage res = new ResultMessage();
                if (!string.IsNullOrEmpty(fileName) && System.IO.File.Exists(pathFile + fileName))
                {
                    res.result = "true";
                    res.msgInfo = "success";
                    res.msg = pathString;
                    res.extraAttribute = fileName;
                }
                else
                {
                    res.result = "false";
                    res.msgInfo = "failed";
                    res.msg = "File not exists";
                    res.extraAttribute = "";

                }
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                ResultMessage res = new ResultMessage();
                res.result = "false";
                res.msgInfo = "failed";
                res.msg = "Error when checking file. Error: " + err.Message.ToString();

                return Json(res, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}
