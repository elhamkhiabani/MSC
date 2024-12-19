using AutoMapper;
using MSC.Core.Presentations;
using MSC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Mapper
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Salary, SalaryViewModel>();

            CreateMap<SalaryViewModel,Salary>();

            CreateMap<CalenderDate, CalenderDateViewModel>();
        }
    }
}
