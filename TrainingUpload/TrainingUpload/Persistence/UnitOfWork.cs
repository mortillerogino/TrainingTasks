using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingUpload.Interfaces;
using TrainingUpload.Models;
using TrainingUpload.Repositories;

namespace TrainingUpload.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UploadedFileContext _context;

        public UnitOfWork(UploadedFileContext context)
        {
            _context = context;
            UploadedFiles = new FileRepository(_context);
        }

        public IFileRepository UploadedFiles { get; } 

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
