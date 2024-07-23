using ASPNETMVC3TDK.Controllers;
using ASPNETMVC3TDK.Models.MasterSystem;
using Newtonsoft.Json;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace ControllerTest
{
	public class MasterSystemApiControllerTest
	{
		private MasterSystemApiController masterSystemApiController;

		[SetUp]
		public void Setup()
		{
			masterSystemApiController = MasterSystemApiController.Instance;
		}

		[Test]
		public void GetDataOptions()
		{
			string PARAM_1 = "SYSTEM_TYPE";
			string PARAM_2 = "";
			string PARAM_3 = "";
			string PARAM_4 = "";

			var result = masterSystemApiController.GetDataOptions(PARAM_1, PARAM_2, PARAM_3, PARAM_4);
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void saveData()
		{
			var masterSystem = new MasterSystem
			{
				MODE = "ADD",
				JSON_DATA = "[{\"SYSTEM_TYPE\": \"TEST_HANGFIRE\",\"SYSTEM_CD\": \"test\",\"SYSTEM_VALUE_TXT\": \"test\",\"SYSTEM_VALUE_DT\": \"\",\"SYSTEM_VALUE_NUM\": null ,\"SYSTEM_REMARK\": \"\",\"IS_ACTIVE\": \"1\"}]"
			};

			var result = masterSystemApiController.SaveData(masterSystem);
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void getDatatables()
		{
			var result = masterSystemApiController.GetDatatables();
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void deleteData()
		{
			var form = new FormCollection
			{
				{ "criteria", "[{\"SYSTEM_TYPE\":\"APP_SETTING\",\"SYSTEM_CD\":\"test\"}, " +
				"{\"SYSTEM_TYPE\":\"REPORT_POURING\",\"SYSTEM_CD\":\"TESTING\"}]" }
			};

			var result = masterSystemApiController.DeleteData(form);
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}
	}
}
