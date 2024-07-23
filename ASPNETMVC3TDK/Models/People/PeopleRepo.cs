using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using PetaPoco;

namespace ASPNETMVC3TDK.Models.People
{
    public class PeopleRepo
    {

        #region Singleton

        private static PeopleRepo instance = null;
        static IDBContext db = DatabaseManager.Instance.GetContext();

        public static PeopleRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PeopleRepo();
                }
                return instance;
            }
        }
        #endregion


        public static IList<People> GetPeople(string NOREG, string SUPER, string DIVISION, string DEPARTEMENT, string SECTION, string LINE,
            string GROUP, string SEARCH, string CLASS, string POSITION, string RECENT, string SKILLS, int? OFFSET, int? FETCH, string ALL)
        {

            dynamic args = new
            {
                P_NOREG = NOREG,
                P_DIVISION = DIVISION,
                P_DEPARTEMENT = DEPARTEMENT,
                P_SECTION = SECTION,
                P_LINE = LINE,
                P_GROUP = GROUP,
                P_SEARCH = SEARCH,
                P_CLASS = CLASS,
                P_POSITION = POSITION,
                P_RECENT = RECENT,
                P_SKILLS = SKILLS,
                P_OFFSET = (OFFSET * FETCH),
                P_FETCH = FETCH,
                P_ALL = ALL,
                P_SUPER = SUPER,
            };
            IList<People> Result;
            if (ALL == "true")
            {
                Result = db.Fetch<People>("People/People_GetPeople", args);
            }
            else
            {
                //OFFSET = OFFSET * FETCH;
                //Result = db.Fetch<People>("EXEC DBO.sp_getPeopleBySuper '" + NOREG + "', '" + SUPER + "', '" + RECENT + "', '" + DIVISION + "', '" + DEPARTEMENT + "', '" + SECTION + "', '" + LINE + "', '" + GROUP + "', '" + CLASS + "', '" + POSITION + "', '" + SEARCH + "', '" + SKILLS + "', '" + ALL + "'," + OFFSET + "," + FETCH + ";");
                Result = db.Fetch<People>("EXEC PDI.sp_getPeopleBySuper @NOREG, @SUPER, @P_RECENT, @P_DIVISION, @P_DEPARTEMENT, @P_SECTION, @P_LINE, @P_GROUP, @P_CLASS, @P_POSITION, @P_SEARCH, @P_SKILLS, @P_ALL, @P_OFFSET, @P_FETCH;",
                new
                    {
                        NOREG = NOREG,
                        SUPER = SUPER,
                        P_RECENT = RECENT,
                        P_DIVISION = DIVISION,
                        P_DEPARTEMENT = DEPARTEMENT,
                        P_SECTION = SECTION,
                        P_LINE = LINE,
                        P_GROUP = GROUP,
                        P_CLASS = CLASS,
                        P_POSITION = POSITION,
                        P_SEARCH = SEARCH,
                        P_SKILLS = SKILLS,
                        P_ALL = ALL,
                        P_OFFSET = OFFSET * FETCH,
                        P_FETCH = FETCH
                    });

            }
            db.GetSqlLoaders();
            db.Close();

            return Result;
        }


    }
}