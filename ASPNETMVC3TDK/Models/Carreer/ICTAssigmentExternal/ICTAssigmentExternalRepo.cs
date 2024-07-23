using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database; 

namespace ASPNETMVC3TDK.Models.ICTAssigmentExternal
{
    public class ICTAssigmentExternalRepo
    {
        #region Singleton

        private static ICTAssigmentExternalRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static ICTAssigmentExternalRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ICTAssigmentExternalRepo();
                }
                return instance;
            } 
        }
        #endregion 

        public IList<M_ICTAssigmentExternal> getICTAssigmentExternal
        (string P_NOREG, string P_ORDER_TYPE, string P_STATUS, bool MODEMASTER, M_ICTAssigmentExternal m)
        {
            IList<M_ICTAssigmentExternal> result = null;
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS,
            };
            if (MODEMASTER)
            {
                result = db.Fetch<M_ICTAssigmentExternal>("CarreerHistory/ICTAssigmentExternal/getMasterICTAssigment", args);
            }
            else
            {
                result = db.Fetch<M_ICTAssigmentExternal>("CarreerHistory/ICTAssigmentExternal/getICTAssigmentExternal", args);
            }
            db.GetSqlLoaders();
            db.Close();
            return result;
        }


        public ResultMessage UpdateData(M_ICTAssigmentExternal m)
        {
            dynamic args = new
            {
                P_TRANSACTION_ID = m.TRANSACTION_ID,
                P_NOREG = m.NOREG,
                P_HOST_COMPANY = m.HOST_COMPANY,
                P_HOST_COUNTRY = m.HOST_COUNTRY,
                P_PERIODE_FROM = m.PERIODE_FROM,
                P_PERIODE_TO = m.PERIODE_TO,
                P_HOST_DIVISION = m.HOST_DIVISION,
                P_HOST_DEPARTMENT = m.HOST_DEPARTMENT,
                P_HOST_SECTION = m.HOST_SECTION,
                P_RESPONSIBILITY = m.RESPONSIBILITY,
                P_ACCOMPLISHMENT = m.ACCOMPLISHMENT,
                P_HOST_CLASS = m.HOST_CLASS,
                P_HOST_POSITION = m.HOST_POSITION,
                P_REMARK_1 = m.REMARK_1,
                P_REMARK_2 = m.REMARK_2,
                P_STATUS_CD = m.STATUS_CD,
                P_REJECT_REASON = m.REJECT_REASON,
            };
            ResultMessage Result = db.Fetch<ResultMessage>("CarreerHistory/ICTAssigmentExternal/getUpdateData", args)[0];
            db.Close();
            return Result;
        }


        public ResultMessage DeleteData(M_ICTAssigmentExternal m)
        {
            dynamic args = new
            {
                P_TRANSACTION_ID = m.TRANSACTION_ID
            }
            ;
            ResultMessage Result = db.Fetch<ResultMessage>("CarreerHistory/ICTAssigmentExternal/getDeleteData", args)[0];
            db.Close();
            return Result;
        }

    }
}