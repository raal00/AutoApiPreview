using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Auth
{
    public class LoginRequestModel : IRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
