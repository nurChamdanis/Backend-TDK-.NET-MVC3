using System.Collections.Generic;
using Toyota.Common.Web.Platform;
using Toyota.Common.Database;
using System.Web.Mvc;
using System.Data.Common.CommandTrees;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO; 
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace ASPNETMVC3TDK.Models.EducationHistory
{
    public class EducationHistoryRepo
    {
    
        #region Singleton

        private static EducationHistoryRepo instance = null; 
        IDBContext db = DatabaseManager.Instance.GetContext(); 

        public static EducationHistoryRepo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EducationHistoryRepo();
                }
                return instance;
            }

        }
        #endregion

         
        public String GetProjectDirectory()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string projectDirectory = Path.GetDirectoryName(assemblyLocation);

            Console.WriteLine($"Project Directory: {projectDirectory}");
            return projectDirectory;
        }

        public IList<EducationHistory> getEducationHistory (string NOREG, bool MODEMASTER )
        {
            IList<EducationHistory> Result = new List<EducationHistory>();

            dynamic args = new
            {
                P_NOREG = NOREG
            };

            if (MODEMASTER) {
                Result = db.Fetch<EducationHistory>("PersonalInformation/EducationData/getMasterEducationData", args);
            } else {
                Result = db.Fetch<EducationHistory>("PersonalInformation/EducationData/getEducationData", args);
            }

            db.GetSqlLoaders();
            db.Close();
            return Result;
        }

        public async Task<ResultMessage> InsertData(M_EducationHistoryReq m)
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
                string contentFolderPath = Path.Combine(personalInformationFolderPath, "Education");

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
                        fileNames = $"ED_{m.NOREG}_{timestamp}";
                        IEnumerable<HttpPostedFileBase> files = m.CERTIFICATE;
                        string ext = null;
                        string filenames = null;
                        foreach (var file in files)
                        {
                            filenames = Path.GetFileName(file.FileName);
                            // Use fileName as needed
                        } 
                        ext = Path.GetExtension(filenames); 

                        // Upload the file to specified folder
                        bool uploadFile = await FileUploadHelper.Upload(m.CERTIFICATE, contentFolderPath, fileNames);

                        // Check if upload was successful
                        if (uploadFile)
                        {
                            dynamic args = new
                            {
                                P_NOREG = m.NOREG,
                                P_EDUCATION_CD = m.EDUCATION_CD,
                                P_MAJOR_CD = m.MAJOR_CD,
                                P_GPA = m.GPA,
                                P_SCHOOL_NAME = m.SCHOOL_NAME,
                                P_COUNTRY_ID = m.COUNTRY_ID,
                                P_VALID_FROM = m.VALID_FROM,
                                P_VALID_TO = m.VALID_TO,
                                P_CERTIFICATE = m.CERTIFICATE,
                                P_CERTIFICATE_NAME = fileNames+""+ext, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/", // Path where file is uploaded
                                P_REMARK_1 = m.REMARK_1,
                            };

                            // Perform database update operation
                            result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getSaveData", args)[0];
                        }

                        //
                    }
                    else
                    {
                        dynamic args = new
                        {
                            P_NOREG = m.NOREG,
                            P_EDUCATION_CD = m.EDUCATION_CD,
                            P_MAJOR_CD = m.MAJOR_CD,
                            P_GPA = m.GPA,
                            P_SCHOOL_NAME = m.SCHOOL_NAME,
                            P_COUNTRY_ID = m.COUNTRY_ID,
                            P_VALID_FROM = m.VALID_FROM,
                            P_VALID_TO = m.VALID_TO,
                            P_CERTIFICATE = m.CERTIFICATE,
                            P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                            P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/",
                            P_REMARK_1 = m.REMARK_1,
                        };

                        // Perform database update operation
                        result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getSaveData", args)[0];

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
                            fileNames = $"ED_{m.NOREG}_{timestamp}";
                            IEnumerable<HttpPostedFileBase> files = m.CERTIFICATE;
                            string ext = null;
                            string filenames = null;
                            foreach (var file in files)
                            {
                                filenames = Path.GetFileName(file.FileName);
                                // Use fileName as needed
                            }
                            ext = Path.GetExtension(filenames);

                            // Upload the file to specified folder
                            bool uploadFile = await FileUploadHelper.Upload(m.CERTIFICATE, contentFolderPath, fileNames);

                            // Check if upload was successful
                            if (uploadFile)
                            {
                                dynamic args = new
                                {
                                    P_NOREG = m.NOREG,
                                    P_EDUCATION_CD = m.EDUCATION_CD,
                                    P_MAJOR_CD = m.MAJOR_CD,
                                    P_GPA = m.GPA,
                                    P_SCHOOL_NAME = m.SCHOOL_NAME,
                                    P_COUNTRY_ID = m.COUNTRY_ID,
                                    P_VALID_FROM = m.VALID_FROM,
                                    P_VALID_TO = m.VALID_TO,
                                    P_CERTIFICATE = m.CERTIFICATE,
                                    P_CERTIFICATE_NAME = fileNames+""+ext, // Use custom file name here
                                    P_CERTIFICATE_PATH = "Content/Document/PersonalInformation/Education/",// Path where file is uploaded
                                    P_REMARK_1 = m.REMARK_1,
                                };

                                // Perform database update operation
                                result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getSaveData", args)[0];
                            }

                            //
                        } 
                        else {
                            dynamic args = new
                            {
                                P_NOREG = m.NOREG,
                                P_EDUCATION_CD = m.EDUCATION_CD,
                                P_MAJOR_CD = m.MAJOR_CD,
                                P_GPA = m.GPA,
                                P_SCHOOL_NAME = m.SCHOOL_NAME,
                                P_COUNTRY_ID = m.COUNTRY_ID,
                                P_VALID_FROM = m.VALID_FROM,
                                P_VALID_TO = m.VALID_TO,
                                P_CERTIFICATE = m.CERTIFICATE,
                                P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/",
                                P_REMARK_1 = m.REMARK_1,
                            };

                            // Perform database update operation
                            result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getSaveData", args)[0];

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


        public async Task<ResultMessage> UpdateData(M_EducationHistoryReq m)
        {
            ResultMessage result = new ResultMessage();

            try
            {
                // Get base folder path for personal information document s
                var personalInformationFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Document/PersonalInformation");

                // Check if base folder path is null
                if (personalInformationFolderPath == null)
                {
                    throw new InvalidOperationException("Failed to resolve base folder path for personal information documents.");
                }

                // Combine base folder path with "Education" folder
                string contentFolderPath = Path.Combine(personalInformationFolderPath, "Education");

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
                        fileNames = $"ED_{m.NOREG}_{timestamp}";
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
                            // Construct arguments for database update
                            dynamic args = new
                            {
                                P_TRANSACTION_ID = m.TRANSACTION_ID,
                                P_NOREG = m.NOREG,
                                P_EDUCATION_CD = m.EDUCATION_CD,
                                P_MAJOR_CD = m.MAJOR_CD,
                                P_GPA = m.GPA,
                                P_SCHOOL_NAME = m.SCHOOL_NAME,
                                P_COUNTRY_ID = m.COUNTRY_ID,
                                P_VALID_FROM = m.VALID_FROM,
                                P_VALID_TO = m.VALID_TO,
                                P_CERTIFICATE = m.CERTIFICATE,
                                P_CERTIFICATE_NAME = fileNames+""+ext, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/", // Path where file is uploaded
                                P_REMARK_1 = m.REMARK_1,
                                P_PK_EDUCATION_CD = m.PK_EDUCATION_CD,
                                P_PK_MAJOR_CD = m.PK_MAJOR_CD,
                            };

                            // Perform database update operation
                            result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getUpdateData", args)[0];
                        }
                    }
                    else
                    {
                        // Construct arguments for database update
                        dynamic args = new
                        {
                            P_TRANSACTION_ID = m.TRANSACTION_ID,
                            P_NOREG = m.NOREG,
                            P_EDUCATION_CD = m.EDUCATION_CD,
                            P_MAJOR_CD = m.MAJOR_CD,
                            P_GPA = m.GPA,
                            P_SCHOOL_NAME = m.SCHOOL_NAME,
                            P_COUNTRY_ID = m.COUNTRY_ID,
                            P_VALID_FROM = m.VALID_FROM,
                            P_VALID_TO = m.VALID_TO,
                            P_CERTIFICATE = m.CERTIFICATE,
                            P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                            P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/",
                            P_REMARK_1 = m.REMARK_1,
                            P_PK_EDUCATION_CD = m.PK_EDUCATION_CD,
                            P_PK_MAJOR_CD = m.PK_MAJOR_CD
                        };

                        // Perform database update operation
                        result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getUpdateData", args)[0];
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
                            fileNames = $"ED_{m.NOREG}_{timestamp}";
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
                                // Construct arguments for database update
                                dynamic args = new
                                {
                                    P_TRANSACTION_ID = m.TRANSACTION_ID,
                                    P_NOREG = m.NOREG,
                                    P_EDUCATION_CD = m.EDUCATION_CD,
                                    P_MAJOR_CD = m.MAJOR_CD,
                                    P_GPA = m.GPA,
                                    P_SCHOOL_NAME = m.SCHOOL_NAME,
                                    P_COUNTRY_ID = m.COUNTRY_ID,
                                    P_VALID_FROM = m.VALID_FROM,
                                    P_VALID_TO = m.VALID_TO,
                                    P_CERTIFICATE = m.CERTIFICATE,
                                    P_CERTIFICATE_NAME = fileNames+""+ext, // Use custom file name here
                                    P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/", // Path where file is uploaded
                                    P_REMARK_1 = m.REMARK_1,
                                    P_PK_EDUCATION_CD = m.PK_EDUCATION_CD,
                                    P_PK_MAJOR_CD = m.PK_MAJOR_CD,
                                };

                                // Perform database update operation
                                result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getUpdateData", args)[0];
                            }
                        }
                        else
                        {
                            // Construct arguments for database update
                            dynamic args = new
                            {
                                P_TRANSACTION_ID = m.TRANSACTION_ID,
                                P_NOREG = m.NOREG,
                                P_EDUCATION_CD = m.EDUCATION_CD,
                                P_MAJOR_CD = m.MAJOR_CD,
                                P_GPA = m.GPA,
                                P_SCHOOL_NAME = m.SCHOOL_NAME,
                                P_COUNTRY_ID = m.COUNTRY_ID,
                                P_VALID_FROM = m.VALID_FROM,
                                P_VALID_TO = m.VALID_TO,
                                P_CERTIFICATE = m.CERTIFICATE,
                                P_CERTIFICATE_NAME = m.CERTIFICATE_NAME, // Use custom file name here
                                P_CERTIFICATE_PATH = "/Content/Document/PersonalInformation/Education/",
                                P_REMARK_1 = m.REMARK_1,
                                P_PK_EDUCATION_CD = m.PK_EDUCATION_CD,
                                P_PK_MAJOR_CD = m.PK_MAJOR_CD,
                            };

                            // Perform database update operation
                            result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getUpdateData", args)[0];
                        }
                        //
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating folder: {ex.Message}");
                    }
                }
            }
            finally
            {
                db.Close(); // Ensure database connection is closed
            }

            return result; 
        }


        public ResultMessage DeleteData(EducationHistory m)
        {
            dynamic args = new
            {
                P_TRANSACTION_ID = m.TRANSACTION_ID
            };
            ResultMessage Result = db.Fetch<ResultMessage>("PersonalInformation/EducationData/getDeleteData", args)[0];
            db.Close();
            return Result; 
        }


    }
}