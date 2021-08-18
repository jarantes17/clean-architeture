using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BPDotNet.Core.Entities.Base;
using BPDotNet.Core.Interfaces.Repositories;
using BPDotNet.Data.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BPDotNet.Data.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Properties

        protected readonly EfContext _context;

        protected DbSet<TEntity> DbSet
        {
            get { return _context.Set<TEntity>(); }
        }

        #endregion

        public Repository(EfContext context)
        {
            _context = context;
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
            return _context.Entry(model);
        }

        public bool Update(List<TEntity> models)
        {
            foreach (TEntity register in models)
            {
                EntityEntry<TEntity> entry = _context.Entry(register);
                DbSet.Attach(register);
                entry.State = EntityState.Modified;
            }

            return Save() > 0;
        }

        public bool Delete(TEntity model)
        {
            if (model is Entity)
            {
                (model as Entity).IsDeleted = true;
                EntityEntry<TEntity> _entry = _context.Entry(model);

                DbSet.Attach(model);

                _entry.State = EntityState.Modified;
            }
            else
            {
                EntityEntry<TEntity> _entry = _context.Entry(model);
                DbSet.Attach(model);
                _entry.State = EntityState.Deleted;
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
            TEntity model = DbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();

            return (model != null) && Delete(model);
        }

        public int Save()
        {
            return _context.SaveChanges();
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
            IQueryable<TEntity> _query = DbSet;

            if (includes != null)
                _query = includes(_query) as IQueryable<TEntity>;

            return _query.SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, object> includes)
        {
            IQueryable<TEntity> _query = DbSet;

            if (includes != null)
                _query = includes(_query) as IQueryable<TEntity>;

            return _query.Where(predicate).AsQueryable();
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
            EntityEntry<TEntity> entry = _context.Entry(model);

            DbSet.Attach(model);

            entry.State = EntityState.Modified;

            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            EntityEntry<TEntity> entry = _context.Entry(model);

            DbSet.Attach(model);

            entry.State = EntityState.Deleted;

            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(params object[] Keys)
        {
            TEntity model = DbSet.Find(Keys);
            return (model != null) && await DeleteAsync(model);
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            TEntity model = DbSet.FirstOrDefault(where);

            return (model != null) && await DeleteAsync(model);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
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
            if (_context != null)
                _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}