using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.Class
{
    public class ClassRepo
    {

        #region Singleton

        private static ClassRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static ClassRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClassRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<Class> getMasterClass(Class m)
        {
            dynamic args = new
            {
                P_CLASS_CD = m.CLASS_CD,
                P_CLASS = m.CLASS, 
            };

            IList<Class> Result = db.Fetch<Class>("MasterDataApi/Class/getMasterClass", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }


    }
}