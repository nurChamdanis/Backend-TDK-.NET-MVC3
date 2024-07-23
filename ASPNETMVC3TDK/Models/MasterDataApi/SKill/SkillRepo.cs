using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.Skill
{
    public class SkillRepo
    {

        #region Singleton

        private static SkillRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static SkillRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SkillRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<Skill> getSkill(Skill m)
        {
            dynamic args = new
            {
                P_FIND = m.SKILL_NAME, 
            };
            IList<Skill> Result = db.Fetch<Skill>("MasterDataApi/Skill/getSkill", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }


    }
}