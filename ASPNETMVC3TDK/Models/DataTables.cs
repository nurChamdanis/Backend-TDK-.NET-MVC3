namespace ASPNETMVC3TDK.Models
{
    public class DataTables
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public object data { get; set; }
        public bool status { get; set; }
        public string msg { get; set; }
        public string msgInfo { get; set; }

    }
}