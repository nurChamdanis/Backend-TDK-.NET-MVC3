using ASPNETMVC3TDK.Models.MasterSystem;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace ASPNETMVC3TDKTEST
{
	public class MasterSystemRepoTest
	{
		private MasterSystemRepo masterSystemRepo;

		[SetUp]
		public void Setup()
		{
			masterSystemRepo = MasterSystemRepo.Instance;
		}

		[Test, Order(3)]
		public void SaveData()
		{
			string username = "arkamaya.yesica";
			var masterSystem = new MasterSystem
			{
				MODE = "ADD",
				JSON_DATA = "[{\"SYSTEM_TYPE\": \"TEST_HANGFIRE\",\"SYSTEM_CD\": \"test\",\"SYSTEM_VALUE_TXT\": \"test\",\"SYSTEM_VALUE_DT\": \"\",\"SYSTEM_VALUE_NUM\": null ,\"SYSTEM_REMARK\": \"\",\"IS_ACTIVE\": \"1\"}]"
			};

			var result = masterSystemRepo.SaveData(masterSystem, username);
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test, Order(1)]
		public void GetDataOptions()
		{
			string PARAM_1 = "SYSTEM_TYPE";
			string PARAM_2 = "";
			string PARAM_3 = "";
			string PARAM_4 = "";

			var listSystem = masterSystemRepo.GetDataOptions(PARAM_1, PARAM_2, PARAM_3, PARAM_4);
			var result = JsonConvert.SerializeObject(listSystem);

			Assert.Pass(result);
		}

		[Test, Order(2)]
		public void GetDataSystem()
		{
			string MODE = "row";
			int P_START = 0;
			int P_LENGTH = 10;
			string SYSTEM_TYPE = "";
			string SYSTEM_CD = "";
			string ORDIR = "";
			string DIR = "";

			var listSystem = masterSystemRepo.GetDataSystem(MODE, P_START, P_LENGTH, SYSTEM_TYPE, SYSTEM_CD, ORDIR, DIR);
			var result = JsonConvert.SerializeObject(listSystem);

			Assert.Pass(result);
		}

		[Test, Order(4)]
		public void DeleteData()
		{
			string username = "arkamaya.yesica";
			var form = new FormCollection
			{
				{ "criteria", "[{\"SYSTEM_TYPE\":\"APP_SETTING\",\"SYSTEM_CD\":\"test\"}, " +
				"{\"SYSTEM_TYPE\":\"REPORT_POURING\",\"SYSTEM_CD\":\"TESTING\"}]" }
			};

			var result = masterSystemRepo.DeleteData(form, username);
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}
	}
}