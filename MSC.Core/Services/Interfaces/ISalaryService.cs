using MSC.Core.Presentations;
using MSC.Core.Repositories;
using MSC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Services.Interfaces
{
    public interface ISalaryService : IRepository<Salary, SalaryViewModel>
    {
    }
}
