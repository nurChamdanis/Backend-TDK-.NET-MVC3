using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database; 
using System;
using System.Linq; 
using System.IO; 
using System.Threading.Tasks;
using System.Web;

namespace ASPNETMVC3TDK.Models.TrainingHistory
{
    public class TrainingHistoryRepo
    {
        #region Singleton

        private static TrainingHistoryRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext();
        /*IDBContext db2 = DatabaseManager.Instance.GetContext("SecurityCenter");*/

        public static TrainingHistoryRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TrainingHistoryRepo();
                }
                return instance;
            }

        }
        #endregion

        public IList<M_TrainingHistory> GetTrainingHistories(string P_NOREG, string P_ORDER_TYPE, string P_STATUS, bool MODEMASTER, M_TrainingHistory m)
        {
            IList<M_TrainingHistory> result;
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS,
            };
            if (MODEMASTER )
            {
                result = db.Fetch<M_TrainingHistory>("Training/History/getMasterHistory", args);
            }
            else
            {
                result = db.Fetch<M_TrainingHistory>("Training/History/getHistory", args); 
            }
            db.GetSqlLoaders();
            db.Close();
            return result;
        }


        public async Task<ResultMessage> UpdateData(M_TrainingHistory m)
        {


            ResultMessage result = new ResultMessage();
            try
            {
                // Get base folder path for personal information documents
                var personalInformationFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Document/Training");

                // Check if base folder path is null
                if (personalInformationFolderPath == null)
                {
                    throw new InvalidOperationException("Failed to resolve base folder path for personal information documents.");
                }

                // Combine base folder path with "Education" folder
                string contentFolderPath = Path.Combine(personalInformationFolderPath, "History");



                if (Directory.Exists(contentFolderPath))
                {
                    Console.WriteLine("Folder exists.");

                    var fileNames = ""; // Initialize fileName variable   

                    // Check if CERTIFICATE collection is not null and contains at least one file
                    if (m.CERTIFICATE != null && m.CERTIFICATE.FirstOrDefault() != null && contentFolderPath != null)
                    {
                        contentFolderPath = contentFolderPath.Replace('\\', '/');

                        // Generate a unique timestamp (example: current timestamp)
                        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                        // Construct the custom file name
                        fileNames = $"TH_{m.NOREG}_{timestamp}";
                        IEnumerable<HttpPostedFileBase> files = m.CERTIFICATE;
                        string ext = null;
                        string filenames = null;
                        foreach (var file in files)
                        {
                            filenames = Path.GetFileName(file.FileName);
                            // Use fileName as needed
                        }
                        ext = Path.GetExtension(filenames);
                        Console.WriteLine(ext + "  this code for check extention ");


                        // Upload the file to specified folder
                        bool uploadFile = await FileUploadHelper.Upload(m.CERTIFICATE, contentFolderPath, fileNames);

                        // Check if upload was successful
                        if (uploadFile)
                        {
                            dynamic args = new
                            {
                                P_NOREG = m.NOREG,
                                P_TRANSACTION_ID = m.TRANSACTION_ID,
                                P_SEQ = m.SEQ,
                                P_INSTITUTION = m.INSTITUTION,
                                P_COUNTRY_ID = m.COUNTRY_ID,
                                P_YEAR = m.YEAR,
                                P_TRAINING_TOPIC = m.TRAINING_TOPIC,
                                P_SKILL = m.SKILL,
                                P_CERTIFICATE_NAME = fileNames + "" + ext,
                                P_CERTIFICATE_PATH = "/Content/Document/Training/History/",
                                P_REMARK_1 = m.REMARK_1,
                                P_REMARK_2 = m.REMARK_2
                            };
                            result = db.Fetch<ResultMessage>("Training/History/getUpdateData", args)[0];
                        }
                    }
                    else
                    {
                        dynamic args = new
                        {
                            P_NOREG = m.NOREG,
                            P_TRANSACTION_ID = m.TRANSACTION_ID,
                            P_SEQ = m.SEQ,
                            P_INSTITUTION = m.INSTITUTION,
                            P_COUNTRY_ID = m.COUNTRY_ID,
                            P_YEAR = m.YEAR,
                            P_TRAINING_TOPIC = m.TRAINING_TOPIC,
                            P_SKILL = m.SKILL,
                            P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                            P_CERTIFICATE_PATH = "/Content/Document/Training/History/",
                            P_REMARK_1 = m.REMARK_1,
                            P_REMARK_2 = m.REMARK_2
                        };
                        result = db.Fetch<ResultMessage>("Training/History/getUpdateData", args)[0];
                    }

                    //
                }
                else
                {
                    Console.WriteLine("Folder does not exist.");
                    try
                    {
                        Directory.CreateDirectory(contentFolderPath);

                        var fileNames = ""; // Initialize fileName variable

                        // Check if CERTIFICATE collection is not null and contains at least one file
                        if (m.CERTIFICATE != null && m.CERTIFICATE.FirstOrDefault() != null && contentFolderPath != null)
                        {
                            contentFolderPath = contentFolderPath.Replace('\\', '/');

                            // Generate a unique timestamp (example: current timestamp)
                            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                            // Construct the custom file name
                            fileNames = $"TH_{m.NOREG}_{timestamp}";
                            IEnumerable<HttpPostedFileBase> files = m.CERTIFICATE;
                            string ext = null;
                            string filenames = null;
                            foreach (var file in files)
                            {
                                filenames = Path.GetFileName(file.FileName);
                                // Use fileName as needed
                            }
                            ext = Path.GetExtension(filenames);
                            Console.WriteLine(ext + "  this code for check extention ");


                            // Upload the file to specified folder
                            bool uploadFile = await FileUploadHelper.Upload(m.CERTIFICATE, contentFolderPath, fileNames);

                            // Check if upload was successful
                            if (uploadFile)
                            {
                                dynamic args = new
                                {
                                    P_NOREG = m.NOREG,
                                    P_TRANSACTION_ID = m.TRANSACTION_ID,
                                    P_SEQ = m.SEQ,
                                    P_INSTITUTION = m.INSTITUTION,
                                    P_COUNTRY_ID = m.COUNTRY_ID,
                                    P_YEAR = m.YEAR,
                                    P_TRAINING_TOPIC = m.TRAINING_TOPIC,
                                    P_SKILL = m.SKILL,
                                    P_CERTIFICATE_NAME = fileNames + "" + ext,
                                    P_CERTIFICATE_PATH = "/Content/Document/Training/History/",
                                    P_REMARK_1 = m.REMARK_1,
                                    P_REMARK_2 = m.REMARK_2
                                };
                                result = db.Fetch<ResultMessage>("Training/History/getUpdateData", args)[0];
                            }
                        }
                        else
                        {
                            dynamic args = new
                            {
                                P_NOREG = m.NOREG,
                                P_TRANSACTION_ID = m.TRANSACTION_ID,
                                P_SEQ = m.SEQ,
                                P_INSTITUTION = m.INSTITUTION,
                                P_COUNTRY_ID = m.COUNTRY_ID,
                                P_YEAR = m.YEAR,
                                P_TRAINING_TOPIC = m.TRAINING_TOPIC,
                                P_SKILL = m.SKILL,
                                P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/Training/History/",
                                P_REMARK_1 = m.REMARK_1,
                                P_REMARK_2 = m.REMARK_2
                            };
                            result = db.Fetch<ResultMessage>("Training/History/getUpdateData", args)[0];
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating folder: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw exception to handle it higher up in the call stack
            }
            finally
            {
                db.Close(); // Ensure database connection is closed
            }
            return result;

        }


        public ResultMessage DeleteData(M_TrainingHistory m)
        {
            dynamic args = new
            { 
                P_TRANSACTION_ID = m.TRANSACTION_ID 
            };
            ResultMessage Result = db.Fetch<ResultMessage>("Training/History/getDeleteData", args)[0];
            db.Close();
            return Result;
        }





    }
}