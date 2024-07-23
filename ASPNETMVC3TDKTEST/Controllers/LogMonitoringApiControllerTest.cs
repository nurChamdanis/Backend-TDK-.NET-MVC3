using ASPNETMVC3TDK.Controllers;
using ASPNETMVC3TDK.Models.LogMonitoring;
using Newtonsoft.Json;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace ControllerTest
{
	public class LogMonitoringApiControllerTest
	{
		private LogMonitoringApiController logMonitoringApiController;

		[SetUp]
		public void Setup()
		{
			logMonitoringApiController = LogMonitoringApiController.Instance;
		}

		[Test]
		public void GetDataOptions()
		{
			string PARAM_1 = "MODULE_ID";
			string PARAM_2 = "";
			string PARAM_3 = "";

			var result = logMonitoringApiController.GetDataOptions(PARAM_1, PARAM_2, PARAM_3);
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void getDatatables()
		{
			var result = logMonitoringApiController.GetDatatables();
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void getLogDetail()
		{
			var result = logMonitoringApiController.GetLogDetail();
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void downloadData()
		{
			var result = logMonitoringApiController.DownloadData();
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}

		[Test]
		public void downloadDataDetail()
		{
			var result = logMonitoringApiController.DownloadDataDetail();
			var finalResult = JsonConvert.SerializeObject(result);

			Assert.Pass(finalResult);
		}
	}
}
