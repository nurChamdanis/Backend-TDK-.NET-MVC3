using System;
using System.Web.Mvc; 
using Toyota.Common.Web.Platform; 
using ASPNETMVC3TDK.Models.Export; 
using ASPNETMVC3TDK.Models.LanguageSkill; 
using System.Collections.Generic;
using ASPNETMVC3TDK.Models.People;

namespace ASPNETMVC3TDK.Controllers.Pages
{
    public class ExportApiController : PageController
    {
        ExportExcelRepo exportAllRepo = new ExportExcelRepo(); 
        PeopleRepo peopleRepo = new PeopleRepo();


        #region getExportExcel
        [HttpGet]
        public JsonResult getExportExcel (string NOREG, string SUPER, string DIVISION, string DEPARTEMENT, string SECTION, string LINE
            , string GROUP, string SEARCH, string CLASS, string POSITION, string RECENT, string SKILLS, int? OFFSET, int? FETCH, string ALL)
        {
            dynamic response = new { };
            dynamic result = new { };
            try
            {
                IList<People> people = PeopleRepo.GetPeople(NOREG, SUPER, DIVISION, DEPARTEMENT, SECTION, LINE, GROUP, SEARCH, CLASS, 
                    POSITION, RECENT, SKILLS, null, null, ALL);
                string[] noregArray = new string[people.Count];
                string query = "";
                for (int i = 0; i < people.Count; i++)
                {
                    People person = people[i];
                    string noreg = person.NOREG;
                    query += "'" + noreg + "'"; 
                    if (i < people.Count - 1)
                        query += ", ";
                }; 

                result = exportAllRepo.getExportExcel(NOREG, query, SUPER, people);

                result.Result.success = "true"; 
                result.Result.msgInfo = "Export excel sucessfuly !"; 
                result.Result.msg = "Export excel sucessfuly !";
                response = Json( 
                    result.Result
                    , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response = new
                {
                    status = 0,
                    data = ex.Message,
                    message = "get data failed!"
                };
                Response.StatusCode = 400;
                response =  Json(result, JsonRequestBehavior.AllowGet);
            }
            return response;
        }
        #endregion

        /**/

        
    }
}