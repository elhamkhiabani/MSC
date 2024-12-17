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
    public class CRUD<T> : ICRUD<T> where T : class 
    {
        private readonly DatabaseContext _context;
        public CRUD(DatabaseContext context)
        {
            _context = context;
        }

        public MessageViewModel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultViewModel<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResultViewModel<T> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public MessageViewModel Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
