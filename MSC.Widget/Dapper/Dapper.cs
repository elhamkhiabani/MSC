using Dapper;
using MSC.Widget.Presentations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Widget.Dapper
{
    public class Dapper<T> : IDapper<T> where T : class
    {
        private readonly IConfiguration _configuration;

        public Dapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResultViewModel<T> CallProcdure<T>(string procName, DynamicParameters parameter, string connection = "MSCConnectionString")
        {
            ResultViewModel<T> result = new ResultViewModel<T>();
            try
            {
                string connectionString =  _configuration.GetConnectionString(connection);

                using (IDbConnection dbConnection = new SqlConnection(connectionString))
                {
                    dbConnection.Open();
                    result.List = dbConnection.Query<T>(procName, parameter, commandType: CommandType.StoredProcedure).ToList();
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
