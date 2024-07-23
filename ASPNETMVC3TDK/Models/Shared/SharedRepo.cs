using System.Linq;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;

namespace ASPNETMVC3TDK.Models.Shared
{
	public class SharedRepo
	{
		#region Singleton

		private static SharedRepo instance = null;
		IDBContext db = DatabaseManager.Instance.GetContext();
		public static SharedRepo Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new SharedRepo();
				}
				return instance;
			}

		}

		#endregion

		public string GetMessage(string MESSAGE_ID, string PARAM_1, string PARAM_2, string PARAM_3, string PARAM_4)
		{
			string result = db.Fetch<string>("EXEC SP_Get_Message '" + MESSAGE_ID + "', '" + PARAM_1 + "', '" + PARAM_2 + "', '" + PARAM_3 + "', '" + PARAM_4 + "'").FirstOrDefault();
			db.Close();
			return result;
		}
	}
}