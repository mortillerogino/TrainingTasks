using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrainingUpload.Models;

namespace TrainingUpload.Utilities
{
    public static class FileHandler
    {
        public static UploadedFileDetails SaveFile(IFormFile file)
        {
            if (file.Length < 1)
            {
                return null;
            }

            string fileName = GetFilename(file);
            string fullPath = GetFilepath(fileName);

            CopyFile(file, fullPath);

            return CreateDetails(fileName, fullPath); ;
        }

        private static void CopyFile(IFormFile file, string fullPath)
        {
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        private static UploadedFileDetails CreateDetails(string fileName, string fullPath)
        {
            return new UploadedFileDetails
            {
                Name = fileName,
                Path = fullPath
            };
        }

        private static string GetFilename(IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        }

        private static string GetFilepath(string fileName)
        {
            string path = "UploadedFiles";
            Directory.CreateDirectory(path);
            return Path.Combine(path, fileName);
        }
    }
}
