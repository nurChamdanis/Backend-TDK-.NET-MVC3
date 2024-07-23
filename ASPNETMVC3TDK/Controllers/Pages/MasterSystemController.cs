using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Controllers
{
    public class MasterSystemController : PageController
    {
        protected override void Startup()
        {
            User user = Lookup.Get<User>();
            ViewBag.Name = user.Name;
            ViewBag.CompanyId = user.Company.Id;
            ViewBag.CompanyName = user.Company.Name;
            Settings.Title = ApplicationSettings.Instance.Name;
        }
    }
}
