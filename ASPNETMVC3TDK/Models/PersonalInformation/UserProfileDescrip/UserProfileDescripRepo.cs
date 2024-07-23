using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared; 

namespace ASPNETMVC3TDK.Models.UserProfileDescrip
{
    public class UserProfileDescripRepo
    {
        #region Singleton

        private static UserProfileDescripRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static UserProfileDescripRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserProfileDescripRepo();
                }
                return instance;
            }

        }
        #endregion
             

        public IList<UserProfileDescrip> GetUserProfileDesc
        (string P_NOREG, UserProfileDescrip m)
        {
            dynamic args = new
            {
                P_NOREG
            };
            IList<UserProfileDescrip> Result = db.Fetch<UserProfileDescrip>("PersonalInformation/UserProfile/getUserDescrip", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }


    }
}