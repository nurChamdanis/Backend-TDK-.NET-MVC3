using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;
using System.Linq;

namespace ASPNETMVC3TDK.Models.OrgHierarchy
{
    public class OrgHierarchyRepo
    {
        #region Singleton

        private static OrgHierarchyRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();

        public static OrgHierarchyRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrgHierarchyRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<OrgHierarchy> getMasterOrgHierarchy(OrgHierarchy m)
        {
            dynamic title = null;
            dynamic args = new
            {
                P_ORG_ID = m.ORG_ID,
                P_ORG_PARENT = m.ORG_PARENT,
                P_ORG_TITLE = m.ORG_TITLE,
                P_LEVEL_ID = m.LEVEL_ID,
            };

            IList<OrgHierarchy> Result = db.Fetch<OrgHierarchy>("MasterDataApi/OrgHierarchy/getMasterOrgHierarchy", args); 
            db.GetSqlLoaders();
            db.Close();

            return Result;
        }

        public IList<OrgHierarchyTitle> getOrgHirarchyTitle(OrgHierarchy m)
        {
            IList<OrgHierarchyTitle> result = null;
            try
            {
                dynamic args = new
                {
                    P_ORG_ID = m.ORG_ID,
                    P_ORG_PARENT = m.ORG_PARENT,
                    P_ORG_TITLE = m.ORG_TITLE,
                    P_LEVEL_ID = m.LEVEL_ID,
                };
                result = db.Fetch<OrgHierarchyTitle>("MasterDataApi/OrgHierarchy/getMasterOrgHierarchyTitle", args); // for getting data  

                db.GetSqlLoaders();
                db.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return result;
        }

    }
}