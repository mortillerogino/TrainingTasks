using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingUpload.Implementations;
using TrainingUpload.Interfaces;
using TrainingUpload.Models;

namespace TrainingUpload.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private UploadedFileContext context;
        private IRepository<UploadedFileDetails> fileRepository;

        public IRepository<UploadedFileDetails> FileRepository
        {
            get
            {
                return this.fileRepository ?? new Repository<UploadedFileDetails>(context);
            }
        }


        public UnitOfWork(UploadedFileContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
