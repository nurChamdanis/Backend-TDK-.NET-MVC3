using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database; 
using System;
using System.Linq; 
using System.IO; 
using System.Threading.Tasks;
using System.Web;

namespace ASPNETMVC3TDK.Models.LanguageSkill

{
    public class LanguageSkillRepo
    {
        #region Singleton

        private static LanguageSkillRepo instance = null;
        IDBContext db = DatabaseManager.Instance.GetContext(); 

        public static LanguageSkillRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LanguageSkillRepo();
                }
                return instance;
            }

        }
        #endregion

        public IList<M_LanguageSkill> GetLanguageSkillById(string P_LANGUAGEID)
        {
            dynamic args = new { P_LANGUAGEID };
            IList<M_LanguageSkill> Result = db.Fetch<M_LanguageSkill>("MasterDataApi/LanguageSkill/getLanguageSkill", args);
            db.GetSqlLoaders();
            db.Close();
            return Result;
        }




        public IList<M_LanguageSkill> GetLanguageSkills (string P_NOREG, string P_ORDER_TYPE, string P_STATUS, bool MODEMASTER, M_LanguageSkill m)
        {
            IList<M_LanguageSkill> result;
            dynamic args = new
            {
                P_NOREG,
                P_ORDER_TYPE,
                P_STATUS
            };
            if (MODEMASTER)
            { 
                result = db.Fetch<M_LanguageSkill>("PersonalInformation/LanguageSkill/getMasterLanguage", args);
            }
            else
            { 
                result = db.Fetch<M_LanguageSkill>("PersonalInformation/LanguageSkill/getLanguageSkill", args);
            }
            db.GetSqlLoaders();
            db.Close();
            return result;
        }


        public async Task<ResultMessage> SaveData(M_LanguageSkill m)
        {  
            ResultMessage result = new ResultMessage(); 
            try
            {
                // Get base folder path for personal information documents
                var personalInformationFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Document/PersonalInformation");

                // Check if base folder path is null
                if (personalInformationFolderPath == null)
                {
                    throw new InvalidOperationException("Failed to resolve base folder path for personal information documents.");
                }

                // Combine base folder path with "Education" folder
                string contentFolderPath = Path.Combine(personalInformationFolderPath, "Language");

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
                        fileNames = $"LN_{m.NOREG}_{timestamp}";
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
                                // 
                                P_NOREG = m.NOREG,
                                P_LANGUAGE_ID = m.LANGUAGE_ID,
                                P_SKILL_LEVEL = m.SKILL_LEVEL,
                                P_SCORE = m.SCORE,
                                P_CERTIFICATE_NAME = fileNames + "" + ext,
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                                P_VALID_TO = m.VALID_TO,
                                P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                                P_STATUS_CD = m.STATUS_CD,
                                P_REJECT_REASON = m.REJECT_REASON,
                                P_REMARK_1 = m.REMARK_1,
                                P_REMARK_2 = m.REMARK_2,
                            };

                            // Perform database update operation
                            result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getSaveData", args)[0];
                        }

                        //
                    }
                    else
                    {
                        dynamic args = new
                        {
                            // 
                            P_NOREG = m.NOREG,
                            P_LANGUAGE_ID = m.LANGUAGE_ID,
                            P_SKILL_LEVEL = m.SKILL_LEVEL,
                            P_SCORE = m.SCORE,
                            P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                            P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                            P_VALID_TO = m.VALID_TO,
                            P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                            P_STATUS_CD = m.STATUS_CD,
                            P_REJECT_REASON = m.REJECT_REASON,
                            P_REMARK_1 = m.REMARK_1,
                            P_REMARK_2 = m.REMARK_2,
                        };

                        // Perform database update operation
                        result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getSaveData", args)[0];
                    }
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
                            fileNames = $"LN_{m.NOREG}_{timestamp}";
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
                                    P_LANGUAGE_ID = m.LANGUAGE_ID,
                                    P_SKILL_LEVEL = m.SKILL_LEVEL,
                                    P_SCORE = m.SCORE,
                                    P_CERTIFICATE_NAME = fileNames + "" + ext,
                                    P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                                    P_VALID_TO = m.VALID_TO,
                                    P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                                    P_STATUS_CD = m.STATUS_CD,
                                    P_REJECT_REASON = m.REJECT_REASON,
                                    P_REMARK_1 = m.REMARK_1,
                                    P_REMARK_2 = m.REMARK_2,
                                };

                                // Perform database update operation
                                result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getSaveData", args)[0];
                            }

                            //
                        }
                        else
                        {
                            dynamic args = new
                            {
                                P_NOREG = m.NOREG,
                                P_LANGUAGE_ID = m.LANGUAGE_ID,
                                P_SKILL_LEVEL = m.SKILL_LEVEL,
                                P_SCORE = m.SCORE,
                                P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                                P_VALID_TO = m.VALID_TO,
                                P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                                P_STATUS_CD = m.STATUS_CD,
                                P_REJECT_REASON = m.REJECT_REASON,
                                P_REMARK_1 = m.REMARK_1,
                                P_REMARK_2 = m.REMARK_2,
                            };

                            // Perform database update operation
                            result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getSaveData", args)[0];

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



        public async Task<ResultMessage> UpdateData(M_LanguageSkill m)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                // Get base folder path for personal information documents
                var personalInformationFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Document/PersonalInformation");

                // Check if base folder path is null
                if (personalInformationFolderPath == null)
                {
                    throw new InvalidOperationException("Failed to resolve base folder path for personal information documents.");
                }

                // Combine base folder path with "Education" folder
                string contentFolderPath = Path.Combine(personalInformationFolderPath, "Language");

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
                        fileNames = $"LN_{m.NOREG}_{timestamp}";
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
                                P_TRANSACTION_ID = m.TRANSACTION_ID,
                                P_NOREG = m.NOREG,
                                P_LANGUAGE_ID = m.LANGUAGE_ID,
                                P_SKILL_LEVEL = m.SKILL_LEVEL,
                                P_SCORE = m.SCORE,
                                P_CERTIFICATE_NAME = fileNames + "" + ext,
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                                P_VALID_TO = m.VALID_TO,
                                P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                                P_STATUS_CD = m.STATUS_CD,
                                P_REJECT_REASON = m.REJECT_REASON,
                                P_REMARK_1 = m.REMARK_1,
                                P_REMARK_2 = m.REMARK_2,
                                P_PK_LANGUAGE_TEST = m.PK_LANGUAGE_TEST, 
                            };
                            result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getUpdateData", args)[0];
                        }

                        //
                    }
                    else
                    {
                        dynamic args = new
                        {
                            P_TRANSACTION_ID = m.TRANSACTION_ID,
                            P_NOREG = m.NOREG,
                            P_LANGUAGE_ID = m.LANGUAGE_ID,
                            P_SKILL_LEVEL = m.SKILL_LEVEL,
                            P_SCORE = m.SCORE,
                            P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                            P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                            P_VALID_TO = m.VALID_TO,
                            P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                            P_STATUS_CD = m.STATUS_CD,
                            P_REJECT_REASON = m.REJECT_REASON,
                            P_REMARK_1 = m.REMARK_1,
                            P_REMARK_2 = m.REMARK_2,
                            P_PK_LANGUAGE_TEST = m.PK_LANGUAGE_TEST,
                        };
                        result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getUpdateData", args)[0];
                    }
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
                            fileNames = $"LN_{m.NOREG}_{timestamp}";
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
                                    P_TRANSACTION_ID = m.TRANSACTION_ID,
                                    P_NOREG = m.NOREG,
                                    P_LANGUAGE_ID = m.LANGUAGE_ID,
                                    P_SKILL_LEVEL = m.SKILL_LEVEL,
                                    P_SCORE = m.SCORE,
                                    P_CERTIFICATE_NAME = fileNames + "" + ext,
                                    P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                                    P_VALID_TO = m.VALID_TO,
                                    P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                                    P_STATUS_CD = m.STATUS_CD,
                                    P_REJECT_REASON = m.REJECT_REASON,
                                    P_REMARK_1 = m.REMARK_1,
                                    P_REMARK_2 = m.REMARK_2,
                                    P_PK_LANGUAGE_TEST = m.PK_LANGUAGE_TEST,
                                };
                                result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getUpdateData", args)[0];
                            }
                        }
                        else
                        {
                            dynamic args = new
                            {
                                P_TRANSACTION_ID = m.TRANSACTION_ID,
                                P_NOREG = m.NOREG,
                                P_LANGUAGE_ID = m.LANGUAGE_ID,
                                P_SKILL_LEVEL = m.SKILL_LEVEL,
                                P_SCORE = m.SCORE,
                                P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Language/",
                                P_VALID_TO = m.VALID_TO,
                                P_LANGUAGE_TEST = m.LANGUAGE_TEST,
                                P_STATUS_CD = m.STATUS_CD,
                                P_REJECT_REASON = m.REJECT_REASON,
                                P_REMARK_1 = m.REMARK_1,
                                P_REMARK_2 = m.REMARK_2,
                                P_PK_LANGUAGE_TEST = m.PK_LANGUAGE_TEST,
                            };
                            result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getUpdateData", args)[0];
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






        public ResultMessage DeleteData(String TRANSACTION_ID)
        {
            dynamic args = new
            { 
                P_TRANSACTION_ID = TRANSACTION_ID
            };

            ResultMessage Result = db.Fetch<ResultMessage>("PersonalInformation/LanguageSkill/getDeleteData", args)[0];
            db.Close();
            return Result;
        }


    }
}