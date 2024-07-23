using ASPNETMVC3TDK.Models.UploadFileResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;

public class FileUploadHelper
{
    public static async Task<bool> Upload(IEnumerable<HttpPostedFileBase> files, string folderPath, string newFileName)
    {
        try
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.ContentLength > 0) // Check if file is not empty
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var ext = Path.GetExtension(fileName);

                        if (ext != null && (ext.ToLower() == ".pdf" || ext.ToLower() == ".png" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".jpg" || ext.ToLower() == ".JPEG"))
                        {
                            var newFileNameWithPath = Path.Combine(folderPath, newFileName+ext);
                            file.SaveAs(newFileNameWithPath);
                        } 
                        else
                        {
                            Console.WriteLine($"Error: Unsupported file format '{ext}'");
                            return false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: No files to upload.");
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred in Upload method: {e.Message}");
            return false;
        }
    }
}