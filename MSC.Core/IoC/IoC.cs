using Microsoft.Extensions.DependencyInjection;
using MSC.Core.CRUD;
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


        }
    }
}
