using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Toyota.Common.App_Start
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
          
            var featureFolderViewLocationFormats = FeatureFolders().ToArray();
            ViewLocationFormats =
              ViewLocationFormats.Union(featureFolderViewLocationFormats).ToArray();
            MasterLocationFormats =
              MasterLocationFormats.Union(featureFolderViewLocationFormats).ToArray();
            PartialViewLocationFormats =
              PartialViewLocationFormats.Union(featureFolderViewLocationFormats).ToArray();
        }

        private IEnumerable<string> FeatureFolders()
        {
            List<string> views = new List<string>();
           var rootPath = "~/Views";
            var rootFolder = HostingEnvironment.MapPath(rootPath);
            if (rootFolder == null)
            {
                return Enumerable.Empty<string>();
            }

            FeatureFolders(rootPath, views);

            return views;
           
        }

        private  void FeatureFolders(string rootPath, List<string> views)
        {
            var rootFolder = HostingEnvironment.MapPath(rootPath);
            var subFolders = Directory.GetDirectories(rootFolder).Select(GetDirectoryName);

            foreach (string dir in subFolders)
            {
                Console.WriteLine($"{rootPath}/{dir}/{{0}}.cshtml");
                Console.WriteLine($"{rootPath}/{dir}/{{1}}/{{0}}.cshtml");
               // views.Add($"{rootPath}/{dir}/{{0}}.cshtml"); 
                views.Add($"{rootPath}/{dir}/{{1}}/{{0}}.cshtml");

                FeatureFolders($"{rootPath}/{dir}", views);
            }

        }

        private string GetDirectoryName(string path)
        {
            return new DirectoryInfo(path).Name;
        }
    }
}