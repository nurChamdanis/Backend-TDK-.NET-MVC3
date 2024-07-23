using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models.UserProfileDescrip;

namespace ASPNETMVC3TDK.Models.UserProfile
{
    public class UserProfileRepo
    {
        #region Singleton

        private static UserProfileRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static UserProfileRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserProfileRepo();
                }
                return instance;
            }

        }
        #endregion
            
        public IList<UserProfile> getUserProfileInformation
                (string P_NOREG, string P_ORDER_TYPE, string P_STATUS , UserProfile m)
        {
            dynamic args = new
            {
                P_NOREG,   
                P_ORDER_TYPE,
                P_STATUS,
            };
            IList<UserProfile> Result = db.Fetch<UserProfile>("PersonalInformation/UserProfile/getUserProfile", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }
         
        public IList<UserProfile> getLookupUser
        (string P_NOREG)
        {
            dynamic args = new
            {
                P_NOREG 
            };
            IList<UserProfile> Result = db.Fetch<UserProfile>("PersonalInformation/UserProfile/getUserFind", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }


        public ResultMessage UpdateDataDescrip(UserProfile m)
        {
            dynamic args = new
            { 
                P_NOREG = m.NOREG,
                P_DESCRIPTION = m.DESCRIPTION,
            };
            ResultMessage Result = db.Fetch<ResultMessage>("PersonalInformation/UserProfile/UpdateDataDescp", args)[0];
            db.Close();
            return Result;
        }

    }
}