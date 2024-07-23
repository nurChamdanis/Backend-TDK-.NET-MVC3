using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Controllers
{
    public class MasterFeatureController : PageController
    {
        protected override void Startup()
        {
            User user = Lookup.Get<User>();
            Settings.Title = ApplicationSettings.Instance.Name;
        }
    }
}
