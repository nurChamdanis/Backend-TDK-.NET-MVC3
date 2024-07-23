namespace ASPNETMVC3TDK.Models.MasterSystem
{
    public class MasterSystem
	{
		public string MODE { get; set; }
		public string SYSTEM_TYPE { get; set; }
		public string SYSTEM_CD { get; set; }
        public string SYSTEM_VALUE_TXT { get; set; }
		public string SYSTEM_VALUE_NUM { get; set; }
		public string SYSTEM_VALUE_DT { get; set; }
		public string SYSTEM_REMARK { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_DT { get; set; }
		public string CHANGED_BY { get; set; }
		public string CHANGED_DT { get; set; } = "";
		public string CNT { get; set; }
		public string JSON_DATA { get; set; } = "";
	}
}