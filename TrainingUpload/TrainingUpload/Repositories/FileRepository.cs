using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrainingUpload.Interfaces;
using TrainingUpload.Models;

namespace TrainingUpload.Repositories
{
    public class FileRepository : Repository<UploadedFileDetails>, IFileRepository
    {
        public FileRepository(UploadedFileContext context)
            : base (context)
        {
        }

        public UploadedFileDetails SaveFile(IFormFile file)
        {
            if (file.Length < 1)
            {
                return null;
            }

            string fileName = "";
            string path = "UploadedFiles";
            Directory.CreateDirectory(path);

            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            UploadedFileDetails details = new UploadedFileDetails
            {
                Name = fileName,
                Path = fullPath
            };

            return details;
        }

        public UploadedFileContext UploadedFileContext
        {
            get { return Context as UploadedFileContext; }
        }
    }
}
