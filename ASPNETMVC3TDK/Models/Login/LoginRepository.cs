using System;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Models.Login
{
    public class LoginRepository
    {
        public static ResultMessage response = new ResultMessage();
        public int checkCompany(string username)
        {
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                dynamic args = new
                {
                    pUser = username,
                };

                var result = db.SingleOrDefault<int>("Login/checkCompany", args);
                db.Close();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public bool insertUserData(string username, string fullname, string regno, string token, string role)
        {
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();
                dynamic args = new
                {
                    pUser = username,
                    pFullname = fullname,
                    pRegno = regno,
                    pToken = token,
                    pRole = role
                };

                var result = db.SingleOrDefault<int>("Login/insertUserData", args);
                db.Close();
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string getToken(string username, string fullname, string regno, string token)
        {
            try
            {
                IDBContext db = DatabaseManager.Instance.GetContext();

                var res = db.SingleOrDefault<string>("Login/getToken", new { username, fullname, regno, token });
                db.Close();
                return res;
            } catch (Exception e)
            {
                return "";
            }
        }


    }
}