using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyota.Common.Credential;
using Toyota.Common.Web.Platform;
using ASPNETMVC3TDK.Shared;
using ASPNETMVC3TDK.Models;
using ASPNETMVC3TDK.Models.Eligibility;
using ASPNETMVC3TDK.Models.TrainingHistory;
using ASPNETMVC3TDK.Models.Achivement;
using System.Linq.Expressions;
using System.Collections.Generic;
using Toyota.Common;

namespace ASPNETMVC3TDK.Controllers
{
    public class TrainingApiController : PageController
    {
        TrainingHistoryRepo trainingHistoryRepo = new TrainingHistoryRepo();
        TrainingEligibilityRepo trainingEligibilityRepo = new TrainingEligibilityRepo();
        AchivementRepo achivementRepo = new AchivementRepo();
        DataTables datatable = new DataTables();


        // getTrainingHistory
        #region getTrainingHistory
        [HttpPost]
        public JsonResult getTrainingHistory(M_TrainingHistory m, bool MODEMASTER)
        { 
            try
            {
                var result = trainingHistoryRepo.GetTrainingHistories(m.NOREG, "DESC", "4", MODEMASTER,  m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
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
         
        //UpdateTrainingHistory
        #region UpdateTrainingHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateTrainingHistory(M_TrainingHistory m)
        {
            try
            { 
                var files = m.CERTIFICATE;
            }
            catch (Exception ex)
            { //a
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            var result = trainingHistoryRepo.UpdateData(m);
            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //DeleteTrainingHistory
        #region DeleteTrainingHistory
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteTrainingHistory(M_TrainingHistory m)
        {
            try
            {
                var files = m.CERTIFICATE;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return Json(trainingHistoryRepo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion


            
        /* API ELIGILIBILITY    
                */
        #region getTrainingEligilibity
        [HttpPost]
        public JsonResult getTrainingEligibility(M_Eligibility m, bool MODEMASTER)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = trainingEligibilityRepo.GetTrainingEligibility(m.NOREG, "DESC", "4", MODEMASTER, m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
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
         
        //Update Training ELIGILIBILITY
        #region UpdateTrainingEligilibility
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateTrainingEligibility(M_Eligibility m)
        {
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.INSTITUTION))
                    cek += "Institution, ";
                if (string.IsNullOrWhiteSpace(m.CERTIFICATE_NAME))
                    cek += "Certificate name, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }

                var files = m.CERTIFICATE;
            }
            catch (Exception ex)
            {
                var fail = new Result();
                fail.ResultCode = false;
                fail.Message = ex.Message;
                Response.StatusCode = 400;
                return Json(fail, JsonRequestBehavior.AllowGet);
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            var result = trainingEligibilityRepo.UpdateData(m);
            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //Delete Training ELIGILIBILITY
        #region DeleteTrainingEligilibility
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteTrainingEligibility(M_Eligibility m)
        {
            //User user = Lookup.Get<User>();
            return Json(trainingEligibilityRepo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion



        /* */
        /* API ACHIVEMENT */
        #region getAchivement
        [HttpPost]
        public JsonResult getTrainingAchivement(M_Achivement m, bool MODEMASTER)
        {
            User user = Lookup.Get<User>();
            try
            {
                var result = achivementRepo.GetTrainingAchivement(m.NOREG, "DESC", "4", MODEMASTER, m);
                var response = new
                {
                    status = 200,
                    data = result,
                    count = result.Count,
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
         
        //UpdateTrainingAchivement
        #region UpdateTrainingAchivement
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult UpdateTrainingAchivement(M_Achivement m)
        {
            try
            {
                string cek = "";
                if (string.IsNullOrWhiteSpace(m.INSTITUTION))
                    cek += "Institution, ";
                if (string.IsNullOrWhiteSpace(m.TRAINING_TOPIC))
                    cek += "Training topic, ";
                if (!string.IsNullOrEmpty(cek))
                {
                    cek = cek.Trim();
                    cek = cek.EndsWith(",") ? cek.Substring(0, cek.LastIndexOf(',')) + "." : cek;
                    throw new Exception("Required : " + cek);
                }
                var files = m.CERTIFICATE;
            }
            catch (Exception ex)
            {
                var fail = new Result();
                fail.ResultCode = false;
                fail.Message = ex.Message;
                Response.StatusCode = 400;
                return Json(fail, JsonRequestBehavior.AllowGet);
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            var result = achivementRepo.UpdateData(m);
            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //UpdateTrainingAchivement
        #region DeleteTrainingAchivement
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult DeleteTrainingAchivement(M_Achivement m)
        {
            //User user = Lookup.Get<User>();
            return Json(achivementRepo.DeleteData(m), JsonRequestBehavior.AllowGet);
        }
        #endregion








    }
}