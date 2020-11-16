using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private DBAccess db;
        private DbSet<T> _dbSet;
        private readonly IDbContextTransaction transaction;
        internal RepositoryBase()
        {
            db = new DBAccess();
            _dbSet = db.Set<T>();
            transaction = db.Database.BeginTransaction();
        }
        public virtual void Add(T entity) => _dbSet.Add(entity);
        public virtual T Get(Expression<Func<T, bool>> where) => _dbSet.Where(where).FirstOrDefault();
        public virtual T Get(int id) => _dbSet.Find(id);
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
            {
                _dbSet.Remove(obj);
            }
        }
        public virtual IQueryable<T> AsQueryable() => _dbSet.AsNoTracking().AsQueryable();
        public virtual IEnumerable<T> GetAll() => _dbSet.ToList();
        public virtual void Commit() => transaction.Commit();
        public virtual void Save() => db.SaveChanges();
        public virtual void Rollback() => transaction.Rollback();
        public virtual void Dispose() => transaction.Dispose();
    }
}