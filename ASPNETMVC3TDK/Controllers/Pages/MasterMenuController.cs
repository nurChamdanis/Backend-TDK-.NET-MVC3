using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Controllers.Pages
{
    public class MasterMenuController : PageController
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