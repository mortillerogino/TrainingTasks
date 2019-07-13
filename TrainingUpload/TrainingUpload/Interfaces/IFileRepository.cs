using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingUpload.Models;

namespace TrainingUpload.Interfaces
{
    public interface IFileRepository : IRepository<UploadedFileDetails>
    {

    }
}
