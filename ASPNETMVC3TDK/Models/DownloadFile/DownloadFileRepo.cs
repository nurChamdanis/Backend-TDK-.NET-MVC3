using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;


namespace ASPNETMVC3TDK.Models.DownloadFile
{
    public class DownloadFileRepo
    {
        #region Singleton

        private static DownloadFileRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        public static DownloadFileRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DownloadFileRepo();
                }
                return instance;
            }

        }

        #endregion

        public IList<ResponseSelect2> GetDataOptions(string SYSTEM_TYPE, string SYSTEM_CD = "")
        {
            dynamic args = new
            {
                p_SYSTEM_TYPE = SYSTEM_TYPE,
                p_SYSTEM_CD = SYSTEM_CD
            };
            IList<ResponseSelect2> Result = db.Fetch<ResponseSelect2>("DownloadFile/DownloadFile_GetDataOptions", args);

            return Result;
        }

    }
}