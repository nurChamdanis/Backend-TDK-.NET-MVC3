namespace ASPNETMVC3TDK.Models.LogMonitoring
{

    public class LogH
    {
        public string r_n_n { get;set; }
        public string PROCESS_ID { get; set; }
        public string PROCESS_DT { get; set; }
        public string MODULE_NAME { get; set; }
        public string FUNCTION_NAME { get; set; }
		public string PROCESS_STS { get; set; }
		public string PROCESS_STS_ID { get; set; }
        public string USER_ID { get; set; }
        public string END_DT { get; set; }
        public string REMARKS { get; set; }
        public string CREATED_BY { get; set; }
        public string CREATED_DT { get; set; }
        public string CHANGED_BY { get; set; }
        public string CHANGED_DT { get; set; }
        public string CNT { get; set; }
    }


    public class LogD
    {
        public string PROCESS_ID { get; set; }
        public string SEQ_NO { get; set; }
        public string ERR_DT { get; set; }
        public string ERR_LOCATION { get; set; }
		public string MSG_TYPE { get; set; }
		public string MSG_ID { get; set; }
		public string ERR_MESSAGE { get; set; }
		public string CNT { get; set; }
	}


}