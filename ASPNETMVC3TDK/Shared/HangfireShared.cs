using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using ASPNETMVC3TDK.Models.Hangfire;
using System.Drawing;
using ASPNETMVC3TDK.Shared;
using System.Globalization;
using System.Diagnostics;
using ASPNETMVC3TDK.Models.MasterSystem;

namespace ASPNETMVC3TDK.Shared
{
    public class HangfireShared
    {
		public static void UPDATE_MasterSystem()
		{
			HangfireRepo hangfireRep = new HangfireRepo();
			try
			{
				hangfireRep.UpdateMasterSystem();

			}
			catch (Exception e)
			{}

		}

		public static string GetIndonesianDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Minggu";
                case DayOfWeek.Monday:
                    return "Senin";
                case DayOfWeek.Tuesday:
                    return "Selasa";
                case DayOfWeek.Wednesday:
                    return "Rabu";
                case DayOfWeek.Thursday:
                    return "Kamis";
                case DayOfWeek.Friday:
                    return "Jumat";
                case DayOfWeek.Saturday:
                    return "Sabtu";
                default:
                    return "";
            }
        }
    }
}