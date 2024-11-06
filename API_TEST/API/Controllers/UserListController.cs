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
    public class UserListController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        [HttpGet]
        [Route("api/users/GetUserList")]
        public async Task<IHttpActionResult> GetUserList()
        {
            UserResponse response = await FetchUser();
            return Ok(response);
        }

        private async Task<UserResponse> FetchUser()
        {
            List<User> employeeList = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("Proc_GetUsers", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            User user = new User
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
                            employeeList.Add(user);
                        }
                    }
                }
            }

            return new UserResponse
            {
                Success = true,
                StatusCode = 200,
                Message = "",
                Data = new UsersData
                {
                    TotalRecord = employeeList.Count,
                    EmployeeList = employeeList
                }
            };
        }
    }
}
