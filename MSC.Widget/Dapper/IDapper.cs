using Dapper;
using MSC.Widget.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Widget.Dapper
{
    public interface IDapper<T>  where T : class
    {
        ResultViewModel<T> CallProcdure<T>(string procName, DynamicParameters parameter, string connection = "MSCConnectionString");
    }
}
