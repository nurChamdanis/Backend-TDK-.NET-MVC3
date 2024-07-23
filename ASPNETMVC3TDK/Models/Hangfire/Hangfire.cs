using System;

namespace ASPNETMVC3TDK.Models.Hangfire
{
	public class Hangfire_Export_MasterSystem
	{
		public string SYSTEM_CD { get; set; }
		public string SYSTEM_TYPE { get; set; }
		public string SYSTEM_VALUE_TXT { get; set; }
		public string SYSTEM_VALUE_DT { get; set; }
		public string SYSTEM_VALUE_NUM { get; set; }
		public string SYSTEM_REMARK { get; set; }
		public string IS_ACTIVE { get; set; }
		public string CREATED_BY { get; set;}
		public DateTime CREATED_DT { get; set; }
		public string CHANGED_BY { get; set; }
		public DateTime? CHANGED_DT { get; set; }
	}
}