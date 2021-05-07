using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Auth
{
    public class SignInRequestModel : IRequestModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
    }
}
