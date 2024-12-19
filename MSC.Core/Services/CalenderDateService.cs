using AutoMapper;
using Dapper;
using MSC.Core.CRUD;
using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using MSC.Core.Repositories;
using MSC.Core.Services.Interfaces;
using MSC.Domain.Models;
using MSC.Widget.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Services
{
    public class CalenderDateService : Repository<CalenderDate, CalenderDateViewModel>,ICalenderDateService
    {
        private readonly IMapper _map;
        private readonly ICRUD<CalenderDate> _calenderCrud;
        private readonly IDapper<CalenderDate> _dapper;

        // سازنده
        public CalenderDateService(ICRUD<CalenderDate> calenderCrud, IMapper map, IDapper<CalenderDate> dapper)
            : base(calenderCrud, map) // ارسال وابستگی‌ها به سازنده پدر (Repository)
        {
            _calenderCrud = calenderCrud;
            _map = map;
            _dapper = dapper;
        }

        public void SaveChange()
        {
            _calenderCrud.Save();
        }
      
    }
}
