// for using jwt method
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin; 
using Owin;
using Microsoft.Owin.Cors; 

using System; 
using System.Configuration;
using System.Collections.Generic; 

using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Hangfire.Storage; 


[assembly: OwinStartup(typeof(ASPNETMVC3TDK.Startup))]
namespace ASPNETMVC3TDK
{

    public class Startup
    { 

        public void Configuration(IAppBuilder app )
        {
            var issuer = "yourIssuer";
            var audience = "yourAudience";
            var secretKey = "yourSecretKey";

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

            };



            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true, // Do not use in production
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new ApplicationOAuthProvider()
            };

            app.UseCors(CorsOptions.AllowAll); 

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // untuk menghubungkan ke DB
            app.UseHangfireAspNet(GetHangfireServers);

            // untuk Mengakses hangfire dashboard /hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },
                DashboardTitle = "Dashboard Batch",
                DisplayStorageConnectionString = false,
                AppPath = null,
                StatsPollingInterval = 2000,
            });

            var hangfire = new BackgroundJobClient();

            BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire! ASDASD"));

            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            /*List<ResponseSelect2> m_cron_schedule = CommonHelper.getCmbOptions("CRON_SCHEDULE");
                       foreach (var item in m_cron_schedule.Select((value, j) => new { j, value }))
                       {
                           var value = item.value;
                           var index = item.j;

                           string[] schedule = value.text.Split(';');
                           for (int i = 0; i < schedule.Length; i++)
                           {
                               var shift_type = "";
                               if (schedule.Length > 1)
                               {
                                   shift_type = (i == 0) ? "_NIGHT" : "_DAY";
                               }
                               if (value.id == "UPDATE_MasterSystem" && value.attr_1 == "1")
                               {
                                   RecurringJob.AddOrUpdate(value.id + shift_type, () => HangfireShared.UPDATE_MasterSystem(), schedule[i], TimeZoneInfo.Local);
                               }
                           }

                       }*/
        }

        private IEnumerable<IDisposable> GetHangfireServers()
        {

            // Setting / Config Hangfire
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(ConfigurationManager.AppSettings["ConnectionString"], new SqlServerStorageOptions // Settingan TMMIN & Local
                //.UseSqlServerStorage(ConfigurationManager.AppSettings["ConnectionStringHangfire"], new SqlServerStorageOptions // Kalo Deploy Pake Yang Ini
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });
            // Untuk Memulaikan tugas / Job nya
            yield return new BackgroundJobServer();
        }
    }

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            bool ret = true;
            try
            {
                var httpContext = System.Web.HttpContext.Current;

                if (httpContext != null && httpContext.Session != null)
                {
                    var ctx = httpContext.Session["isLogin"] ?? null;
                    if (ctx == null)
                    {
                        ret = false;
                    }
                }
                else
                {
                    ret = false; // HttpContext.Current or HttpContext.Current.Session is null, handle accordingly
                }
            }
            catch (Exception e)
            {
                ret = false;
                // handle the exception, log, or throw it if necessary
            }

            return ret;
        }
    }




}
