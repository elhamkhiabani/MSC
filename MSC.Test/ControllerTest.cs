using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MSC.API.Controllers;
using MSC.Core.CRUD;
using MSC.Core.Handlers;
using MSC.Core.Handlers.Intefaces;
using MSC.Core.Presentations;
using MSC.Core.Presentations.Base;
using MSC.Core.Repositories;
using MSC.Core.Services;
using MSC.Core.Services.Interfaces;
using MSC.Data.DatabseContext;
using MSC.Domain.Models;
using MSC.Widget.Dapper;
using MSC.Widget.InputProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MSC.Test
{
    public class ControllerTest
    {
        private readonly SalaryController _salary;
        private readonly ISalaryService _salaryService;
        private readonly IMonthSalaryCalculateHandler _salaryhandler;

        public ControllerTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer("MSCConnectionString"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICRUD<Salary>, CRUD<Salary>>(provider =>
            {
                var context = provider.GetRequiredService<DatabaseContext>();
                return new CRUD<Salary>(context);
            });
            services.AddScoped<IRepository<Salary,SalaryViewModel>, Repository<Salary, SalaryViewModel>>(provider =>
            {
                var map = provider.GetService<IMapper>();
                var crud = provider.GetService<ICRUD<Salary>>();

                return new Repository<Salary,SalaryViewModel>(crud, map);
            });


            services.AddScoped<ISalaryService, SalaryService>(provider =>
            {
                var map = provider.GetService<IMapper>();
                var dapper = provider.GetService<IDapper<Salary>>();
                var crud = provider.GetService<ICRUD<Salary>>();

                return new SalaryService(crud, map, dapper);
            });
            services.AddScoped<IMonthSalaryCalculateHandler, MonthSalaryCalculateHandler>(provider =>
            {
                var salary = provider.GetService<ISalaryService>();
                var calender = provider.GetService<ICalenderDateService>();
                var processor = provider.GetService<IInputProcessor<SalaryViewModel>>();
                var map = provider.GetService<IMapper>();

                return new MonthSalaryCalculateHandler(salary,calender,processor,map);
            });

            services.AddScoped<IInputProcessor<SalaryViewModel>, InputProcessor<SalaryViewModel>>();
            services.AddScoped<ICalenderDateService, CalenderDateService>(provider =>
            {
                var map = provider.GetService<IMapper>();
                var dapper = provider.GetService<IDapper<CalenderDate>>();
                var crud = provider.GetService<ICRUD<CalenderDate>>();

                return new CalenderDateService(crud, map, dapper);
            });

            var serviceProvider = services.BuildServiceProvider();

            _salaryService = serviceProvider.GetService<ISalaryService>();
            _salaryhandler = serviceProvider.GetService<IMonthSalaryCalculateHandler>();

           

            _salary = new SalaryController(_salaryhandler, _salaryService);
        }

        [Fact]
        public void add()
        {
            // Arrange
            var mockData = new RequestSalaryViewModel
            {
                Data = "{\"ID\":0,\"FirstName\":\"Elham\",\"LastName\":\"Khiabani\",\"BasicSalary\":10000,\"Allowance\":100,\"Transportation\":10,\"Date\":null,\"Time\":null,\"OverTimeCalculatorMethod\":null,\"SalaryAmount\":0,\"IsActive\":false,\"IsDelete\":false,\"CreatorID\":0,\"CreationDateTime\":\"0001-01-01T00:00:00\",\"ModifierID\":0,\"ModifierDateTime\":\"0001-01-01T00:00:00\"}",
                OverTimeCalculator = "CalculatorA"
            };
            var message = new MessageViewModel();
            //_handlerMock.Setup(service => service.Add(mockData,"Json")).Returns(message);

            // Act
            var result = _salary.add(mockData,"Json");

            // Assert
            Assert.Equal<string>(result.Status,"Success");
        }
    }
}
