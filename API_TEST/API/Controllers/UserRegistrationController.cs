using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class UserRegistrationController : ApiController
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        [HttpPost]
        public async Task<IHttpActionResult> RegisterUser()
        {
            string requestData = Request.Content.ReadAsStringAsync().Result;
            string decodedData = HttpUtility.UrlDecode(requestData);
            string jsondata = decodedData.Substring(decodedData.IndexOf('{'));

            UserRegistrationRequest data = JsonConvert.DeserializeObject<UserRegistrationRequest>(jsondata);

            int Code = data.Code;
            string FirstName = data.FirstName;
            string MiddleName = data.MiddleName;
            string lastName = data.LastName;
            string LoginName = data.LastName;
            string LoginPassword = data.LoginPassword;
            string Mobileno = data.MobileNo;
            string Email = data.Email;
            string DOB = data.DOB;
            String Status = data.Status;

            UserRegistrationResponse response = await Registration(Code, FirstName, MiddleName, lastName, LoginName, LoginPassword, Mobileno, Email, DOB, Status);

            string responsestr = JsonConvert.SerializeObject(response);
            JObject jsonStr = (JObject)JsonConvert.DeserializeObject(responsestr);

            return Ok(jsonStr);
        }
        public async Task<UserRegistrationResponse> Registration(int Code, string FirstName, string MiddleName, string lastName, string LoginName, string LoginPassword, string Mobileno, string Email, string DOB, String Status)
        {
            DateTime DOBcon = Convert.ToDateTime(DOB);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("Proc_Registration", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Code", Code);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@MiddleName", MiddleName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@LoginName", LoginName);
                    command.Parameters.AddWithValue("@LoginPassword", LoginPassword);
                    command.Parameters.AddWithValue("@MobileNo", Mobileno);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@DOB", DOBcon);
                    command.Parameters.AddWithValue("@Status", Status);


                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        string Success = reader["success"].ToString();
                        int statusCode = (int)reader["statusCode"];
                        string message = reader["Message"].ToString();
                        int code = (int)reader["Code"];


                        return new UserRegistrationResponse { Success = Success, StatusCode = statusCode, Message = message, Data = new { code = code } };
                    }
                }
            }
        }
    }
}
