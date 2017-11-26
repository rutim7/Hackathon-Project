using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Data.Repositories
{
    public abstract class GenericRepository<TEntity> :IGeneralService<TEntity> where TEntity : class 
    {
        protected readonly SentimeContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected GenericRepository(SentimeContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
           
            return  DbSet.AsEnumerable();
        }
        public TEntity FindSync(object id)
        {
           return DbSet.Find(id);
        }

        public async Task<TEntity> Find(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public  void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async void RemoveById(object id)
        {
            TEntity entityToDelete = await DbSet.FindAsync(id);
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                if (entityToDelete != null) DbSet.Attach(entityToDelete);
            }
            if (entityToDelete != null) DbSet.Remove(entityToDelete);
        }

        public  void Remove(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void UpdateEntity(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        
    }
}
