using System;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using System.Collections.Generic;
using ASPNETMVC3TDK.Models.People;
 
namespace ASPNETMVC3TDK.Controllers
{
    public class PeopleApiController : PageController
    {
        PeopleRepo PeopleRepo = new PeopleRepo();

        #region GetPeople
        [HttpGet]
        public JsonResult GetPeople(string NOREG, string SUPER, string DIVISION, string DEPARTEMENT, string SECTION, string LINE, 
            string GROUP, string SEARCH, string CLASS, string POSITION, string RECENT, string SKILLS, int? OFFSET, int? FETCH, string ALL)
        {
            //User user = Lookup.Get<User>();
            try
            {
                IList<People> result = PeopleRepo.GetPeople(NOREG, SUPER, DIVISION, DEPARTEMENT, SECTION, LINE, GROUP, SEARCH, CLASS, POSITION, 
                    RECENT, SKILLS, OFFSET, FETCH, ALL);

                IList<People> resultCount = PeopleRepo.GetPeople(NOREG, SUPER, DIVISION, DEPARTEMENT, SECTION, LINE, GROUP, SEARCH, CLASS, POSITION, 
                    RECENT, SKILLS, null, null, ALL);

                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
                    countData = resultCount.Count,
                    message = "get data successfully!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    status = 200,
                    data = ex.Message,
                    message = "get data failed!"
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}