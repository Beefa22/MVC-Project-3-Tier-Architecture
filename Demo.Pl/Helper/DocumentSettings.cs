﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.Pl.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1-Get Located Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files",folderName);

            //2-Get File Name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3-Get File Path => File Path + File Name
            string filePath = Path.Combine(folderPath, fileName);

            //4-Save file as Stream [Data Per Time]

            using var fs = new FileStream(filePath,FileMode.Create) ;

            file.CopyTo(fs);

            return fileName;

        }

        public static void DeleteFile(string fileName, string folderName)
        {
            if (fileName is not null && folderName is not null) 
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            
        }
    }
}
