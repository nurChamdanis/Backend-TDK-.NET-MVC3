using ASPNETMVC3TDK.Models.LogMonitoring;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace ASPNETMVC3TDKTEST
{
	public class LogMonitoringRepoTest
	{
		private LogMonitoringRepo logMonitoringRepo;

		[SetUp]
		public void Setup()
		{
			logMonitoringRepo = LogMonitoringRepo.Instance;
		}

		[Test, Order(1)]
		public void GetDataOptions()
		{
			string PARAM_1 = "MODULE_ID";
			string PARAM_2 = "";
			string PARAM_3 = "";

			var listLog = logMonitoringRepo.GetDataOptions(PARAM_1, PARAM_2, PARAM_3);
			var result = JsonConvert.SerializeObject(listLog);

			Assert.Pass(result);
		}

		[Test, Order(2)]
		public void GetDataLog()
		{
			string MODE = "row";
			int P_START = 0;
			int P_LENGTH = 10;
			string P_PROCESS_DT = "";
			string P_END_DT = "";
			string P_USER_ID = "";
			string P_PROCESS_ID = "";
			string P_MODULE_ID = "";
			string P_FUNCTION_ID = "";
			string P_PROCESS_STS = "";
			string ORDER = "";
			string DIR = "";

			var listLog = logMonitoringRepo.GetDataLog(MODE, P_START, P_LENGTH, P_PROCESS_DT, P_END_DT, P_USER_ID, P_PROCESS_ID, P_MODULE_ID, P_FUNCTION_ID, P_PROCESS_STS, ORDER, DIR);
			var result = JsonConvert.SerializeObject(listLog);

			Assert.Pass(result);
		}

		[Test, Order(3)]
		public void GetDataLogDetail()
		{
			string MODE = "row";
			int P_START = 0;
			int P_LENGTH = 10;
			string P_PROCESS_ID = "";
			string P_MESSAGE_TYPE = "";
			string ORDER = "";
			string DIR = "";

			var listLog = logMonitoringRepo.GetDataLogDetail(MODE, P_START, P_LENGTH, P_PROCESS_ID, P_MESSAGE_TYPE, ORDER, DIR);
			var result = JsonConvert.SerializeObject(listLog);

			Assert.Pass(result);
		}
	}
}