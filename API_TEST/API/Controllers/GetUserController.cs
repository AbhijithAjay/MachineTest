using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class GetUserController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        [HttpGet]
        [Route("api/GetUser/GetUserByCode")]
        public async Task<IHttpActionResult> GetUserByCode(int code)
        {
            GetUserResponse response = await FetchUserByCode(code);
            return Ok(response);
        }

        private async Task<GetUserResponse> FetchUserByCode(int code)
        {
            Employee employee = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("Proc_GetUserByCode", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Code", code);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                Code = Convert.ToInt32(reader["Code"]),
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                LoginName = reader["LoginName"].ToString(),
                                LoginPassword = reader["LoginPassword"].ToString(),
                                MobileNo = reader["MobileNo"].ToString(),
                                Email = reader["Email"].ToString(),
                                DOB = Convert.ToDateTime(reader["DOB"]),
                                Status = reader["Status"].ToString()
                            };
                        }
                    }
                }
            }

            return new GetUserResponse
            {
                Success = true,
                StatusCode = 200,
                Data = new EmployeeData { Employee = employee }
            };
        }
    }
}
