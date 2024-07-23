namespace ProjectStarter.Models.Common
{
    public class MaterialLookupModel
    {
        public string MAT_NO { get; set; }
        public int SelectionType { get; set; }

        public string IdTextReturn { get; set; }

        public MaterialLookupModel(string matNo, int type, string text)
        {
            MAT_NO = matNo;
            SelectionType = type;
            IdTextReturn = text;
        }

        public MaterialLookupModel()
        {

        }
    }
}