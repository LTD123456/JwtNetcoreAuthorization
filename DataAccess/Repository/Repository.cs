using DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataAccessDbContext _context;
        private DbSet<T> _entities;
        string errMessage = string.Empty;
        public Repository(DataAccessDbContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }

        public void Delete(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
            if (saveChange)
                _context.SaveChanges();
        }

        public T Get(int id, bool isActive = true)
        {
            return _entities.FirstOrDefault<T>(x => x.Id == id && (x.Active || !isActive));
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public void Insert(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            entity.CreatedTime = DateTime.Now;
            entity.UpdatedTime = DateTime.Now;
            _entities.Add(entity);
            if (saveChange)
                _context.SaveChanges();
        }

        public void Update(T entity, bool saveChange = true)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }
            entity.UpdatedTime = DateTime.Now;
            _entities.Update(entity);
            if (saveChange)
                _context.SaveChanges();
        }
    }
}
