using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Handlers.Intefaces
{
    public interface IMonthSalaryCalculateHandler
    {
        MessageViewModel Add(RequestSalaryViewModel entity, string dataType);
        MessageViewModel DeleteSalaryForMonth(FilterViewModel entity, int creatorId = 0, bool hardDelete = false);
    }
}
