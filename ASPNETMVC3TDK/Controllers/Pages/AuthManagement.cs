using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Controllers
{
    public class AuthManagementController : PageController
    {
        protected override void Startup()
        {
            User user = Lookup.Get<User>();
            Settings.Title = ApplicationSettings.Instance.Name;
            ViewBag.AppAlias = ApplicationSettings.Instance.Alias;
        }
    }
}
