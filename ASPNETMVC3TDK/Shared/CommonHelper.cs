using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.DownloadFile;
using System.Collections.Generic;
using System.Linq;

namespace ASPNETMVC3TDK.Shared
{
    public class CommonHelper
    {
        public static string parseEmptyString(string v)
        {
            return (!string.IsNullOrEmpty(v)) ? v : "";
        }

        public static List<ResponseSelect2> getCmbOptions(string TYPE, string CODE = "")
        {
            DownloadFileRepo repo = new DownloadFileRepo();
            List<ResponseSelect2> result = repo.GetDataOptions(TYPE, CODE).ToList();

            return result;
        }
    }
}