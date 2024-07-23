using System;
using System.Data;
using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;

namespace ASPNETMVC3TDK.Models.QuestionCategory
{
    public class QuestionCategoryRepo
    {

        #region Singleton
        private static QuestionCategoryRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/
         
        public static QuestionCategoryRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuestionCategoryRepo();
                }
                return instance;
            }
        }
        #endregion

        public SubmissionCheckAttr SubmissionCheck(int? SUBMISSION_ID)
        {
            SubmissionCheckAttr submissionAttr = new SubmissionCheckAttr();

            dynamic args = new
            {
                P_R_SUBMISSION_ID = SUBMISSION_ID
            };

            IList<SubmissionCheckAttr> submissionResult = db.Fetch<SubmissionCheckAttr>("QuestionCategory/QuestionCategory_CheckSubmission", args);

            if (submissionResult.Count > 0) {
                submissionAttr.M_PERIODE_ID = submissionResult[0].M_PERIODE_ID;
                submissionAttr.NOREG = submissionResult[0].NOREG;
            } else
            {
                throw new Exception("Submission Not Found");
            }

            return submissionAttr;
        }

        public PeriodeCheckAttr PeriodeCheck(int? PERIODE_ID, string NOREG)
        {
            PeriodeCheckAttr periodAttr = new PeriodeCheckAttr();

            dynamic args = new
            {
                P_M_PERIODE_ID = PERIODE_ID
            };

            IList<PeriodeCheckAttr> periodResult = db.Fetch<PeriodeCheckAttr>("QuestionCategory/QuestionCategory_CheckPeriod", args);

            if (periodResult.Count > 0) {
                periodAttr.M_PERIODE_ID = periodResult[0].M_PERIODE_ID;
                periodAttr.FOUND_PERIODE = true;
            }
            else {
                if (PERIODE_ID == null) {
                    throw new Exception("There Is No New Periode Yet");
                }

                args = new
                {
                    P_M_PERIODE_ID = PERIODE_ID,
                    P_NOREG = NOREG
                };

                IList<PeriodeCheckAttr> periodExistResult = db.Fetch<PeriodeCheckAttr>("QuestionCategory/QuestionCategory_CheckExistPeriod", args);
                if (periodExistResult.Count > 0)
                {
                    periodAttr.M_PERIODE_ID = periodExistResult[0].M_PERIODE_ID;
                    periodAttr.FOUND_PERIODE = false;
                } else {
                    throw new Exception("Periode Not Found");
                }
            }

            return periodAttr;
        }

        public IList<M_Category> GetQuestionCategory(int? CATEGORY_ID, int? PERIODE_ID, bool? RECENT_DATA)
        {
            dynamic args = new
            {
                P_M_CATEGORY_ID = CATEGORY_ID,
                P_M_PERIODE_ID = PERIODE_ID,
                P_RECENT_DATA = RECENT_DATA
            };

            IList<M_Category>  result = db.Fetch<M_Category>("QuestionCategory/QuestionCategory_GetQuestionCategory", args);

            return result;
        }

        public IList<M_Question> GetQuestionByCategory(string NOREG, int? PERIODE_ID, int? CATEGORY_ID, int? QUESTION_ID, bool? RECENT_DATA)
        {
            dynamic args = new
            {
                P_M_CATEGORY_ID = CATEGORY_ID,
                P_M_QUESTION_ID = QUESTION_ID,
                P_M_PERIODE_ID = PERIODE_ID,
                P_RECENT_DATA = RECENT_DATA
            };

            IList<M_Question> result = db.Fetch<M_Question>("QuestionCategory/QuestionCategory_GetQuestionbyCategory", args);

            foreach (M_Question item in result)
            {
                dynamic VALUE = GetAnswerQuestion(NOREG, PERIODE_ID, item.M_QUESTION_ID);
                if (VALUE?.GetType()?.Name == "List`1" || item.TYPE_OPTION_NAME == "radio_button" || item.TYPE_OPTION_NAME == "dropdown")
                {
                    item.TYPE_ANSWER = "array";
                    item.ANSWER = VALUE ?? new List<dynamic>();
                    item.ANSWER_TEXT = "";
                } else
                {
                    item.TYPE_ANSWER = "text";
                    item.ANSWER = new List<dynamic>();
                    item.ANSWER_TEXT = VALUE ?? "";
                }
            }

            return result;
        }

        public IList<M_Option> GetOptionDetail(int? ID_REFERENCE, string SEARCH_VALUE, int? QUESTION_ID, int? PERIODE_ID, int? OPTION_LIST, int? OPTION_PARENT_VALUE, bool? RECENT_DATA)
        {
            IList<M_Option> result;

            if (OPTION_LIST == 1)
            {
                dynamic args = new
                {
                    P_ID_REFERENCE = ID_REFERENCE,
                    P_SEARCH_VALUE = SEARCH_VALUE,
                    P_QUESTION_ID = QUESTION_ID,
                    P_VALUE = OPTION_PARENT_VALUE
                };

                result = db.Fetch<M_Option>("QuestionCategory/QuestionCategory_GetDynamicOptionDetail", args);
            } else
            {
                dynamic args = new
                {
                    P_M_QUESTION_ID = QUESTION_ID,
                    P_M_PERIODE_ID = PERIODE_ID,
                    P_RECENT_DATA = RECENT_DATA
                };

                result = db.Fetch<M_Option>("QuestionCategory/QuestionCategory_GetOptionDetail", args);
            }

            return result;
        }

        public dynamic GetAnswerQuestion(string NOREG, int? PERIODE_ID, int? QUESTION_ID)
        {
            dynamic args = new
            {
                P_NOREG = NOREG,
                P_M_PERIODE_ID = PERIODE_ID,
                P_M_QUESTION_ID = QUESTION_ID
            };

            dynamic VALUE = null;

            IList<FieldTableAnswerDetail> result = db.Fetch<FieldTableAnswerDetail>("QuestionCategory/QuestionCategory_GetAnswerValue", args);

            if (result.Count > 0)
            {
                if (result[0].ANSWER != null)
                {
                    VALUE = new List<dynamic>();
                    foreach (var item in result)
                    {
                        VALUE.Add(item.ANSWER);
                    }
                }
                else
                {
                    VALUE = result[0].ANSWER_TEXT;
                }
            }

            return VALUE;
        }

        public bool InsertQuestionData(string NOREG, int M_PERIODE_ID, int M_CATEGORY_ID, bool UPDATE_SUBMISSION_DATE, string XML_DATA_ANSWER, string XML_DATA_ANSWER_DETAIL)
        {
            dynamic args = new {
                P_NOREG = NOREG,
                P_M_PERIODE_ID = M_PERIODE_ID,
                P_M_CATEGORY_ID = M_CATEGORY_ID,
                P_UPDATE_SUBMISSION_DATE = UPDATE_SUBMISSION_DATE,
                P_DATA_ANSWER = XML_DATA_ANSWER,
                P_DATA_ANSWER_DETAIL = XML_DATA_ANSWER_DETAIL,
            };

            ResultMessage result = db.Fetch<ResultMessage>("QuestionCategory/QuestionCategory_InsertData", args)[0];
            db.Close();

            if (result.result != "true") {
                throw new Exception(result.msg);
            }

            return true;
        }

    }
}
