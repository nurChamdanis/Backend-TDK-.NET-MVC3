using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyota.Common.Web.Platform;
using System.Web.Mvc;
using ASPNETMVC3TDK.Models;

/*New Reference for Generate JWT*/
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


namespace ASPNETMVC3TDK.Controllers
{
    public class LoginController : LoginPageController
    {
        string AppAlias = ApplicationSettings.Instance.Alias;
        //Repository Login
        Models.Login.LoginRepository repoLogin = new Models.Login.LoginRepository();

        protected override void Startup()
        {
            Settings.Title = "Login";
            ViewBag.Message = Session["msg"];
            Session["msg"] = "";
            Session["alias"] = AppAlias;
            Console.WriteLine(Session);


            var username = Lookup.Get<Toyota.Common.Credential.User>();
            if (username == null)
            {
                Session.Remove("isLogin");
            }
        }

        public ActionResult checkCompany()
        {
            //getUserRole();
            //string company = Lookup.Get<Toyota.Common.Credential.User>().Company.Id.ToString();
            string username = Lookup.Get<Toyota.Common.Credential.User>().Username.ToString();
            string role = Lookup.Get<Toyota.Common.Credential.User>().Roles.Where(w => w.Application.Id == AppAlias).Select(x => x.Id).FirstOrDefault();
            int res = repoLogin.checkCompany(username);
            if (res == 0)
            {
                Session["msg"] = "User Not Register Please Contact Administartor";
            }
             
            return Json(new { data = res });
        }


        public ActionResult updateLoginFlag()
        {
            string username = Lookup.Get<Toyota.Common.Credential.User>().Username.ToString();
            string fullName = Lookup.Get<Toyota.Common.Credential.User>().Name.ToString();
            string regno = Lookup.Get<Toyota.Common.Credential.User>().RegistrationNumber.ToString();           
            string role = Lookup.Get<Toyota.Common.Credential.User>().Roles.Where(w => w.Application.Id == AppAlias).Select(x => x.Id).FirstOrDefault();

            bool res = false;
            if (!string.IsNullOrEmpty(username))
            {
                string oldJWT = Convert.ToString(Request.Headers["Authorization"]);
                string token = GenerateJSONWebToken(Request.Form["password"]);
                res = repoLogin.insertUserData(username, fullName, regno, token, role);

            }

            return Json(new { data = res });
        }

        public ActionResult getToken()
        {
            string fullName = Lookup.Get<Toyota.Common.Credential.User>().Name.ToString();
            string regno = Lookup.Get<Toyota.Common.Credential.User>().RegistrationNumber.ToString();
            string username = Lookup.Get<Toyota.Common.Credential.User>().Username.ToString();
            string role = Lookup.Get<Toyota.Common.Credential.User>().Roles.Where(w => w.Application.Id == AppAlias).Select(x => x.Id).FirstOrDefault();
            string token = GenerateJSONWebToken(Request.Form["password"]);
            //string token = repoLogin.getToken(username, fullName, regno, newToken);

            return Json(new { token });
        }

        //public IConfiguration _config;
        public string status;
        public string messages;
        public string token;

        ResultMessage message = new ResultMessage();
       

        private string GenerateJSONWebToken(string password)
        {
            string username = Lookup.Get<Toyota.Common.Credential.User>().Username.ToString();
            string fullName = Lookup.Get<Toyota.Common.Credential.User>().Name.ToString();
            var AppId = Lookup.Get<Toyota.Common.Credential.User>().Applications;
            string role = Lookup.Get<Toyota.Common.Credential.User>().Roles.Where(w => w.Application.Id == AppAlias).Select(x => x.Id).FirstOrDefault();
            string regno = Lookup.Get<Toyota.Common.Credential.User>().RegistrationNumber.ToString();

            var url = Url.Content("~");

            string secret_key = username + password + regno;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("username", username));
            permClaims.Add(new Claim("firstname", fullName));
            permClaims.Add(new Claim("role", role));
            permClaims.Add(new Claim("regno", regno));

            var token = new JwtSecurityToken(url, //Issure    
                    url,  //Audience    
                    permClaims,
                    expires: DateTime.Now.AddDays(365),
                    signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            Session["isLogin"] = "Login";

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
