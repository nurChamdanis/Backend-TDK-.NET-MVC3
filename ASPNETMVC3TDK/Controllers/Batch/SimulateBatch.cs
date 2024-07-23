using ASPNETMVC3TDK.Shared;
using System.Threading;
namespace ASPNETMVC3TDK.Controllers
{
    public class SimulateBatch
    {
        public string Update_MasterSystem()
        {
            HangfireShared.UPDATE_MasterSystem();
            return "success";
        }

        public static void aThousandLoop()
        {
            int i = 0;
            while (i < 1000)
            {
                Thread.Sleep(1000);
                i++;
            }
        }
    }
}
