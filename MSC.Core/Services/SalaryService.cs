using AutoMapper;
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
        private readonly ICRUD<Salary> _salaryCrud;
        private readonly IDapper<Salary> _dapper;

        // سازنده
        public SalaryService(ICRUD<Salary> salaryCrud, IMapper map, IDapper<Salary> dapper)
            : base(salaryCrud, map) // ارسال وابستگی‌ها به سازنده پدر (Repository)
        {
            _salaryCrud = salaryCrud;
            _map = map;
            _dapper = dapper;
        }

        public  MessageViewModel AddOrUpdate(Salary entity,int creatorID=0)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                Expression<Func<Salary, bool>> expression= x=>x.FirstName == entity.FirstName && x.LastName == entity.LastName && x.Date==entity.Date;
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

        public ResultViewModel<SalaryViewModel> SelectByID(int id)
        {
            ResultViewModel<SalaryViewModel> result = new ResultViewModel<SalaryViewModel>();
            try
            {
                Parameter parameter = new Parameter();
                //parameter.GEt();
                var res = _dapper.CallProcdure("GetAllSalary", parameter);
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
                result.Result = _map.Map<SalaryViewModel>(res);
                result.Message = new MessageViewModel
                {
                    ID = 1,
                    Message = "SelectByID Success",
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

        public ResultViewModel<SalaryViewModel> SelectAll()
        {
            ResultViewModel<SalaryViewModel> result = new ResultViewModel<SalaryViewModel>();
            try
            {
                Parameter parameter = new Parameter();
                //parameter.GetCustomAttributes("FromDate", from);
                var res = _dapper.CallProcdure("GetAllSalary", parameter);
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
                result.List = _map.Map<List<SalaryViewModel>>(res);
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
