using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using MSC.Core.Repositories;
using MSC.Domain.Models;

namespace MSC.Core.Services.Interfaces
{
    public interface ICalenderDateService  : IRepository<CalenderDate, CalenderDateViewModel>
    {
        void SaveChange();
    }
}
