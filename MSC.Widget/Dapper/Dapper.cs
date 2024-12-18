using Dapper;
using MSC.Widget.Presentations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Widget.Dapper
{
    public class Dapper<T> : IDapper<T> where T : class
    {
        public Dapper()
        {

        }

        public ResultViewModel<T> CallProcdure(string procName, Parameter parameter, string connection = "MSCConnectionString")
        {
            ResultViewModel<T> result = new ResultViewModel<T>();
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(connection))
                {
                    dbConnection.Open();
                    result.List = dbConnection.QuerySingleOrDefault<List<T>>(procName, parameter, commandType: CommandType.StoredProcedure);
                    result.Message = new MessageViewModel
                    {
                        ID = 1,
                        Message = "Dapper Success",
                        Status = "Success",
                        Value = ""
                    };
                }
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
