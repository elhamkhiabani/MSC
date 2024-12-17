using MSC.Core.Presentations.Base;
using MSC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.CRUD
{
    public interface ICRUD<T>  where T : class
    {
        void Save();

        ResultViewModel<T> GetByID(int id);

        ResultViewModel<T> GetAll();

        MessageViewModel Update(T entity);

        MessageViewModel Delete(int id);


    }
}
