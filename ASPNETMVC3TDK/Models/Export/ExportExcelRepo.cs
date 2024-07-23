using Toyota.Common.Web.Platform;
using Toyota.Common.Database; 
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel; 
using System.Threading.Tasks; 
using System.IO;
using System.Collections.Generic; 
using ASPNETMVC3TDK.Models.UserProfile;
using ASPNETMVC3TDK.Models.People;

namespace ASPNETMVC3TDK.Models.Export   
{
    public class ExportExcelRepo
    {
        #region Singleton

        private static ExportExcelRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static ExportExcelRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportExcelRepo();
                }
                return instance;
            } 
        }
        #endregion

        PeopleRepo peopleRepo = new PeopleRepo();
        UserProfileRepo userProfileRepo = new UserProfileRepo();

        public async Task<ResultMessage> getExportExcel(dynamic NOREG, dynamic ARRAY_NOREG, dynamic SUPER, dynamic peoples )
        {  
            ResultMessage responses = new ResultMessage();
            IWorkbook workbook = new XSSFWorkbook();
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Content/Excel");
            dynamic P_ARRAY_NOREG = ARRAY_NOREG;
            dynamic args = new
            {
                P_NOREG = P_ARRAY_NOREG,
                P_ORDER_TYPE = "DESC",
                P_STATUS = "4"
            };
            var final_location = path + "/LIST_" + NOREG + ".xlsx";
            var filename = "LIST_" + NOREG + ".xlsx";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            } 
            bool rest = await FileUploadExcelHelper.UploadExcel(args, db, workbook, final_location, peoples);
            IList<M_Location> dataRotation = db.Fetch<M_Location>("MasterDataApi/Location/getLocation");
            dynamic SYSTEM_VALUE = dataRotation[0].SYSTEM_VALUE;
            if (rest)
            {
                responses.status = "200";
                responses.data = SYSTEM_VALUE+ "/Content/Excel/" + filename;
            }
            else
            {
                responses.status = "400";
                responses.data = "";
            }
            db.GetSqlLoaders();
            db.Close();
            return responses;
        }

        

    }
}