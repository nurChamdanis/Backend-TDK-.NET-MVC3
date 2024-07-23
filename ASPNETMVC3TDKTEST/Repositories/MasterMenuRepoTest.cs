using ASPNETMVC3TDK.Models.MasterFunction;
using Newtonsoft.Json;

namespace ASPNETMVC3TDKTEST
{
	public class MasterMenuRepoTest
	{
		private MasterMenuRepo masterMenuRepo;

		[SetUp]
		public void Setup()
		{
			masterMenuRepo = MasterMenuRepo.Instance;
		}

		[Test]
		public void Test1()
		{
			var result = masterMenuRepo.GetData(1, 0, "");

			//Assert.IsNotNull(result);

			var res = JsonConvert.SerializeObject(result);
			Console.WriteLine(res);

			// Output result to console
			foreach (var item in result)
			{
				Console.WriteLine($"Menu ID: {item.MENU_ID}, Menu Text: {item.MENU_TEXT}");
			}
			//Assert.AreEqual(res,"Test");
		}
	}
}

		