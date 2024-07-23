using System.Collections.Generic;
using System.Linq;
using Toyota.Common.Database;
using Toyota.Common.Web.Platform;

namespace ASPNETMVC3TDK.Models
{
    public class ResultMessage
    {
        public string result { get; set; }
        public string status { get; set; }
        public string msgInfo { get; set; }
        public string msg { get; set; }
        public string extraAttribute { get; set; }
        public string lastIdentity { get; set; }
        public string sql { get; set; }
        public object data { get; set; }
        public string process_id { get; set; }
        public string success { get; set; }

        public List<ResultMessage> GetMessage(string MSG_ID, string PARAM_1, string PARAM_2, string PARAM_3, string PARAM_4)
        {
            IDBContext db = DatabaseManager.Instance.GetContext();
            var res = db.Query<ResultMessage>("ResultMessage_GetMessage",
                new
                {
                    MSG_ID = MSG_ID,
                    PARAM_1 = PARAM_1,
                    PARAM_2 = PARAM_2,
                    PARAM_3 = PARAM_3,
                    PARAM_4 = PARAM_4
                }
            );
            db.Close();
            return res.ToList();
        }
    }
}