using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.Country
{
    public class CountryRepo
    {
        #region Singleton

        private static CountryRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();

        public static CountryRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CountryRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<Country> getCountry (string COUNTRY_CD)
        { 
            IList<Country> Result = db.Fetch<Country>("MasterDataApi/Country/getCountry");
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }

    }
}