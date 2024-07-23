using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.Approval
{
    public class ApprovalRepo
    {

        #region Singleton

        private static ApprovalRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static ApprovalRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApprovalRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<Approval> getAllAprove(string P_NOREG, string P_ORDER_TYPE, string P_STATUS, Approval m)
        {
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS,
            };
            IList<Approval> Result = db.Fetch<Approval>("Approval/getApproval", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }
        

    }
}