
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;

namespace ASPNETMVC3TDK.Models.QuestionCategory
{
    public class M_Category
    {
        public int M_CATEGORY_ID { get; set; } 
        public string CATEGORY_NAME { get; set; }
        public string CATEGORY_DESC { get; set; }
    }

    public class M_Question
    {
        public int M_QUESTION_ID { get; set; }
        public string QUESTION_DESC { get; set; }
        public string QUESTION_MANDATORY { get; set; }
        public string QUESTION_SEPARATOR { get; set; }
        public int? OPTION_LIST { get; set; }
        public int? OPTION_PARENT { get; set; }
        public string OPTION_PLACEHOLDER { get; set; }
        public string TYPE_OPTION_NAME { get; set; }
        public string TYPE_ANSWER { get; set; }
        public string ANSWER_TEXT { get; set; }
        public IList<dynamic> ANSWER { get; set; }
    }

    public class M_Answer
    {
        public string ANSWER { get; set; }
        public string ANSWER_TEXT { get; set; }
    }

    public class M_Option
    {
        public string ID { set; get; }
        public string DESC { set; get; }
    }

    public class PeriodeCheckAttr
    {
        public int? M_PERIODE_ID { set; get; }
        public bool? FOUND_PERIODE { set; get; }
    }

    public class SubmissionCheckAttr
    {
        public int? R_SUBMISSION_ID { set; get; }
        public int? M_PERIODE_ID { set; get; }
        public string NOREG { set; get; }
    }

    public class RequestDataQuestion
    {
        public string NOREG { get; set; }
        public int M_PERIODE_ID { get; set; }
        public int M_CATEGORY_ID { get; set; }
        public bool UPDATE_SUBMISSION_DATE { get; set; }
        public IList<M_Question> DATA_QUESTION { get; set; }
    }

    public class FieldTableAnswer
    {
        public string R_ANSWER_ID { get; set; }
        public int M_PERIODE_ID { get; set; }
        public int M_CATEGORY_ID { get; set; }
        public string NOREG { get; set; }
        public int M_QUESTION_ID { get; set; }
        public string ANSWER_SUBMITTED_DT { get; set; }
        public string CREATED_DT { get; set; }
        public string CREATED_BY { get; set; }
        public int R_SUBMISSION_ID { get; set; }
    }

    public class FieldTableAnswerDetail
    {
        public string R_ANSWER_DETAIL_ID { get; set; }
        public string R_ANSWER_ID { get; set; }
        public string ANSWER { get; set; }
        public string CREATED_DT { get; set; }
        public string CREATED_BY { get; set; }
        public string ANSWER_TEXT { get; set; }
    }

}