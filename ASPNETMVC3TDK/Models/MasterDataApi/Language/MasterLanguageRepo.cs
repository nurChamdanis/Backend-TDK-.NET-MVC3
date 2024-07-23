using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.MasterLanguage
{
    public class MasterLanguageRepo
    {
        #region Singleton

        private static MasterLanguageRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();

        public static MasterLanguageRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MasterLanguageRepo();
                }
                return instance;
            }
        }
        #endregion

        public IList<Language> GetLanguage (string P_LANGUAGE_ID)
        {
            dynamic args = new { P_LANGUAGE_ID };
            IList<Language> Result = db.Fetch<Language>("MasterDataApi/Language/getLanguage", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }

        public IList<LanguageSkill> GetLanguageSkill(string P_SKILL_LEVEL)
        {
            dynamic args = new { P_SKILL_LEVEL };
            IList<LanguageSkill> Result = db.Fetch<LanguageSkill>("MasterDataApi/LanguageSkill/getLanguageSkill", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }
    }
}