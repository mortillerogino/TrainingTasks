using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrainingUpload.Interfaces;
using TrainingUpload.Models;

namespace TrainingUpload.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public void Remove(TEntity entity)
        {
            TEntity existing = dbSet.SingleOrDefault(t => t == entity);

            if (existing != null)
                dbSet.Remove(existing);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.SingleOrDefault(predicate);
        }
    }
}
