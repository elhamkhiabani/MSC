using AutoMapper;
using MSC.Core.Handlers.Intefaces;
using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using MSC.Core.Services.Interfaces;
using MSC.Domain.Models;
using MSC.Widget.InputProcessor;
using OverTimePolices;
using OverTimePolices.Services;
using OverTimePolices.Services.Interfces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Handlers
{
    public class MonthSalaryCalculateHandler : IMonthSalaryCalculateHandler
    {
        private readonly ISalaryService _salaryService;
        private readonly ICalenderDateService _clenderService;
        private readonly IInputProcessor<SalaryViewModel> _processor;
        private readonly IMapper _map;
        public MonthSalaryCalculateHandler(ISalaryService salaryService, ICalenderDateService clenderService, IInputProcessor<SalaryViewModel> processor, IMapper map)
        {
            _salaryService = salaryService;
            _clenderService = clenderService;
            _processor = processor;
            _map = map;

        }

        public MessageViewModel UpdateForMonth(UpdateViewModel entity, int creatorId = 0)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                if (entity.Allowance == null && entity.Transportation == null && entity.OverTimeCalculatorMethod == null && entity.BasicSalary == null)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Please Enter changes",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }

                var calender = _clenderService.GetAll(true, x => x.Month == entity.Month).List.FirstOrDefault();
                var salaries = _salaryService.GetAll(true, x => x.FirstName == entity.FirstName && x.LastName == entity.LastName && Convert.ToInt32(x.Date.Substring(5, 2)) == calender.NumberOfMonth);
                if (salaries.List.Count == 0)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Not Found",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }

                var message = DeleteBatch(salaries.List);
                string value = "";
                if (message.Status == "Success")
                {
                    foreach (var salary in salaries.List)
                    {
                        if (entity.Transportation != null)
                        {
                            salary.Transportation = entity.Transportation??0;
                        }
                        if (entity.Allowance != null)
                        {
                            salary.Allowance= entity.Allowance ?? 0;

                        }
                        if (entity.BasicSalary != null)
                        {
                            salary.BasicSalary = entity.BasicSalary??0 ;
                        }

                        if (entity.OverTimeCalculatorMethod != null)
                        {
                            salary.OverTimeCalculatorMethod= entity.OverTimeCalculatorMethod?? "";
                        }

                        salary.ID = 0;
                        var addMessage=Add(salary);

                        if (addMessage.Status=="Error")
                        {
                            value = addMessage.Message + "|";
                        }

                    }

                }
                result = new MessageViewModel
                {
                    ID = 0,
                    Message = "Update Success",
                    Status = "Success",
                    Value = ""
                };
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


        private MessageViewModel DeleteBatch(List<SalaryViewModel> entity, int creatorId = 0, bool hardDelete = false)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {

                foreach (var salary in entity)
                {
                    if (hardDelete)
                    {
                        _salaryService.Delete(salary.ID, creatorId);

                        result = new MessageViewModel
                        {
                            ID = salary.ID,
                            Message = "Hard Delete Success",
                            Status = "Success",
                            Value = ""
                        };
                        return result;
                    }

                    _salaryService.Delete(salary.ID);

                }
                result = new MessageViewModel
                {
                    ID = 0,
                    Message = "Soft Delete Success",
                    Status = "Success",
                    Value = ""
                };

                _salaryService.SaveChange();
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


        public MessageViewModel DeleteSalaryForMonth(FilterViewModel entity, int creatorId = 0, bool hardDelete = false)
        {
            MessageViewModel result = new MessageViewModel();
            try
            {
                //var date = Convert.ToInt32(entity.FromDate.Replace("/", ""));
                //var calender = _clenderService.GetByID(date).Result;
                var calender = _clenderService.GetAll(true, x => x.Month == entity.FromDate).List.FirstOrDefault();
                var salaries = _salaryService.GetAll(true, x => x.FirstName == entity.FirstName && x.LastName == entity.LastName && Convert.ToInt32(x.Date.Substring(5, 2)) == calender.NumberOfMonth);
                if (salaries.List.Count == 0)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Not Found",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }
                foreach (var salary in salaries.List)
                {
                    if (hardDelete)
                    {
                        _salaryService.Delete(salary.ID, creatorId);

                        result = new MessageViewModel
                        {
                            ID = salary.ID,
                            Message = "Hard Delete Success",
                            Status = "Success",
                            Value = ""
                        };
                        return result;
                    }

                    _salaryService.Delete(salary.ID);

                }
                result = new MessageViewModel
                {
                    ID = 0,
                    Message = "Soft Delete Success",
                    Status = "Success",
                    Value = ""
                };

                _salaryService.SaveChange();
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


        public MessageViewModel Add(SalaryViewModel entity)
        {
            MessageViewModel result = new MessageViewModel();
            OverTimeCalculatorService overTime;
            try
            {
                switch (entity.OverTimeCalculatorMethod)
                {
                    case "CalculatorA":
                        overTime = new OverTimeCalculatorService(new CalculatorA());
                        break;
                    case "CalculatorB":
                        overTime = new OverTimeCalculatorService(new CalculatorB());
                        break;
                    case "CalculatorC":
                        overTime = new OverTimeCalculatorService(new CalculatorC());
                        break;
                    default:
                        overTime = new OverTimeCalculatorService(new CalculatorA());
                        break;

                }

                

                OverTimePolices.Presentation.EntityViewModel salaryCal = new OverTimePolices.Presentation.EntityViewModel
                {
                    Allowance = entity.Allowance,
                    BasicSalary = entity.BasicSalary,
                    Transportation = entity.Transportation
                };
                entity.SalaryAmount = overTime.OverTimeCalculator(salaryCal).Result;
                var salary = _map.Map<Salary>(entity);
                DateTime date = DateTime.Now;
                salary.Date =entity.Date; ;
                salary.CalenderDateID = Convert.ToInt32(salary.Date.Replace("/", ""));
                salary.Time = date.ToShortTimeString();
                var saveMessage = _salaryService.AddSalary(salary);
                if (saveMessage.Status == "Success")
                {
                    result = saveMessage;
                }
                else
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Error In Save Salary",
                        Status = "Error",
                        Value = saveMessage.Message
                    };
                }

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


        public MessageViewModel Add(RequestSalaryViewModel entity, string dataType)
        {
            MessageViewModel result = new MessageViewModel();
            OverTimeCalculatorService overTime;
            try
            {
                var data = _processor.Process(dataType, entity.Data);
                switch (entity.OverTimeCalculator)
                {
                    case "CalculatorA":
                        overTime = new OverTimeCalculatorService(new CalculatorA());
                        break;
                    case "CalculatorB":
                        overTime = new OverTimeCalculatorService(new CalculatorB());
                        break;
                    case "CalculatorC":
                        overTime = new OverTimeCalculatorService(new CalculatorC());
                        break;
                    default:
                        overTime = new OverTimeCalculatorService(new CalculatorA());
                        break;

                }

                if (data == null)
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Input is Null",
                        Status = "Error",
                        Value = ""
                    };
                    return result;
                }

                OverTimePolices.Presentation.EntityViewModel salaryCal = new OverTimePolices.Presentation.EntityViewModel
                {
                    Allowance = data.Result.Allowance,
                    BasicSalary = data.Result.BasicSalary,
                    Transportation = data.Result.Transportation
                };
                data.Result.OverTimeCalculatorMethod = entity.OverTimeCalculator;
                data.Result.SalaryAmount = overTime.OverTimeCalculator(salaryCal).Result;
                var salary = _map.Map<Salary>(data.Result);
                DateTime date = DateTime.Now;
                salary.Date = date.ToString("yyyy/MM/dd", new CultureInfo("fa-IR")); ;
                salary.CalenderDateID = Convert.ToInt32(salary.Date.Replace("/", ""));
                salary.Time = date.ToShortTimeString();
                var saveMessage = _salaryService.AddSalary(salary);
                if (saveMessage.Status == "Success")
                {
                    result = saveMessage;
                }
                else
                {
                    result = new MessageViewModel
                    {
                        ID = -1000,
                        Message = "Error In Save Salary",
                        Status = "Error",
                        Value = saveMessage.Message
                    };
                }

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

    }
}
