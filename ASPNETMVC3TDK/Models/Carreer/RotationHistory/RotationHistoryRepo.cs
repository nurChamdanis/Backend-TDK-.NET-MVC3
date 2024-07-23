using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.RotationHistory
{
    public class RotationHistoryRepo
    {
        #region Singleton

        private static RotationHistoryRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static RotationHistoryRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RotationHistoryRepo();
                }
                return instance;
            }

        }
        #endregion

        public IList<M_RotationHistory> getRotationHistory
                (string P_NOREG, string P_ORDER_TYPE, string P_STATUS, bool MODEMASTER, M_RotationHistory m)
        {
            IList<M_RotationHistory> result;
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS,
            };
            if (MODEMASTER)
            {
                result = db.Fetch<M_RotationHistory>("CarreerHistory/RotationHistory/getMasterRotation", args);
            }
            else
            {
                result = db.Fetch<M_RotationHistory>("CarreerHistory/RotationHistory/getRotationHistory", args);
            }
            db.GetSqlLoaders();
            db.Close();
            return result;
        }

        //

   


        public ResultMessage UpdateData(M_RotationHistory m)
        {
            dynamic args = new
            {
                P_NOREG = m.NOREG,
                P_DIVISION_ID = m.DIVISION_ID,
                P_TRANSACTION_ID = m.TRANSACTION_ID,
                P_RESPONSIBILITY = m.RESPONSIBILITY,
                P_ACCOMPLISHMENT = m.ACCOMPLISHMENT,
                P_VALID_FROM = m.VALID_FROM,
                P_VALID_TO = m.VALID_TO,
                P_DEPARTMENT_NAME = m.DEPARTMENT_NAME,
                P_SECTION_NAME = m.SECTION_NAME,
                P_DIVISION_NAME = m.DIVISION_NAME,
                P_LINE_ID = m.LINE_ID,
                P_LINE_NAME = m.LINE_NAME,
                P_GROUP_ID = m.GROUP_ID,
                P_GROUP_NAME = m.GROUP_NAME,
                P_CLASS = m.CLASS,
                P_POSITION_LEVEL = m.POSITION_LEVEL,
                P_POSITION_ABBR = m.POSITION_ABBR,
                P_POSITION_DESC = m.POSITION_DESC,
                P_POSITION_PERCENT = m.POSITION_PERCENT,
                P_DIRECTORATE_ID = m.DIRECTORATE_ID,
                P_DIRECTORATE_NAME = m.DIRECTORATE_NAME,
                P_DEPARTMENT_ID = m.DEPARTMENT_ID,
                P_STATUS_CD = m.STATUS_CD,
                P_REJECT_REASON = m.REJECT_REASON,
                P_SECTION_ID = m.SECTION_ID,
                P_ORG_ID = m.ORG_ID
            };
            ResultMessage Result = db.Fetch<ResultMessage>("CarreerHistory/RotationHistory/getUpdateData", args)[0];
            db.Close();
            return Result;
        }




        public ResultMessage DeleteData(M_RotationHistory m)
        {
            dynamic args = new
            {
                P_TRANSACTION_ID = m.TRANSACTION_ID
            }
            ;
            ResultMessage Result = db.Fetch<ResultMessage>("CarreerHistory/RotationHistory/getDeleteData", args)[0];
            db.Close();
            return Result;
        }


    }
}