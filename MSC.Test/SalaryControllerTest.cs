using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSC.API.Controllers;
using MSC.Core.CRUD;
using MSC.Core.Handlers;
using MSC.Core.Handlers.Intefaces;
using MSC.Core.Mapper;
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
using System.IO;
using Xunit;

namespace MSC.Test
{
    public class SalaryControllerTest
    {
        private readonly SalaryController _salary;
        private readonly ISalaryService _salaryService;
        private readonly IMonthSalaryCalculateHandler _salaryhandler;

        public SalaryControllerTest()
        {
            var services = new ServiceCollection();

            
            var connectionString = "Server=localhost;Database=MSCDB;Trusted_Connection=True";

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));


            services.AddAutoMapper(typeof(MyMapper).Assembly);

            services.AddScoped<ICRUD<Salary>, CRUD<Salary>>(provider =>
            {
                var context = provider.GetRequiredService<DatabaseContext>();
                return new CRUD<Salary>(context);
            });
            services.AddScoped<ICRUD<CalenderDate>, CRUD<CalenderDate>>(provider =>
            {
                var context = provider.GetRequiredService<DatabaseContext>();
                return new CRUD<CalenderDate>(context);
            });
            services.AddScoped<IRepository<Salary, SalaryViewModel>, Repository<Salary, SalaryViewModel>>();
            services.AddScoped<IRepository<CalenderDate, CalenderDateViewModel>, Repository<CalenderDate, CalenderDateViewModel>>();


            services.AddScoped<ISalaryService, SalaryService>(provider =>
            {
                var map = provider.GetService<IMapper>();
                var dapper = provider.GetService<IDapper<Salary>>();
                return new SalaryService(provider, map, dapper);
            });


        
            services.AddScoped<IMonthSalaryCalculateHandler, MonthSalaryCalculateHandler>(provider =>
            {
                var salary = provider.GetService<ISalaryService>();
                var calender = provider.GetService<ICalenderDateService>();
                var processor = provider.GetService<IInputProcessor<SalaryViewModel>>();
                var map = provider.GetService<IMapper>();
                return new MonthSalaryCalculateHandler(salary, calender, processor, map);
            });

            services.AddScoped<IInputProcessor<SalaryViewModel>, InputProcessor<SalaryViewModel>>();
            services.AddScoped<ICalenderDateService, CalenderDateService>(provider =>
            {
                var map = provider.GetService<IMapper>();
                var dapper = provider.GetService<IDapper<CalenderDate>>();
                return new CalenderDateService(provider, map, dapper);
            });


            var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Or specify your path
    .AddJsonFile("appsettings.json")
    .Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IDapper<Salary>, Dapper<Salary>>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();

                return new Dapper<Salary>(config);
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
                Data = "{\"ID\":0,\"FirstName\":\"Elham\",\"LastName\":\"Khiabani\",\"BasicSalary\":10000,\"Allowance\":100,\"Transportation\":10,\"Date\":null,\"Time\":null,\"OverTimeCalculatorMethod\":null,\"SalaryAmount\":0,\"IsActive\":true,\"IsDelete\":false,\"CreatorID\":0,\"CreationDateTime\":\"0001-01-01T00:00:00\",\"ModifierID\":0,\"ModifierDateTime\":\"0001-01-01T00:00:00\"}",
                OverTimeCalculator = "CalculatorA"
            };

            // Act
            var result = _salary.add(mockData, "Json");

            // Assert
            //Assert.Equal<string>(result.Status, "Success");
            Assert.Equal<string>(result.Value, "Duplicate");

        }


        [Fact]
        public void update()
        {
            // Arrange
            var mockData = new UpdateViewModel
            {
                Allowance = 100,
                FirstName = "Elham",
                LastName = "Khiabani",
                Month = "دی"
            };

            // Act
            var result = _salary.update(mockData);

            // Assert
            Assert.Equal<string>(result.Status, "Success");
        }

        [Fact]
        public void remove()
        {
            // Arrange
            var mockData = new FilterViewModel
            {
                FirstName = "Elham",
                LastName = "Khiabani",
                FromDate = "دی"
            };

            // Act
            var result = _salary.removeForMonth(mockData);

            // Assert
            Assert.Equal<string>(result.Status, "Success");
        }


        [Fact]
        public void get()
        {
            // Arrange
            var mockData = new FilterViewModel
            {
                FirstName = "Elham",
                LastName = "Khiabani",
                FromDate = "1403/09/22"
            };

            // Act
            var result = _salary.getMonthlyByFullname(mockData);

            // Assert
            Assert.Equal<string>(result.Message.Status, "Success");
        }


        [Fact]
        public void getRange()
        {
            // Arrange
            var mockData = new FilterViewModel
            {
                FromDate = "1403/09/22",
                ToDate = "1403/10/01",
                PageNumber =1,
                PageSize =20
            };

            // Act
            var result = _salary.selectAll(mockData);

            // Assert
            Assert.Equal<string>(result.Message.Status, "Success");
        }
    }
}
