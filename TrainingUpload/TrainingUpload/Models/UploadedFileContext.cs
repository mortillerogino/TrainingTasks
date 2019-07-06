using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingUpload.Models
{
    public class UploadedFileContext : DbContext 
    {
        public UploadedFileContext(DbContextOptions<UploadedFileContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UploadedFileDetails> UploadedFileDetailsList { get; set; }

    }
}
