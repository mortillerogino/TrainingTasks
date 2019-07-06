using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingUpload.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFileRepository UploadedFiles { get; }
        Task<int> CompleteAsync();
    }
}
