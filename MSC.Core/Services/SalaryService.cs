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
    public class SalaryService : Repository<Salary, SalaryViewModel>, ISalaryService 
    {
        private readonly IMapper _map;
        private readonly IDapper<Salary> _dapper;
        private readonly IServiceProvider _service;

        // سازنده
        public SalaryService(IServiceProvider service, IMapper map, IDapper<Salary> dapper) :base(service)
        {
            _service = service;
            _map = map;
            _dapper = dapper;
        }

        //public void SaveChange()
        //{
        //    _salaryCrud.Save();
        //}

        public  MessageViewModel AddSalary(Salary entity,int creatorID=0)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                Expression<Func<Salary, bool>> expression= x=>x.FirstName == entity.FirstName && x.LastName == entity.LastName && x.Date==entity.Date && x.IsDelete==false;
                var exist = GetAll(true,expression);
                if (exist.List.Count()>0)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Duplicate",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }
                entity.IsActive = true;
                var message = Add(entity);
                result = message;
                return result;
            }
            catch (Exception ex)
            {
                result = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }

        public ResultViewModel<SalaryViewModel> SelectByFullName(FilterViewModel entity)
        {
            ResultViewModel<SalaryViewModel> result = new ResultViewModel<SalaryViewModel>();
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@FirstName",entity.FirstName);
                parameter.Add("@LastName", entity.LastName);
                parameter.Add("@FromDate", entity.FromDate);

                var res = _dapper.CallProcdure<Salary>("GetMonthlyByFullname", parameter);
                if (res == null)
                {
                    result.Message = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Error",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }
                result.List = _map.Map<List<SalaryViewModel>>(res.List);
                result.Message = new MessageViewModel
                {
                    ID = 1,
                    Message = "SelectByFullName Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }

        public ResultViewModel<SalaryViewModel> SelectAll(FilterViewModel entity)
        {
            ResultViewModel<SalaryViewModel> result = new ResultViewModel<SalaryViewModel>();
            try
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@FirstName", entity.FirstName);
                parameter.Add("@LastName", entity.LastName);
                parameter.Add("@FromDate", entity.FromDate);
                parameter.Add("@ToDate", entity.ToDate);
                parameter.Add("@PageNumber", entity.PageNumber);
                parameter.Add("@PageSize", entity.PageSize);
                var res = _dapper.CallProcdure<Salary>("GetAllSalary", parameter);
                if (res==null)
                {
                    result.Message = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Error",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }
                result.List = _map.Map<List<SalaryViewModel>>(res.List);
                result.Message = new MessageViewModel
                {
                    ID = 1,
                    Message = "SelectAll Success",
                    Status = "Success",
                    Value = ""
                };
                return result;
            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
        }
    }
}
