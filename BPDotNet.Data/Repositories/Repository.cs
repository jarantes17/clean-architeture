using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BPDotNet.Core.Entities.Base;
using BPDotNet.Core.Interfaces.Repositories;
using BPDotNet.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BPDotNet.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Properties

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EfContext Context;

        // ReSharper disable once MemberCanBePrivate.Global
        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

        #endregion

        protected Repository(EfContext context)
        {
            Context = context;
        }

        #region Methods: Create/Update/Remove/Save

        public TEntity Create(TEntity model)
        {
            DbSet.Add(model);
            Save();
            return model;
        }

        public List<TEntity> Create(List<TEntity> models)
        {
            DbSet.AddRange(models);
            Save();
            return models;
        }

        public bool Update(TEntity model)
        {
            EntityEntry<TEntity> entry = NewMethod(model);

            DbSet.Attach(model);

            entry.State = EntityState.Modified;

            return Save() > 0;
        }

        private EntityEntry<TEntity> NewMethod(TEntity model)
        {
            return Context.Entry(model);
        }

        public bool Update(List<TEntity> models)
        {
            foreach (TEntity register in models)
            {
                EntityEntry<TEntity> entry = Context.Entry(register);
                DbSet.Attach(register);
                entry.State = EntityState.Modified;
            }

            return Save() > 0;
        }

        public bool Delete(TEntity model)
        {
            if (model is Entity entity)
            {
                entity.IsDeleted = true;
                EntityEntry<TEntity> entry = Context.Entry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;
            }
            else
            {
                EntityEntry<TEntity> entry = Context.Entry(model);
                DbSet.Attach(model);
                entry.State = EntityState.Deleted;
            }

            return Save() > 0;
        }

        public bool Delete(params object[] keys)
        {
            TEntity model = DbSet.Find(keys);
            return (model != null) && Delete(model);
        }

        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            TEntity model = DbSet.Where(where).FirstOrDefault();

            return (model != null) && Delete(model);
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        #endregion

        #region Methods: Search

        public TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().FirstOrDefault(where);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, object> includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (includes != null)
                query = includes(query) as IQueryable<TEntity>;

            return query?.SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, object> includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (includes != null)
                query = includes(query) as IQueryable<TEntity>;

            return query?.Where(predicate).AsQueryable();
        }

        #endregion

        #region Assyncronous Methods

        public async Task<TEntity> CreateAsync(TEntity model)
        {
            DbSet.Add(model);
            await SaveAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            EntityEntry<TEntity> entry = Context.Entry(model);

            DbSet.Attach(model);

            entry.State = EntityState.Modified;

            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            EntityEntry<TEntity> entry = Context.Entry(model);

            DbSet.Attach(model);

            entry.State = EntityState.Deleted;

            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(params object[] keys)
        {
            TEntity model = await DbSet.FindAsync(keys);
            return (model != null) && await DeleteAsync(model);
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            TEntity model = DbSet.FirstOrDefault(where);

            return (model != null) && await DeleteAsync(model);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        #endregion

        #region Search Methods Async

        public async Task<TEntity> GetAsync(params object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(where);
        }

        #endregion


        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}