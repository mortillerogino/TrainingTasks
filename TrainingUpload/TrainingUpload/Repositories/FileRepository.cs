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

        

        public UploadedFileContext UploadedFileContext
        {
            get { return Context as UploadedFileContext; }
        }
    }
}
