using System.Collections.Generic; 
using Toyota.Common.Database;
using System; 
using System.IO;
using System.Threading.Tasks; 
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ASPNETMVC3TDK.Models.EducationHistory;
using ASPNETMVC3TDK.Models.LanguageSkill;
using ASPNETMVC3TDK.Models.RotationHistory;
using ASPNETMVC3TDK.Models.ProjectAssignment;
using ASPNETMVC3TDK.Models.ICTAssigmentExternal;
using ASPNETMVC3TDK.Models.TrainingHistory;
using ASPNETMVC3TDK.Models.Eligibility;
using ASPNETMVC3TDK.Models.Achivement;
using ASPNETMVC3TDK.Models.People;
using ASPNETMVC3TDK.Models.UserProfile;
using DotNetOpenAuth.Messaging;

public class FileUploadExcelHelper
{

    /* CHECK CONDITION IS FALSE INVALID EXTENTION OR NO*/
    public static bool IsExcelFileValid(string filePath)
    {

        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Attempt to load the file as an NPOI workbook
                IWorkbook workbook = new XSSFWorkbook(fileStream);
                // If successful, assume file is valid
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error validating Excel file: {ex.Message}");
            return false;
        }
    }

    /* LOAD INSERT HEADER AND DATA*/
    private static void LoadHeadersInsertData(ISheet sheet, string[] headers, IDBContext db, String label, dynamic args, IList<People> peoples)
    {
        if (label == "Personal Data")
        {
            IList<E_PersonalData> dataPersonal = new List<E_PersonalData>();
            dataPersonal = db.Fetch<E_PersonalData>("PersonalInformation/UserProfile/getUserProfileExport", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var personel in dataPersonal)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.PERSONNEL_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.DATE_OF_BIRTH);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.CLASS);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.POSITION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.DIVISION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.DEPARTMENT);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.SECTION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.LINE);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.GROUP);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.JOIN_BY);
                row.CreateCell(newCell = newCell + 1).SetCellValue(personel.EMAIL);
            }
            //
        }
        else
        if (label == "Education")
        {
            IList<M_EducationHistoryReq> dataEducation = new List<M_EducationHistoryReq>();
            dataEducation = db.Fetch<M_EducationHistoryReq>("PersonalInformation/EducationData/getMasterEducationData", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }

            int rowCount = 1;
            foreach (var education in dataEducation)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.EDUCATION_LEVEL);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.SCHOOL_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.MAJOR_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.VALID_FROM);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.VALID_TO);
                row.CreateCell(newCell = newCell + 1).SetCellValue(education.GPA);
            }
        }
        //
        else if (label == "Language")
        {
            IList<M_LanguageSkillRepo> dataLanguage = new List<M_LanguageSkillRepo>();
            dataLanguage = db.Fetch<M_LanguageSkillRepo>("PersonalInformation/LanguageSkill/getMasterLanguage", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var language in dataLanguage)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(language.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(language.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(language.LANGUAGE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(language.LANGUAGE_TEST);
                row.CreateCell(newCell = newCell + 1).SetCellValue(language.SCORE);
                row.CreateCell(newCell = newCell + 1).SetCellValue(language.SKILL_LEVEL);
            }
        }
        //
        else if (label == "Rotation History")
        {
            IList<E_RotationHistory> dataRotation = new List<E_RotationHistory>();
            dataRotation = db.Fetch<E_RotationHistory>("CarreerHistory/RotationHistory/getMasterRotation", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var rotation in dataRotation)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.CLASS);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.POSITION_DESC);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.DIVISION_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.DEPARTMENT_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.SECTION_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.LINE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.GROUP_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.VALID_FROM);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.VALID_TO);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.RESPONSIBILITY);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.ACCOMPLISHMENT);
            }
        }
        //
        else if (label == "Project Assignment")
        {
            IList<E_ProjectAssignment> dataProject = new List<E_ProjectAssignment>();
            dataProject = db.Fetch<E_ProjectAssignment>("CarreerHistory/ProjectAssigment/getMasterProjectAssigment", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var rotation in dataProject)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.CLASS);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.POSITION_DESC);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.PROJECT_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.DIVISION_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.DEPARTMENT_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.SECTION_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.VALID_FROM);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.VALID_TO);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.RESPONSIBILITY);
                row.CreateCell(newCell = newCell + 1).SetCellValue(rotation.ACCOMPLISHMENT);
            }
        }
        //
        else if (label == "ICT Assignment to External")
        {
            IList<E_ICTAssigmentExternal> dataICT = new List<E_ICTAssigmentExternal>();
            dataICT = db.Fetch<E_ICTAssigmentExternal>("CarreerHistory/ICTAssigmentExternal/getMasterICTAssigment", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var ict in dataICT)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.HOST_POSITION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.HOST_COMPANY);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.HOST_COUNTRY);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.HOST_DIVISION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.HOST_DEPARTMENT);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.HOST_SECTION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.PERIODE_FROM);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.PERIODE_TO);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.RESPONSIBILITY);
                row.CreateCell(newCell = newCell + 1).SetCellValue(ict.ACCOMPLISHMENT);
            }
        }
        // 
        else if (label == "Functional & Voluntary Train")
        {
            IList<E_TrainingHistory> dataHistory = new List<E_TrainingHistory>();
            dataHistory = db.Fetch<E_TrainingHistory>("Training/History/getMasterHistory", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var history in dataHistory)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.INSTITUTION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.COUNTRY_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.TRAINING_TOPIC);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.YEAR);
                row.CreateCell(newCell = newCell + 1).SetCellValue(history.SKILL);
            }
        }
        //
        else if (label == "Certification & Compulsory")
        {
            IList<E_Eligibility> dataEligilibity = new List<E_Eligibility>();
            dataEligilibity = db.Fetch<E_Eligibility>("Training/Eligibility/getMasterEligilibility", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var eligibility in dataEligilibity)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.INSTITUTION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.COUNTRY_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.TRAINING_TOPIC);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.YEAR);
                row.CreateCell(newCell = newCell + 1).SetCellValue(eligibility.SKILL);
            }
        }
        //
        else if (label == "Achievement")
        {
            IList<E_Achivement> dataAchivement = new List<E_Achivement>();
            dataAchivement = db.Fetch<E_Achivement>("Training/Achivement/getMasterAchivement", args);
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }
            int rowCount = 1;
            foreach (var achivement in dataAchivement)
            {
                int newCell = 0;
                IRow row = sheet.CreateRow(rowCount++);
                row.CreateCell(newCell).SetCellValue(rowCount - 1);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.NOREG);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.EMPLOYEE_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.INSTITUTION);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.COUNTRY_NAME);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.TRAINING_TOPIC);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.YEAR);
                row.CreateCell(newCell = newCell + 1).SetCellValue(achivement.SKILL);
            }
        }

    }

    // INITIALIZATION FOR HEADER AND CALL METHOD DATA INSERT
    private static void CreateHeadersAndData(IWorkbook workbook, string[] headers, string label, IDBContext db, dynamic args, IList<People> peoples )
    {
        //
        // 0. Personal Data
        if (label == "Personal Data")
        {
            IList<People> people = peoples;
            // Sheet 1. Education History
            ISheet PersonalData = workbook.CreateSheet("0. " + label);
            IRow PdHeaderRow = PersonalData.CreateRow(0);
            LoadHeadersInsertData(PersonalData, headers, db, label, args, people);
        } 
        //
        else if (label == "Education")
        {
            // Sheet 1. Education History
            ISheet Education = workbook.CreateSheet("1. "+label);
            IRow EdHeaderRow = Education.CreateRow(0);
            LoadHeadersInsertData(Education, headers, db, label, args, null);
        }
        //
        else if(label == "Language")
        {
            // Sheet 2. Language History
            ISheet Language = workbook.CreateSheet("2. "+label);
            IRow LnHeaderRow = Language.CreateRow(0);
            LoadHeadersInsertData(Language, headers, db, label, args, null);
        }
        //
        else if(label == "Rotation History")
        {
            // Sheet 3. Rotation History
            ISheet Rotation = workbook.CreateSheet("3. "+label);
            IRow RtHeaderRow = Rotation.CreateRow(0);
            LoadHeadersInsertData(Rotation, headers, db, label, args, null);
        }
        //
        else if (label == "Project Assignment") 
        {
            // Sheet 3. Rotation History
            ISheet ProjectAssigment = workbook.CreateSheet("4. "+label);
            IRow PtHeaderRow = ProjectAssigment.CreateRow(0);
            LoadHeadersInsertData(ProjectAssigment, headers, db, label, args, null);
        }
        //
        else if (label == "ICT Assignment to External")
        {
            // Sheet 3. Rotation History
            ISheet ICT = workbook.CreateSheet("5. " + label);
            IRow IctHeaderRow = ICT.CreateRow(0);
            LoadHeadersInsertData(ICT, headers, db, label, args, null);
        }
        //
        else if (label == "Functional & Voluntary Train")
        {
            // Sheet 3. Rotation History
            ISheet History = workbook.CreateSheet("6. "+label) ;
            IRow HsHeaderRow = History.CreateRow(0);
            LoadHeadersInsertData(History, headers, db, label, args, null);
        }
        //
        else if (label == "Certification & Compulsory")
        {
            // Sheet 3. Rotation History
            ISheet Eligilibity = workbook.CreateSheet("7. "+label);
            IRow EbtHeaderRow = Eligilibity.CreateRow(0);
            LoadHeadersInsertData(Eligilibity, headers, db, label, args, null);
        }
        //
        else if (label == "Achievement")
        {
            // Sheet 3. Rotation History
            ISheet Achivement = workbook.CreateSheet("8. "+label);
            IRow AchtHeaderRow = Achivement.CreateRow(0);
            LoadHeadersInsertData(Achivement, headers, db, label, args, null);
        }
        //
    }
     
    /* ASYNC DATA PROCESS */
    public static async Task<bool> UploadExcel(dynamic args, IDBContext db, IWorkbook workbook, string filePath, IList<People> peoples)
    {
        try
        { 

            /* -- CONDITION TABS IS DEFAULT -- */
            string[] tabs = { "Personal Data", "Education", "Language", "Rotation History", "Project Assignment", 
                "ICT Assignment to External", "Functional & Voluntary Train", "Certification & Compulsory", "Achievement" };

            // Define headers all tabs
            // Education 
            string[] Pdheaders = { "No", "Noreg", "Nama", "Birth of Date", "Class", "Position", "Division", "Department", "Section", "Line", "Group", "Joined By", "Email" };
            // Education 
            string[] Edheaders = { "No", "Noreg", "Employee Name", "Education Level", "School Name", "Major Desc", "Year Started", "Year Graduated", "GPA" };
            // Language 
            string[] LnHeaders = { "No", "Noreg", "Employee Name", "Language", "Language Test", "Score", "Skill Level" };
            // Rotation
            string[] RtHeaders = { "No", "Noreg", "Employee Name", "Class", "Position", "Division", "Department", "Section", "Line", "Group", "Valid From", "Valid To", "Responsibility", "Accomplishment" };
            // Project
            string[] PjRtHeaders = { "No", "Noreg", "Employee Name", "Class", "Position", "Project Name", "Division", "Department", "Section", "Valid From", "Valid To", "Responsibility", "Accomplishment" };
            // ICT
            string[] ICTRtHeaders = { "No", "Noreg", "Employee Name", "Position", "Host Company", "Host Country", "Host Division", "Host Department", "Host Section", "Valid From",
                "Valid To", "Responsibility", "Accomplishment" };
            // History
            string[] HsHeaders = { "No", "Noreg", "Employee Name", "Institution", "Country", "Training Topic", "Year", "Skills" };
            // Eligilibity
            string[] EbtRtHeaders = { "No", "Noreg", "Employee Name", "Institution", "Country", "Training Topic",  "Year", "Skills" };
            // Achivement
            string[] AchHeaders = { "No", "Noreg", "Employee Name", "Institution", "Country", "Training Topic",  "Year", "Skills" };

            // Initialize a new workbook
            workbook = new XSSFWorkbook();

            /* -- LOOP ALL TABS AS LIKE DEFAULT TABS -- */
            for (int i = 0; i < tabs.Length; i++)
            {
                if (tabs[i] == "Personal Data") { CreateHeadersAndData(workbook, Pdheaders, tabs[i], db, args, peoples); }

                else if(tabs[i] == "Education") { CreateHeadersAndData(workbook, Edheaders, tabs[i], db, args, null); }

                else if (tabs[i] == "Language") { CreateHeadersAndData(workbook, LnHeaders, tabs[i], db, args, null); }

                else if (tabs[i] == "Rotation History") { CreateHeadersAndData(workbook, RtHeaders, tabs[i], db, args, null); }

                else if (tabs[i] == "Project Assignment") { CreateHeadersAndData(workbook, PjRtHeaders, tabs[i], db, args, null); }
                
                else if (tabs[i] == "ICT Assignment to External") { CreateHeadersAndData(workbook, ICTRtHeaders, tabs[i], db, args, null); } 

                else if(tabs[i] == "Functional & Voluntary Train") { CreateHeadersAndData(workbook, HsHeaders, tabs[i], db, args, null); }

                else if (tabs[i] == "Certification & Compulsory") { CreateHeadersAndData(workbook, EbtRtHeaders, tabs[i], db, args, null); }

                else if(tabs[i] == "Achievement") { CreateHeadersAndData(workbook, AchHeaders, tabs[i], db, args, null); } 
            }

            /*  -- define save result when all data is collected --- */
            // Save workbook to the specified file path
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }

            // Check if the generated Excel file is valid
            bool isValid = IsExcelFileValid(filePath);

            return isValid;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

}