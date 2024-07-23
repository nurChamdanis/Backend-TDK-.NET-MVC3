using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.Education
{
    public class EducationRepo
    {
        #region Singleton

        private static EducationRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();

        public static EducationRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EducationRepo();
                }
                return instance;
            }
        }
        #endregion

        public IList<Education> GetEducationLevel(string P_EDUCATION_DESC)
        {
            dynamic args = new
            {
                P_EDUCATION_DESC,
            };
            IList<Education> Result = db.Fetch<Education>("MasterDataApi/Education/getEducation", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }
    }
}