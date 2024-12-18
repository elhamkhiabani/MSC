using AutoMapper;
using MSC.Core.CRUD;
using MSC.Core.Presentations;
using MSC.Core.Repositories;
using MSC.Core.Services.Interfaces;
using MSC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Services
{
    public class SalaryService : Repository<Salary, SalaryViewModel>, ISalaryService 
    {
        private readonly IMapper _map;
        private readonly ICRUD<Salary> _salaryCrud;

        // سازنده
        public SalaryService(ICRUD<Salary> salaryCrud, IMapper map)
            : base(salaryCrud, map) // ارسال وابستگی‌ها به سازنده پدر (Repository)
        {
            _salaryCrud = salaryCrud;
            _map = map;
        }
    }
}
