using System.Collections.Generic;
using System.Web.Mvc;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;


namespace ASPNETMVC3TDK.Models.AuthManagement
{
    public class AuthManagementRepo
    {
        #region Singleton

        private static AuthManagementRepo instance = null;
        //IDBContext db = DatabaseManager.Instance.GetContext();
        IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");

        public static AuthManagementRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthManagementRepo();
                }
                return instance;
            }

        }

        #endregion


        public IList<AuthManagement> GetDataAuthManagement(int P_START, int P_LENGTH, string P_ORDER_BY, string P_ORDER_TYPE, string ID, string NAME)
        {
            dynamic args = new
            {
                P_START,
                P_LENGTH,
                P_ID = ID?.Replace("'", "`"),
                P_NAME = NAME?.Replace("'", "`"),
                P_FROMNUMBER = P_START,
                P_TONUMBER = P_START + P_LENGTH,
                P_ORDER_BY,
                P_ORDER_TYPE,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<AuthManagement> Result = db2.Fetch<AuthManagement>("AuthManagement/AuthManagement_GetDataAuthManagement", args);
            db2.Close();
            return Result;
        }

        public int GetDataAuthManagementCount(string ID, string NAME)
        {
            dynamic args = new
            {
                P_ID = ID?.Replace("'", "`"),
                P_NAME = NAME?.Replace("'", "`"),
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            int Result = db2.Fetch<int>("AuthManagement/AuthManagement_GetDataAuthManagementCount", args)[0];
            db2.Close();
            return Result;
        }

        public IList<AuthManagement> GetDataFunctionAuthManagement(int P_START, int P_LENGTH, string P_ORDER_BY, string P_ORDER_TYPE, string ID, string NAME, string ROLE)
        {
            dynamic args = new
            {
                P_START,
                P_LENGTH,
                P_ID = ID?.Replace("'", "`"),
                P_NAME = NAME?.Replace("'", "`"),
                P_ROLE = ROLE,
                P_FROMNUMBER = P_START,
                P_TONUMBER = P_START + P_LENGTH,
                P_ORDER_BY,
                P_ORDER_TYPE,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<AuthManagement> Result = db2.Fetch<AuthManagement>("AuthManagement/AuthManagement_GetDataFunctionAuthManagement", args);
            db2.Close();
            return Result;
        }

        public int GetDataFunctionAuthManagementCount(string ID, string NAME, string ROLE)
        {
            dynamic args = new
            {
                P_ID = ID?.Replace("'", "`"),
                P_NAME = NAME?.Replace("'", "`"),
                P_ROLE = ROLE,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            int Result = db2.Fetch<int>("AuthManagement/AuthManagement_GetDataFunctionAuthManagementCount", args)[0];
            db2.Close();
            return Result;
        }

        public IList<AuthManagement> GetDataFeatureAuthManagement(int P_START, int P_LENGTH, string P_ORDER_BY, string P_ORDER_TYPE, string ID, string NAME, string ROLE, string FUNCTION)
        {
            dynamic args = new
            {
                P_START,
                P_LENGTH,
                P_ID = ID?.Replace("'", "`"),
                P_NAME = NAME?.Replace("'", "`"),
                P_ROLE = ROLE,
                P_FUNCTION = FUNCTION,
                P_FROMNUMBER = P_START,
                P_TONUMBER = P_START + P_LENGTH,
                P_ORDER_BY,
                P_ORDER_TYPE,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<AuthManagement> Result = db2.Fetch<AuthManagement>("AuthManagement/AuthManagement_GetDataFeatureAuthManagement", args);
            db2.Close();
            return Result;
        }

        public int GetDataFeatureAuthManagementCount(string ID, string NAME, string ROLE, string FUNCTION)
        {
            dynamic args = new
            {
                P_ID = ID?.Replace("'", "`"),
                P_NAME = NAME?.Replace("'", "`"),
                P_ROLE = ROLE,
                P_FUNCTION = FUNCTION,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            int Result = db2.Fetch<int>("AuthManagement/AuthManagement_GetDataFeatureAuthManagementCount", args)[0];
            db2.Close();
            return Result;
        }

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

        public IList<ResponseSelect2> GetListFunction(string ROLE)
        {
            dynamic args = new
            {
                P_ROLE = ROLE,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<ResponseSelect2> Result = db2.Fetch<ResponseSelect2>("Shared/GetListFunction", args);
            db2.Close();
            return Result;
        }

        public IList<ResponseSelect2> GetListFeature(string ROLE, string FUNCTION)
        {
            dynamic args = new
            {
                P_ROLE = ROLE,
                P_FUNCTION = FUNCTION,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };
            IList<ResponseSelect2> Result = db2.Fetch<ResponseSelect2>("Shared/GetListFeature", args);
            db2.Close();
            return Result;
        }

        public ResultMessage AssignFunction(AuthManagement m, string USERNAME)
        {
            dynamic args = new
            {
                P_ROLE = m.ROLE,
                P_FUNCTION = m.FUNCTION,
                P_USERNAME = USERNAME,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };

            ResultMessage Result = db2.Fetch<ResultMessage>("AuthManagement/AuthManagement_AssignFunction", args)[0];
            db2.Close();
            return Result;
        }
        public ResultMessage AssignFeature(AuthManagement m, string USERNAME)
        {
            dynamic args = new
            {
                P_ROLE = m.ROLE,
                P_FUNCTION = m.FUNCTION,
                P_FEATURE = m.FEATURE,
                P_USERNAME = USERNAME,
                P_APPALIAS = ApplicationSettings.Instance.Alias
            };

            ResultMessage Result = db2.Fetch<ResultMessage>("AuthManagement/AuthManagement_AssignFeature", args)[0];
            db2.Close();
            return Result;
        }
        public ResultMessage DeleteFunction(FormCollection form)
        {
            dynamic args = new
            {
                P_FUNCTIONS = form["criteria"]
            };

            ResultMessage Result = db2.Fetch<ResultMessage>("AuthManagement/AuthManagement_DeleteFunction", args)[0];
            db2.Close();
            return Result;
        }
        public ResultMessage DeleteFeature(FormCollection form)
        {
            dynamic args = new
            {
                P_FEATURES = form["criteria"]
            };

            ResultMessage Result = db2.Fetch<ResultMessage>("AuthManagement/AuthManagement_DeleteFeature", args)[0];
            db2.Close();
            return Result;
        }

    }
}