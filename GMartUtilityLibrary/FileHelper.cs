using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Linq;


namespace GMartUtilityLibrary
{
    public static class FileHelper
    {
       
       
        public static string GetUniqueFileName(IFormFile uploadedFile, string webrootPath,string typeName )
        {
            string uniqueFileName = null;

            if (uploadedFile != null)
            {
                string uploadToFolder = GetImagePath(webrootPath, typeName);
                //get name of file from uploaded file
                string filename = Path.GetFileName(uploadedFile.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + filename;
                string filePath = Path.Combine(uploadToFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(fileStream);//copy to wwwroot
                }
            }
            return uniqueFileName;
        }
        public static string GetImagePath(string webrootPath,string typeName)
        {
            return Path.Combine(webrootPath, "images", typeName);
        }

        public static string GetImageFullPathForImage(string webrootPath, string typeName,string imageName)
        {
            string fullImagePath;
            fullImagePath = Path.Combine(GetImagePath(webrootPath, typeName), imageName);
            return fullImagePath;
        }


    }
}
