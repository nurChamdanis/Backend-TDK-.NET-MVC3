using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;


namespace ASPNETMVC3TDK.Models.Application
{
    public class ApplicationRepo
    {
        #region Singleton

        private static ApplicationRepo instance = null;
        //IDBContext db = DatabaseManager.Instance.GetContext();
		IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");

		public static ApplicationRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationRepo();
                }
                return instance;
            }

        }

        #endregion

        public IList<ResponseSelect2> GetListApplications()
        {
            dynamic args = new
            {
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<ResponseSelect2> Result = db2.Fetch<ResponseSelect2>("Shared/GetCurrentApplication", args);
            db2.Close();
            return Result;
        }
        public IList<Application> GetDataApplication(int P_START, int P_LENGTH, string P_ORDER_BY, string P_ORDER_TYPE, string NAME, string TYPE, string DESCRIPTION)
        {
            dynamic args = new
            {
                P_START,
                P_LENGTH,
                P_NAME  = NAME?.Replace("'", "`"),
                P_TYPE = TYPE?.Replace("'", "`"),
                P_DESCRIPTION = DESCRIPTION?.Replace("'", "`"),
                P_FROMNUMBER = P_START,
                P_TONUMBER = P_START + P_LENGTH,
                P_ORDER_BY,
                P_ORDER_TYPE
            };
            IList<Application> Result = db2.Fetch<Application>("Application/Application_GetDataApplication", args);
            db2.Close();
            return Result;
        }

        public int GetDataApplicationCount(string NAME, string TYPE, string DESCRIPTION)
        {
            dynamic args = new
            { 
                P_NAME = NAME?.Replace("'", "`"),
                P_TYPE = TYPE?.Replace("'", "`"),
                P_DESCRIPTION = DESCRIPTION?.Replace("'", "`"),
            };
            int Result = db2.Fetch<int>("Application/Application_GetDataApplicationCount", args)[0];
            db2.Close();
            return Result;
        }

		public IList<ResponseSelect2> GetListType()
		{
			IList<ResponseSelect2> Result = db2.Fetch<ResponseSelect2>("Application/GetTypeApplication");
			db2.Close();
			return Result;
		}

		public ResultMessage SaveData(Application m, string USERNAME)
		{
			dynamic args = new
			{
                P_ID = m.ID,
				P_NAME = m.NAME,
                P_TYPE = m.TYPE,
                P_RUNTIME = m.RUNTIME,
				P_DESCRIPTION = m.DESCRIPTION,
				P_USERNAME = USERNAME,
				P_MODE = m.MODE,
			};

			ResultMessage Result = db2.Fetch<ResultMessage>("Application/Application_SaveData", args)[0];
			db2.Close();
			return Result;
		}
        public ResultMessage DeleteData(FormCollection form)
	    {
		    dynamic args = new
		    {
				P_IDS = form["criteria"]
			};

		    ResultMessage Result = db2.Fetch<ResultMessage>("Application/Application_DeleteData", args)[0];
		    db2.Close();
		    return Result;
	    }

    }
}