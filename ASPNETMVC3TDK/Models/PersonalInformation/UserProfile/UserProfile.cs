namespace ASPNETMVC3TDK.Models.UserProfile
{
    public class UserProfile
    {
        public string NOREG { get; set; }
        public string PERSONNEL_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; } = null;
        public string AGE { get; set; } = null;
        public string CLASS { get; set; } = null;
        public string POSITION { get; set; } = null;
        public string MAIL { get; set; }
        public string GENDER_DESC { get; set; }
        public string DESCRIPTION { get; set; }
        public string ENTRY_DATE { get; set; } = null;
        public string TELEPHONE { get; set; } = null;
        public string DIVISION { get; set; } = null;
        public string SUPERIOR { get; set; } = null;
        public string JOIN_BY { get; set; } = null;
        public string MODE { get; set; } = null;
    }
}