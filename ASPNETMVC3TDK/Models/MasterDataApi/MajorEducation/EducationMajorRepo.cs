using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.EducationMajor
{
    public class EducationMajorRepo
    {
        #region Singleton

        private static EducationMajorRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();

        public static EducationMajorRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EducationMajorRepo();
                }
                return instance;
            }
        }
        #endregion

        public IList<EducationMajor> getEducationMajor(string P_EDUCATION_CD)
        { 
            dynamic args = new
            {
                P_EDUCATION_CD, 
            };
            IList<EducationMajor> Result = db.Fetch<EducationMajor>("MasterDataApi/EducationMajor/getEducationMajor", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }
    }
}