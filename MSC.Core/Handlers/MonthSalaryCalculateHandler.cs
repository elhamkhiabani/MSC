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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Handlers
{
    public class MonthSalaryCalculateHandler : IMonthSalaryCalculateHandler
    {
        private readonly ISalaryService _salaryService;
        private readonly IInputProcessor<SalaryViewModel> _processor;
        private readonly IMapper _map;
        public MonthSalaryCalculateHandler(ISalaryService salaryService, IInputProcessor<SalaryViewModel> processor, IMapper map)
        {
            _salaryService = salaryService;
            _processor = processor;
            _map = map;
            
        }

        public MessageViewModel Add(RequestSalaryViewModel entity,string dataType)
        {
            MessageViewModel result= new MessageViewModel();
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

                if (data==null)
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
                data.Result.OverTimeCalculatorMethod = nameof(overTime);
                data.Result.SalaryAmount = overTime.OverTimeCalculator(salaryCal).Result;
                var salary = _map.Map<Salary>(data.Result);
                var saveMessage = _salaryService.Add(salary);
                if (saveMessage.Status=="Success")
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
                        Value = ""
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
