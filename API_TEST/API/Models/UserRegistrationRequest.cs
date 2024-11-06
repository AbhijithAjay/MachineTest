using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class UserRegistrationRequest
    {
        public int Code { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
        public string Status { get; set; }
    }

    public class UserRegistrationResponse
    {
        public string Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
