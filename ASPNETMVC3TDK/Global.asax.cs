using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Toyota.Common.App_Start;
using App_Start;
using Toyota.Common.Web.Platform;
using Toyota.Common.Credential;
using Toyota.Common.Validation;

namespace ASPNETMVC3TDK
{
    public class MvcApplication : WebApplication
    {
        public MvcApplication()
        {
            #region old setting App
            ApplicationSettings.Instance.Name = "Talent Digitalization";
            ApplicationSettings.Instance.Alias = "TD-001";
            ApplicationSettings.Instance.OwnerName = "PT.TMMIN";
            ApplicationSettings.Instance.OwnerAlias = "PT.TMMIN";
            ApplicationSettings.Instance.OwnerEmail = "info@arkamaya.co.id";
            ApplicationSettings.Instance.Security.UnauthorizedController = "NotAuthorize"; //redirect controller if user login not allowed permission (controller must be exists in app)
            ApplicationSettings.Instance.Runtime.HomeController = "NotAuthorize"; //default controller after login (controller must be exists in app)
            ApplicationSettings.Instance.Menu.Enabled = true; // option setting enable/disable all menu
            ApplicationSettings.Instance.Security.EnableAuthentication = true; // option setting authentication app
            ApplicationSettings.Instance.Security.IgnoreAuthorization = true; // option setting ignore or restrict controller
            ApplicationSettings.Instance.Security.EnableSingleSignOn = false; // option setting using SSO service or not
            #endregion

            #region new setting app
            ApplicationSettings.Instance.DefaultDbSc = "SecurityCenter";    // default connfig key for DB SC
            //ApplicationSettings.Instance.Menu.SecurityCenter = false;        // option setting data menu (true=get menu from sc, false =get data menu from xml)
            ApplicationSettings.Instance.Menu.SecurityCenter = true;        // option setting data menu (true=get menu from sc, false =get data menu from xml) // settingan TMMIN
            ApplicationSettings.Instance.Security.EnableTracking = false;    // option setting tracking (DB : SC , Table : TB_T_COUNTER)
            ApplicationSettings.Instance.Security.Encrypt = false;           // Option setting encryption password/ not
            #endregion


            /*BypassLogin(false);*/
            BypassLogin(false);
            ApplicationSettings.Instance.Security.EnableAuthentication = false;
            ApplicationSettings.Instance.Security.IgnoreAuthorization = true;
            //ApplicationSettings.Instance.Security.IgnoreAuthorization = false; // settingan TMMIN

             
        }

        private void BypassLogin(bool isBypass)
        {
            if (isBypass)
            {

                #region simulation user
                ApplicationSettings.Instance.Security.SimulateAuthenticatedSession = false;
                //offline
                ApplicationSettings.Instance.Security.IgnoreAuthorization = isBypass;
                ApplicationSettings.Instance.Security.SimulateAuthenticatedSession = isBypass;
                ApplicationSettings.Instance.Security.SimulatedAuthenticatedUser = new User()
                {
                    Username = "arkamaya.achmad",
                    Password = "@Toyota123",
                    FirstName = "Achmad",
                    LastName = "Arkamaya",
                    RegistrationNumber = "123456789"
                };
                ApplicationSettings.Instance.Security.SimulatedAuthenticatedUser.Company = new Toyota.Common.CompanyInfo()
                {
                    Id = "2000"
                };
                #endregion
            }
        }

        protected override void Startup()
        {
            var localTimeZone = TimeZoneInfo.Local;

            Console.WriteLine($"Time zone display name: {localTimeZone.DisplayName}"); //same as .ToString()
            Console.WriteLine($"Time zone id (Windows): {localTimeZone.Id}");

            ProviderRegistry.Instance.Register<IUserProvider>(typeof(DbUserProvider), DatabaseManager.Instance, ApplicationSettings.Instance.DefaultDbSc);
            ProviderRegistry.Instance.Register<ISingleSignOnProvider>(typeof(SingleSignOnProvider), ProviderRegistry.Instance.Get<IUserProvider>(), DatabaseManager.Instance, "SSO");

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelValidatorProviders.Providers.Add(new CustomModelValidatorProvider());

        }

        protected new void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register the AllowCorsAttribute globally
            GlobalFilters.Filters.Add(new AllowCorsAttribute()); 
            // Your other methods...
        }

        //FID.Ridwan: 20230923 -> handle if double url
        protected void Application_Error(object sender, EventArgs e)
        {
            if (HttpContext.Current.Error != null)
            {
                string currentError = HttpContext.Current.Error.ToString();
                if (currentError.Contains("potentially dangerous"))
                {
                    var contextReqWeb = ((System.Web.HttpApplication)sender).Request;

                    string urlScheme = contextReqWeb.Url.Scheme;
                    string host = HttpContext.Current.Request.Url.Authority;
                    string action = HttpContext.Current.Request.Url.PathAndQuery;
                    action = action.Replace("/" + urlScheme + "://" + host, "").Replace("/" + urlScheme + ":/" + host, "").Replace(urlScheme + "://" + host, "").Replace(urlScheme + ":/" + host, "");

                    var redirectUrl = urlScheme + "://" + host + action; //System.Web.HttpUtility.UrlEncode(action);

                    Response.Redirect(redirectUrl);
                }
                else if (currentError.Contains("was not found or does not implement IController"))
                {
                    Response.Redirect("/NotAuthorize");
                }
                else if (currentError.Contains("A public action method 'Index' was not found on controller"))
                {
                    Response.Redirect("/NotAuthorize");
                }
                else if (currentError.Contains("System.NullReferenceException")) {
                    Response.Redirect("/NotAuthorize");
                }

                //HTTP 404
            }
        }
   
         
    }
}