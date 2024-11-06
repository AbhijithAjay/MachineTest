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
    public class SingleUserController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        [HttpPost]
        [Route("api/login/login")]
        public async Task<IHttpActionResult> Login(LoginRequest request)
        {
            LoginResponse response = await ValidateUser(request.Username, request.Password);

            return Ok(response);
        }

        private async Task<LoginResponse> ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("Proc_UserLogin", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new LoginResponse
                            {
                                Success = true,
                                StatusCode = 200,
                                Message = "Login Successful",
                                Data = new UserData
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
                                }
                            };
                        }
                        else
                        {
                            return new LoginResponse
                            {
                                Success = false,
                                StatusCode = 401,
                                Message = "Invalid username or password",
                                Data = null
                            };
                        }
                    }
                }
            }
        }
    }
}
