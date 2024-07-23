using System;
using System.Linq;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.UserProfile;
using ASPNETMVC3TDK.Models.UserProfileDescrip;
using System.Linq.Expressions;
using System.Collections.Generic;
 
namespace ASPNETMVC3TDK.Controllers
{
    public class UserProfileApiController : PageController
    {
        UserProfileRepo userProfileRepo = new UserProfileRepo();
        UserProfileDescripRepo userProfileDescripRepo = new UserProfileDescripRepo();
        DataTables datatable = new DataTables();

        #region GetUserProfile
        [HttpPost]
        public JsonResult GetUserProfile(UserProfile m)
        {
            User user = Lookup.Get<User>();
            try
            {

                IList<UserProfile> result = userProfileRepo.getUserProfileInformation(m.NOREG, "DESC", "4", m);

                var response = new
                {
                    status = 200,
                    data = result,
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


        # region GetUserProfileDescp
        [HttpPost]
        public JsonResult GetUserProfileDesc(UserProfileDescrip m)
        {
            User user = Lookup.Get<User>();
            try
            {

                var result = userProfileDescripRepo.GetUserProfileDesc(m.NOREG, m);
                var response = new
                {
                    status = 200,
                    data = result,
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
         
        //Update User Data Description
        #region UpdateRotationHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateDataDescrip(UserProfile m)
        {
            //User user = Lookup.Get<User>();
            return Json(userProfileRepo.UpdateDataDescrip(m), JsonRequestBehavior.AllowGet);
        }
        #endregion
 

    }
}