using Microsoft.Extensions.DependencyInjection;
using MSC.Core.CRUD;
using MSC.Core.Handlers;
using MSC.Core.Handlers.Intefaces;
using MSC.Core.Repositories;
using MSC.Core.Services;
using MSC.Core.Services.Interfaces;
using MSC.Widget.Dapper;
using MSC.Widget.InputProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.IoC
{
    public static class IoC
    {
        public static void Injector(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICRUD<>),typeof(CRUD<>));
            services.AddScoped(typeof(IRepository<,>),typeof(Repository<,>));
            services.AddScoped<ISalaryService,SalaryService>();
            services.AddScoped(typeof(IInputProcessor<>),typeof(InputProcessor<>));
            services.AddScoped(typeof(IDapper<>), typeof(Dapper<>));

            services.AddScoped<IMonthSalaryCalculateHandler, MonthSalaryCalculateHandler>();

        }
    }
}
