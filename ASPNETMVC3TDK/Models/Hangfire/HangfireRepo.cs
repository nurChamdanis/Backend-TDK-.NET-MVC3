using System;
using System.Linq;
using System.IO;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
namespace ASPNETMVC3TDK.Models.Hangfire
{
    public class HangfireRepo
    {
        #region Singleton

        private static HangfireRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        public static HangfireRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HangfireRepo();
                }
                return instance;
            }

        }

		#endregion

		public void UpdateMasterSystem()
		{
			string sqlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL", "Shared", "UpdateMasterSystem.sql");
			string sql = File.ReadAllText(sqlFilePath);
			db.Execute(sql, new { });
			db.Close();
		}

		public string GetMessage(string MESSAGE_ID, string PARAM_1, string PARAM_2, string PARAM_3, string PARAM_4)
        {
            string result = db.Fetch<string>("Shared/GetMessage", new { MESSAGE_ID, PARAM_1, PARAM_2, PARAM_3, PARAM_4 }).FirstOrDefault();
            db.Close();
            return result;
        }

        public ResultMessage Save_Log(string PROCESS_ID, string FUNC_ID, string PROCESS_NM, string TYPE, string MSG, string BATCH_STS, string CREATED_BY)
        {
            dynamic args = new
            {
                _PROCESS_ID = PROCESS_ID,
                _FUNC_ID = FUNC_ID,
                _PROCESS_NM = PROCESS_NM,
                _TYPE = TYPE,
                _MSG = MSG,
                _BATCH_STS = BATCH_STS,
                _CREATED_BY = CREATED_BY,
            };
            ResultMessage result = db.SingleOrDefault<ResultMessage>("Hangfire/Hangfire_Report_Save_Log", args);
            db.Close();
            return result;
        }
    }
}