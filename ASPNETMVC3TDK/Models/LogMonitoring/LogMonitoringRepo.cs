using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using PetaPoco;


namespace ASPNETMVC3TDK.Models.LogMonitoring
{
    public class LogMonitoringRepo
    {
        #region Singleton

        private static LogMonitoringRepo instance = null;

		// ============== COMMENT JIKA INGIN MENGGUNAKAN UNIT TEST ====================
		IDBContext db = DatabaseManager.Instance.GetContext();
		// ============== END UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST =================


		// ============== UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST ====================
		/*Database _db;
		DatabaseSpExecutor db;

		public LogMonitoringRepo()
		{
			// Initialize db2 here
			_db = new Database("server=192.168.4.6\\MSSQL2016; database=SKELETON_DB; uid=development; password=*!guntursari17!*; Trusted_Connection=false; MultipleActiveResultSets=true; Max Pool Size=1000; Timeout=10000", "System.Data.SqlClient");
			db = new DatabaseSpExecutor(_db);
		}*/
		// ============== END UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST =================

		public static LogMonitoringRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogMonitoringRepo();
                }
                return instance;
            }

        }
        #endregion

        public IList<LogH> GetDataLog(string MODE, int P_START, int P_LENGTH, string P_PROCESS_DT, string P_END_DT, string P_USER_ID, string P_PROCESS_ID, string P_MODULE_ID, string P_FUNCTION_ID, string P_PROCESS_STS, string ORDER, string DIR)
		{
			string query = $"EXEC SP_Log_Inquiry '{MODE}', '{P_START}', '{P_LENGTH}', '{P_PROCESS_DT}', '{P_END_DT}', '{P_USER_ID}', '{P_PROCESS_ID}', '{P_MODULE_ID}', '{P_FUNCTION_ID}', '{P_PROCESS_STS}', '{ORDER}', '{DIR}'";

            IList<LogH> result = db.Fetch<LogH>(query);
			db.Close();
			return result;
		}
		public IList<LogD> GetDataLogDetail(string MODE, int P_START, int P_LENGTH, string P_PROCESS_ID, string P_MESSAGE_TYPE, string ORDER, string DIR)
		{
			string query = "EXEC SP_Get_Detail_Log '" + MODE + "', '" + P_START + "', '" + P_LENGTH + "', '" + P_PROCESS_ID + "', '" + P_MESSAGE_TYPE + "', '" + ORDER + "', '" + DIR + "'";
			IList<LogD> result = db.Fetch<LogD>(query);
			db.Close();
			return result;
		}

		public IList<ResponseSelect2> GetDataOptions(string PARAM_1, string PARAM_2, string PARAM_3)
		{
			string query = "EXEC SP_Get_Master_Data_Options '" + PARAM_1 + "', '" + PARAM_2 + "', '" + PARAM_3 + "'";

            IList<ResponseSelect2> result = db.Fetch<ResponseSelect2>(query);

			db.Close();
			return result;
		}

	}
}