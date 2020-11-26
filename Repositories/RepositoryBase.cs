using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DataAccess;
using Dotnet_Core_Scaffolding_Oracle.Models;

namespace Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private ModelContext db;
        private DbSet<T> _dbSet;
        private readonly IDbContextTransaction transaction;
        internal RepositoryBase()
        {
            db = new ModelContext();
            _dbSet = db.Set<T>();
            transaction = db.Database.BeginTransaction();
        }
        public virtual void Add(T entity) => _dbSet.Add(entity);
        public virtual T Get(Expression<Func<T, bool>> where) => _dbSet.Where(where).FirstOrDefault();
        public virtual T Get(int id) => _dbSet.Find(id);
        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
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
        public virtual void Commit()
        {
            transaction.Commit();
            Dispose();
        }
        public virtual void Save() => db.SaveChanges();
        public virtual void Rollback()
        {
            transaction.Rollback();
            Dispose();
        }
        public virtual void Dispose()
        {
            db.Dispose();
            transaction.Dispose();
        }
    }
}