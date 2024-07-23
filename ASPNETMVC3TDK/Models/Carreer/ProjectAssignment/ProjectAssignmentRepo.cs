using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.ProjectAssignment
{
    public class ProjectAssignmentRepo
    {
        #region Singleton

        private static ProjectAssignmentRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static ProjectAssignmentRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectAssignmentRepo();
                }
                return instance;
            }

        }
        #endregion

        /**/
        public IList<M_ProjectAssignment> getProjectAssignment(string P_NOREG, string P_ORDER_TYPE, string P_STATUS, bool MODEMASTER, M_ProjectAssignment m)
        {
            IList<M_ProjectAssignment> result;
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS,
            };
            if (MODEMASTER)
            {
                result = db.Fetch<M_ProjectAssignment>("CarreerHistory/ProjectAssigment/getMasterProjectAssigment", args);
            }
            else
            {
                result = db.Fetch<M_ProjectAssignment>("CarreerHistory/ProjectAssigment/ProjectAsistantManager", args);
            }
            db.GetSqlLoaders();
            db.Close();
            return result;
        }

        

        public ResultMessage UpdateData(M_ProjectAssignment m)
        {
            dynamic args = new
            {
                P_TRANSACTION_ID = m.TRANSACTION_ID ,
                P_NOREG = m.NOREG ,
                P_CLASS = m.CLASS ,
                P_PROJECT_NAME = m.PROJECT_NAME ,
                P_POSITION_ABBR = m.POSITION_ABBR , 
                P_DIVISION_ID = m.DIVISION_ID ,
                P_DIVISION_NAME = m.DIVISION_NAME ,
                P_DEPARTMENT_ID = m.DEPARTMENT_ID ,
                P_DEPARTMENT_NAME = m.DEPARTMENT_NAME ,
                P_SECTION_ID = m.SECTION_ID ,
                P_SECTION_NAME = m.SECTION_NAME ,
                P_RESPONSIBILITY = m.RESPONSIBILITY ,
                P_ACCOMPLISHMENT = m.ACCOMPLISHMENT ,
                P_VALID_FROM = m.VALID_FROM ,
                P_VALID_TO = m.VALID_TO ,
                P_STATUS_CD = m.STATUS_CD , 
            };
            ResultMessage Result = db.Fetch<ResultMessage>("CarreerHistory/ProjectAssigment/getUpdateData", args)[0];
            db.Close();
            return Result;
        }


        public ResultMessage DeleteData(M_ProjectAssignment m)
        {
            dynamic args = new
            {
                P_TRANSACTION_ID = m.TRANSACTION_ID
            }
            ;
            ResultMessage Result = db.Fetch<ResultMessage>("CarreerHistory/ProjectAssigment/getDeleteData", args)[0];
            db.Close();
            return Result;
        }













    }
     
}
