using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingUpload.Models;

namespace TrainingUpload.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UploadedFileDetails> FileRepository { get; }

        void Commit();
    }
}
