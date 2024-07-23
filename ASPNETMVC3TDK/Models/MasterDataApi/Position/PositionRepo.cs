using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using ASPNETMVC3TDK.Shared;

namespace ASPNETMVC3TDK.Models.Position
{
    public class PositionRepo
    {

        #region Singleton

        private static PositionRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static PositionRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PositionRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<Position> getPosition(string P_NOREG, string P_ORDER_TYPE, string P_STATUS, Position m)
        {
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS,
            };
            IList<Position> Result = db.Fetch<Position>("MasterDataApi/Position/getPosition", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }

        public IList<PositionMaster> getMasterPosition(PositionMaster m)
        {
            dynamic args = new
            {
                P_POSITION_LEVEL =  m.POSITION_LEVEL,
                P_POSITION_ABBR = m.POSITION_ABBR,
                P_POSITION_DESC = m.POSITION_DESC,
            };

            IList<PositionMaster> Result = db.Fetch<PositionMaster>("MasterDataApi/Position/getMasterPosition", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }
    }
}