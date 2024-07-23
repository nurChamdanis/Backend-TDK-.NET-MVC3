using System;
using Toyota.Common;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using System.IO;
using System.Text;
using NPOI.SS.Formula.Functions;
using ASPNETMVC3TDK.Models.Class;
using ASPNETMVC3TDK.Models.Position;
using ASPNETMVC3TDK.Models.Skill;
using System.Text.RegularExpressions;
using System.Windows.Shapes;
using System.Windows;
using Toyota.Common.Credential;
using ASPNETMVC3TDK.Models.MasterSystem;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ASPNETMVC3TDK.Models
{
    public class PdfApi_Model
    {
        public string FILE_ID { get; set; }
        public string FILE_LOCATION { get; set; }
        public string CREATED_BY { get; set; }
        public string CHANGED_BY { get; set; }
        public string CREATED_DT { get; set; }
        public string CHANGED_DT { get; set; }


        private static PdfApi_Model instance = null;
        static IDBContext db = DatabaseManager.Instance.GetContext();
        public static PdfApi_Model Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PdfApi_Model();
                }
                return instance;
            }
        }
        public Result setInsertPDF(PdfApi_Model data)
        {
            Result result = new Result();
            string mode = "add";
            var cek = cekDataPdf(data.FILE_ID);
            if (cek.ResultCode == true)
                mode = "edit";
            dynamic args = new
            {
                MODE = mode,
                FILE_ID = data.FILE_ID,
                FILE_LOCATION = data.FILE_LOCATION,
                CREATED_BY = data.CREATED_BY,
                //CREATED_DT = DateTime.Now,
            };

            List<ResultMessage> Result = db.Fetch<ResultMessage>("PDF/PDF_SaveData", args);
            result.ResultCode = Result[0].result == "true" ? true : false;
            result.Message = Result[0].msg;
            db.GetSqlLoaders();
            db.Close();
            return result;
        }
        public Result getUrl()
        {
            var result = db.Fetch<Result>("PDF/PDF_getUrl");
            db.GetSqlLoaders();
            db.Close();
            return result[0];
        }

        public Result cekDataPdf(string namaFile)
        {
            Result hasil = new Result();
            dynamic cek = new
            {
                FILE_ID = namaFile
            };
            List<PdfApi_Model> result = db.Fetch<PdfApi_Model>("PDF/PDF_cekFile", cek);
            db.GetSqlLoaders();
            db.Close();
            if (result.Count > 0)
                hasil.ResultCode = true;
            else
                hasil.ResultCode = false;
            return hasil;
        }
    }
}