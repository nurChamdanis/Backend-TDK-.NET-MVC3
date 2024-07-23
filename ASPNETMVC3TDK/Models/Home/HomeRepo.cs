using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;


namespace ASPNETMVC3TDK.Models.Home
{
    public class HomeRepo
    {
        #region Singleton

        private static HomeRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        public static HomeRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HomeRepo();
                }
                return instance;
            }

        }

        #endregion

        public IList<Home> GetDataTable(string DATE, string USERNAME)
        {
            dynamic args = new
            {
                DATE = DATE?.Replace("'", "`"),
                USERNAME = USERNAME?.Replace("'", "`"),
            };
            IList<Home> Result = db.Fetch<Home>("Home/Home_GetTable", args);
            db.Close();
            return Result;
        }
    }
}