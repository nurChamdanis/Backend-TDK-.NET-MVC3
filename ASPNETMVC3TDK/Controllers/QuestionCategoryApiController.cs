using System;
using System.IO;
using System.Xml;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Collections.Generic;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Models.QuestionCategory;

namespace ASPNETMVC3TDK.Controllers
{
    public class QuestionCategoryApiController : PageController
    {
        QuestionCategoryRepo repo = new QuestionCategoryRepo();

        #region GetQuestionCategory
        [HttpPost]
        public JsonResult GetQuestionCategory(string NOREG, int? SUBMISSION_ID, int? CATEGORY_ID)
        {        
            User user = Lookup.Get<User>();
            try
            {
                SubmissionCheckAttr resultSubmission = new SubmissionCheckAttr();

                bool self_data = true;
                if (SUBMISSION_ID != null){
                    resultSubmission = repo.SubmissionCheck(SUBMISSION_ID);

                    self_data = (NOREG == resultSubmission.NOREG);
                }

                PeriodeCheckAttr resultPeriod = repo.PeriodeCheck(resultSubmission.M_PERIODE_ID, resultSubmission.NOREG);
                IList<M_Category> resultRows = repo.GetQuestionCategory(CATEGORY_ID, resultSubmission.M_PERIODE_ID, resultPeriod.FOUND_PERIODE);

                var response = new
                {
                    status = 1,
                    data = resultRows,
                    periode_id = resultPeriod.M_PERIODE_ID,
                    self_data = self_data,
                    message = "get data success!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 0,
                    data = (int?)null,
                    message = ex.Message
                };

                Response.StatusCode = 400;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region GetQuestionByCategory
        [HttpPost]
        public JsonResult GetQuestionByCategory(string NOREG, int? PERIODE_ID, int? CATEGORY_ID, int? QUESTION_ID)
        {
            User user = Lookup.Get<User>();
            try
            {
                PeriodeCheckAttr resultPeriod = repo.PeriodeCheck(PERIODE_ID, NOREG);
                IList<M_Question> resultRows = repo.GetQuestionByCategory(NOREG, resultPeriod.M_PERIODE_ID, CATEGORY_ID, QUESTION_ID, resultPeriod.FOUND_PERIODE);

                var response = new
                {
                    status = 200 ,
                    data = resultRows,
                    permission_edit = resultPeriod.FOUND_PERIODE,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 400,
                    data = (int?)null,
                    message = ex.Message
                };

                Response.StatusCode = 400;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region GetOptionDetail
        [HttpPost]
        public JsonResult GetOptionDetail(int? ID_REFERENCE, string SEARCH_VALUE, int? QUESTION_ID, int? PERIODE_ID, int? OPTION_LIST, int? OPTION_PARENT_VALUE, bool? RECENT_DATA)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = repo.GetOptionDetail(ID_REFERENCE, SEARCH_VALUE, QUESTION_ID, PERIODE_ID, OPTION_LIST, OPTION_PARENT_VALUE, RECENT_DATA);
                var response = new
                {
                    status = 200,
                    data = result,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 500,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region InsertData
        [HttpPost]
        public JsonResult InsertData(RequestDataQuestion request)
        {
            User user = Lookup.Get<User>();
            try
            {
                string NOREG = request.NOREG;
                int M_PERIODE_ID = request.M_PERIODE_ID;
                int M_CATEGORY_ID = request.M_CATEGORY_ID;
                bool UPDATE_SUBMISSION_DATE = request.UPDATE_SUBMISSION_DATE;
                var DATA_QUESTION = request.DATA_QUESTION;
                DateTime CURRENT_TIME = DateTime.Now;

                // Menginisialisasi data TB_R_ANSWER dan TB_R_ANSWER_DETAIL
                IList<FieldTableAnswer> DATA_LIST_TB_ANSWER = new List<FieldTableAnswer>();
                IList<FieldTableAnswerDetail> DATA_LIST_TB_ANSWER_DETAIL = new List<FieldTableAnswerDetail>();

                int INDEX_ITEM_ANSWER= 0;
                foreach (var ITEM_DATA in DATA_QUESTION)
                {
                    if ((ITEM_DATA?.TYPE_ANSWER == "array" && ITEM_DATA?.ANSWER != null) || (ITEM_DATA?.TYPE_ANSWER == "text" && ITEM_DATA?.ANSWER_TEXT != null))
                    {
                        FieldTableAnswer DATA_ANSWER = new FieldTableAnswer();
                        string R_ANSWER_ID = "R_ANS_" + NOREG + "_" + CURRENT_TIME.ToString("yyyyMMddHHmmss") + "_" + INDEX_ITEM_ANSWER;

                        DATA_ANSWER.R_ANSWER_ID = R_ANSWER_ID;
                        DATA_ANSWER.M_PERIODE_ID = M_PERIODE_ID;
                        DATA_ANSWER.M_CATEGORY_ID = M_CATEGORY_ID;
                        DATA_ANSWER.NOREG = NOREG;
                        DATA_ANSWER.M_QUESTION_ID = ITEM_DATA.M_QUESTION_ID;
                        DATA_ANSWER.ANSWER_SUBMITTED_DT = CURRENT_TIME.ToString("yyyy-MM-dd HH:mm:ss");
                        DATA_ANSWER.CREATED_DT = CURRENT_TIME.ToString("yyyy-MM-dd HH:mm:ss");
                        DATA_ANSWER.CREATED_BY = NOREG;
                        DATA_ANSWER.R_SUBMISSION_ID = 3;

                        DATA_LIST_TB_ANSWER.Add(DATA_ANSWER);

                        string R_ANSWER_DETAIL_ID = "";
                        if (ITEM_DATA.TYPE_ANSWER == "array")
                        {
                            int INDEX_ITEM_ANSWER_DETAIL = 0;

                            foreach (var ITEM_DATA_ANSWER_ARRAY in ITEM_DATA.ANSWER)
                            {
                                FieldTableAnswerDetail DATA_ANSWER_DETAIL = new FieldTableAnswerDetail();

                                R_ANSWER_DETAIL_ID = "R_ANS_DETAIL_" + NOREG + "_" + CURRENT_TIME.ToString("yyyyMMddHHmmss") + "_" + INDEX_ITEM_ANSWER + INDEX_ITEM_ANSWER_DETAIL;

                                DATA_ANSWER_DETAIL.R_ANSWER_DETAIL_ID = R_ANSWER_DETAIL_ID;
                                DATA_ANSWER_DETAIL.R_ANSWER_ID = R_ANSWER_ID;
                                DATA_ANSWER_DETAIL.CREATED_DT = CURRENT_TIME.ToString("yyyy-MM-dd HH:mm:ss");
                                DATA_ANSWER_DETAIL.CREATED_BY = NOREG;
                                DATA_ANSWER_DETAIL.ANSWER = ITEM_DATA_ANSWER_ARRAY.ToString();
                                DATA_ANSWER_DETAIL.ANSWER_TEXT = "";

                                DATA_LIST_TB_ANSWER_DETAIL.Add(DATA_ANSWER_DETAIL);

                                R_ANSWER_DETAIL_ID = "";

                                INDEX_ITEM_ANSWER_DETAIL += 1;
                            }
                        }
                        else
                        {
                            FieldTableAnswerDetail DATA_ANSWER_DETAIL = new FieldTableAnswerDetail();

                            R_ANSWER_DETAIL_ID = "R_ANS_DETAIL_" + NOREG + "_" + CURRENT_TIME.ToString("yyyyMMddHHmmss") + "_0" + INDEX_ITEM_ANSWER;

                            DATA_ANSWER_DETAIL.R_ANSWER_DETAIL_ID = R_ANSWER_DETAIL_ID;
                            DATA_ANSWER_DETAIL.R_ANSWER_ID = R_ANSWER_ID;
                            DATA_ANSWER_DETAIL.CREATED_DT = CURRENT_TIME.ToString("yyyy-MM-dd HH:mm:ss");
                            DATA_ANSWER_DETAIL.CREATED_BY = NOREG;
                            DATA_ANSWER_DETAIL.ANSWER = "";
                            DATA_ANSWER_DETAIL.ANSWER_TEXT = ITEM_DATA.ANSWER_TEXT;

                            DATA_LIST_TB_ANSWER_DETAIL.Add(DATA_ANSWER_DETAIL);
                        }

                        INDEX_ITEM_ANSWER += 1;
                    }
                }
                // ** //

                if (DATA_LIST_TB_ANSWER.Count > 0)
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", ""); // This removes default namespaces

                    XmlSerializer XML_SERIALIZER_DATA_ANSWER = new XmlSerializer(typeof(List<FieldTableAnswer>));
                    XmlSerializer XML_SERIALIZER_DATA_ANSWER_DETAIL = new XmlSerializer(typeof(List<FieldTableAnswerDetail>));

                    // Configure XmlWriterSettings to omit XML declaration
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.OmitXmlDeclaration = true;

                    var XML_DATA_ANSWER = "";
                    using (var STRING_WRITER_ANSWER = new StringWriter())
                    {
                        using (XmlWriter XML_WRITER_ANSWER = XmlWriter.Create(STRING_WRITER_ANSWER, settings))
                        {
                            XML_SERIALIZER_DATA_ANSWER.Serialize(XML_WRITER_ANSWER, DATA_LIST_TB_ANSWER, namespaces);
                            XML_DATA_ANSWER = STRING_WRITER_ANSWER.ToString();
                        }
                    }

                    var XML_DATA_ANSWER_DETAIL = "";
                    using (var STRING_WRITER_DETAIL = new StringWriter())
                    {
                        using (XmlWriter XML_WRITER_ANSWER_DETAIL = XmlWriter.Create(STRING_WRITER_DETAIL, settings))
                        {
                            XML_SERIALIZER_DATA_ANSWER_DETAIL.Serialize(XML_WRITER_ANSWER_DETAIL, DATA_LIST_TB_ANSWER_DETAIL, namespaces);
                            XML_DATA_ANSWER_DETAIL = STRING_WRITER_DETAIL.ToString();
                        }
                    }

                    dynamic result = repo.InsertQuestionData(
                        NOREG,
                        M_PERIODE_ID,
                        M_CATEGORY_ID,
                        UPDATE_SUBMISSION_DATE,
                        XML_DATA_ANSWER,
                        XML_DATA_ANSWER_DETAIL
                    );
                }

                var response = new
                {
                    status = 200,
                    data = (string)null,
                    message = "insert data success!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 500,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        
    }
}