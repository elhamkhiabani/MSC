using MSC.Core.Presentations.Base;
using MSC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.CRUD
{
    public interface ICRUD<T>  where T : class, IEntity
    {
        void Add(T entity);

        void Save();

        T GetByID(int id);

        IQueryable<T> GetAll(bool isActive);

        void Update(T entity);

        bool Remove(T entity, bool hardDelete = false);


    }
}
