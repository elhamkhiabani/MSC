using Microsoft.EntityFrameworkCore;
using MSC.Core.Presentations.Base;
using MSC.Data.DatabseContext;
using MSC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.CRUD
{
    public class CRUD<T> : ICRUD<T> where T : class, IEntity
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;
        public CRUD(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public bool Remove(T entity, bool hardDelete = false)
        {
            if (hardDelete == true)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                entity.IsDelete = true;
                entity.IsActive = false;
                Update(entity);
            }
            return true;
        }

    



    public T GetByID(int id)
    {
        return _dbSet.Find(id);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        entity.ModifierDateTime = DateTime.Now;
        _dbSet.Update(entity);
    }
}
}
