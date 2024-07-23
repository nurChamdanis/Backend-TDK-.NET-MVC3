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

namespace ASPNETMVC3TDK.Controllers
{
    public class PdfApiController : PageController
    {
        UserProfileRepo userProfileRepo = new UserProfileRepo();
        UserProfileDescripRepo userProfileDescripRepo = new UserProfileDescripRepo();
        DataTables datatable = new DataTables();
        PdfApi_Model db_file = new PdfApi_Model();
        public async Task<ActionResult> GenerateQR(string code = "cekkk")
        {
            try
            {
                var link = db_file.getUrl();
                string setCode = link.Message + "/Content/Upload/PDF/" + code+".pdf";
                using (var qrGenerator = new QRCodeGenerator())
                {
                    using (var qrCodeData = qrGenerator.CreateQrCode(setCode, QRCodeGenerator.ECCLevel.Q))
                    {
                        using (var qrCode = new QRCode(qrCodeData))
                        {
                            using (var qrCodeImage = qrCode.GetGraphic(20))
                            {
                                using (var memoryStream = new System.IO.MemoryStream())
                                {
                                    qrCodeImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                                    byte[] imageBytes = memoryStream.ToArray();
                                    return File(imageBytes, "image/png");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> SavePdf(string nameFile)
        {
            Result respon = new Result();
            try
            {
                HttpPostedFileBase imageData = Request.Files.Get("filePDF");

                if (imageData != null)
                {
                    string root = Server.MapPath(
                        "~/Content/Upload/PDF/"
                    );
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    var file = imageData;
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        var filename_wa = Path.GetFileName(file.FileName);
                        string fileExt = Path.GetExtension(filename_wa);
                        string newname = nameFile + fileExt;
                        string savefile = Path.Combine(
                            Server.MapPath("~/Content/Upload/PDF/"), newname
                        );
                        string resultFilePath = Path.Combine("~/Content/Upload/PDF/", newname);
                        if (fileExt.ToLower() == ".pdf")
                        {
                            file.SaveAs(Server.MapPath(resultFilePath));
                            filename_wa = "Content/Upload/PDF/" + newname;
                            var data = new PdfApi_Model();
                            data.FILE_ID = nameFile;
                            data.FILE_LOCATION = filename_wa;
                            data.CREATED_BY = "sys";
                            respon = db_file.setInsertPDF(data);

                            var link = db_file.getUrl();
                            respon.ResultDesc = link.Message + "/"+filename_wa;
                        }
                        else
                        {
                            respon.Message = "File is not extentions pdf";
                        }
                    }
                }
                else
                {
                    respon.Message = "File not found";

                }

                //respon.ResultCode = true;
            }
            catch (Exception ex)
            {
                respon.ResultCode = false;
                respon.Message = "Error. " + ex.Message;
            }
            return Json(respon, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePdf64(string base64String, string nameFile)
        {
            Result respon = new Result();
            try
            {
                if (string.IsNullOrEmpty(base64String))
                {
                    respon.Message = "Base64 string is empty";
                }
                else
                {
                    // Remove any data:image/jpeg;base64, prefix from the string if present
                    if (base64String.Contains(","))
                    {
                        base64String = base64String.Split(',')[1];
                    }

                    byte[] pdfBytes = Convert.FromBase64String(base64String);
                    string root = Server.MapPath("~/Content/Upload/PDF/");
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }

                    string newname = nameFile + ".pdf";
                    string savefile = Path.Combine(root, newname);
                    string resultFilePath = Path.Combine("~/Content/Upload/PDF/", newname);

                    System.IO.File.WriteAllBytes(savefile, pdfBytes);
                    string filename_wa = "Content/Upload/PDF/" + newname;

                    var data = new PdfApi_Model
                    {
                        FILE_ID = nameFile,
                        FILE_LOCATION = filename_wa,
                        CREATED_BY = "sys"
                    };

                    respon = db_file.setInsertPDF(data);
                    var link = db_file.getUrl();
                    respon.ResultDesc = link.Message + "/" + filename_wa;
                }

                respon.ResultCode = true;
            }
            catch (Exception ex)
            {
                respon.ResultCode = false;
                respon.Message = "Error. " + ex.Message;
            }

            return Json(respon, JsonRequestBehavior.AllowGet);
        }
    }

}