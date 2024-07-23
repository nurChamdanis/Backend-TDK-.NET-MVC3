using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using PetaPoco;


namespace ASPNETMVC3TDK.Models.MasterSystem
{
    public class MasterSystemRepo
    {
		#region Singleton

		private static MasterSystemRepo instance = null;


		// ============== COMMENT JIKA INGIN MENGGUNAKAN UNIT TEST ====================
		IDBContext db = DatabaseManager.Instance.GetContext();
		// ============== END COMMENT JIKA INGIN MENGGUNAKAN UNIT TEST =================


		// ============== UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST ====================
		/*Database _db;
		DatabaseSpExecutor db;

		public MasterSystemRepo()
		{
			// Initialize db2 here
			_db = new Database("server=192.168.4.6\\MSSQL2016; database=SKELETON_DB; uid=development; password=*!guntursari17!*; Trusted_Connection=false; MultipleActiveResultSets=true; Max Pool Size=1000; Timeout=10000", "System.Data.SqlClient");
			db = new DatabaseSpExecutor(_db);
		}*/
		// ============== END UNCOMMENT JIKA INGIN MENGGUNAKAN UNIT TEST =================

		public static MasterSystemRepo Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MasterSystemRepo();
				}
				return instance;
			}

		}

		#endregion

		public ResultMessage SaveData(MasterSystem m, string USERNAME)
        {
			ResultMessage result = db.Fetch<ResultMessage>("EXEC SP_Master_System_Save 'xxxx', '" + m.MODE + "', '" + USERNAME + "', '" + m.JSON_DATA + "'")[0];
			db.Close();
			return result;
        }

        public IList<ResponseSelect2> GetDataOptions(string PARAM_1, string PARAM_2, string PARAM_3, string PARAM_4)
        {
            IList<ResponseSelect2> result = db.Fetch<ResponseSelect2>("EXEC SP_Master_System_Get_Options '" + PARAM_1 + "', '" + PARAM_2 + "', '" + PARAM_3 + "', '" + PARAM_4 + "'");

			db.Close();
			return result;
		}

		public IList<MasterSystem> GetDataSystem(string MODE, int P_START, int P_LENGTH, string SYSTEM_TYPE, string SYSTEM_CD, string ORDER, string DIR)
        {
            IList<MasterSystem> result = db.Fetch<MasterSystem>("EXEC SP_Master_System_Inquiry '" + MODE + "', '" + P_START + "', '" + P_LENGTH + "', '" + SYSTEM_TYPE + "', '" + SYSTEM_CD + "', '" + ORDER + "', '" + DIR + "'");

			db.Close();
			return result;
        }

		public ResultMessage DeleteData(FormCollection form, string username)
		{
			ResultMessage result = db.Fetch<ResultMessage>("EXEC SP_Master_System_Delete '" + form["criteria"] + "', '" + username + "'")[0];
			db.Close();
			return result;
		}
	}
}