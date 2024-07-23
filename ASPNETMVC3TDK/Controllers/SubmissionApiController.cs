using System;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using System.Collections.Generic;
using ASPNETMVC3TDK.Models.Submission;
 
namespace ASPNETMVC3TDK.Controllers
{
    public class SubmissionApiController : PageController
    {
        SubmissionRepo SubmissionRepo = new SubmissionRepo();

        #region GetListSubmission
        [HttpGet]
        public JsonResult GetListSubmission(string NOREG, string SEARCH, string DIVISION, string DEPARTEMENT, string SECTION,
            string SORT, int? OFFSET, int? FETCH, string ALL)
        {
            //User user = Lookup.Get<User>();
            try
            {
                IList<Submission> resultData = SubmissionRepo.GetSubmissions(NOREG, SEARCH, DIVISION, DEPARTEMENT, SECTION, SORT, OFFSET, FETCH, ALL);

                int resultCount = SubmissionRepo.GetCount(NOREG, SEARCH, DIVISION, DEPARTEMENT, SECTION, ALL);

                var response = new
                {
                    status = 200,
                    data = resultData,
                    total_data = resultCount,
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