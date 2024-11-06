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
    public class UpdateUserController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        [HttpPut]
        [Route("api/UpdateUser/UpdateUser")]
        public async Task<IHttpActionResult> UpdateUser(UserUpdateRequest request)
        {
            UserUpdateResponse response = await UpdateUserInDB(request);
            return Ok(response);
        }

        private async Task<UserUpdateResponse> UpdateUserInDB(UserUpdateRequest request)
        {
            DateTime DOBco = Convert.ToDateTime(request.DOB);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("Proc_UpdateUser", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Code", request.Code);
                    command.Parameters.AddWithValue("@FirstName", request.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", request.MiddleName);
                    command.Parameters.AddWithValue("@LastName", request.LastName);
                    command.Parameters.AddWithValue("@LoginName", request.LoginName);
                    command.Parameters.AddWithValue("@LoginPassword", request.LoginPassword);
                    command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                    command.Parameters.AddWithValue("@Email", request.Email);
                    command.Parameters.AddWithValue("@DOB", DOBco);
                    command.Parameters.AddWithValue("@Status", request.Status);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return new UserUpdateResponse
                        {
                            Success = true,
                            StatusCode = 200,
                            Message = "User Updated Successfully ...",
                            Data = new { code = request.Code }
                        };
                    }
                    else
                    {
                        return new UserUpdateResponse
                        {
                            Success = false,
                            StatusCode = 404,
                            Message = "User not found",
                            Data = null
                        };
                    }
                }
            }
        }
    }
}