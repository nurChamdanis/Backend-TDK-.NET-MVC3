using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;

namespace ASPNETMVC3TDK.Models.Submission
{
    public class SubmissionRepo
    {

        #region Singleton
        private static SubmissionRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();

        public static SubmissionRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SubmissionRepo();
                }
                return instance;
            }
        }
        #endregion


        public IList<Submission> GetSubmissions(string NOREG, string SEARCH, string DIVISION, string DEPARTMENT, string SECTION, 
            string SORT, int? OFFSET, int? FETCH, string ALL)
        {
            dynamic args = new
            {
                P_NOREG = NOREG,
                P_SEARCH = SEARCH,
                P_DIVISION = DIVISION,
                P_DEPARTMENT = DEPARTMENT,
                P_SECTION = SECTION,
                P_SORT = SORT,
                P_OFFSET = OFFSET * FETCH,
                P_FETCH = FETCH,
                P_ALL = ALL,
            };

            IList<Submission> Result = db.Fetch<Submission>("Submission/Submission_GetSubmission", args);
            db.GetSqlLoaders();
            db.Close();

            return Result;
        }


        public int GetCount(string NOREG, string SEARCH, string DIVISION, string DEPARTMENT, string SECTION, string ALL)
        {
            dynamic args = new
            {
                P_NOREG = NOREG,
                P_SEARCH = SEARCH,
                P_DIVISION = DIVISION,
                P_DEPARTMENT = DEPARTMENT,
                P_SECTION = SECTION,
                P_ALL = ALL,
            };

            int Result = db.Fetch<int>("Submission/Submission_Count", args)[0];
            db.GetSqlLoaders();
            db.Close();

            return Result;
        }


    }
}