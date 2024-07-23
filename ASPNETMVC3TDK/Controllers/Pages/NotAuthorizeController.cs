using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

using ASPNETMVC3TDK;

namespace ASPNETMVC3TDK.Controllers
{
    public class NotAuthorizeController : PageController
    {
        protected override void Startup()
        {
            User user = Lookup.Get<User>();
/*            ViewBag.Name = user.Name;*/
            Settings.Title = ApplicationSettings.Instance.Name;
        }
    }
}
