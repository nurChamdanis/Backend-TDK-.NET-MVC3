using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Controllers
{
    public class HomeController : PageController
    {
        protected override void Startup()
        {
            User user = Lookup.Get<User>();
            ViewBag.Name = user.Name;
            Settings.Title = ApplicationSettings.Instance.Name;
        }
    }

}

