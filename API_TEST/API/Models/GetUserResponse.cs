using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class GetUserResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public EmployeeData Data { get; set; }
    }

    public class EmployeeData
    {
        public Employee Employee { get; set; }
    }

    public class Employee
    {
        public int Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Status { get; set; }
    }
}