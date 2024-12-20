using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
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
        ResultViewModel<SalaryViewModel> SelectByFullName(FilterViewModel entity);

        MessageViewModel AddSalary(Salary entity,int creatorID=0);

        ResultViewModel<SalaryViewModel> SelectAll(FilterViewModel entity);
        //void SaveChange();
    }
}
