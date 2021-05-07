using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Auth
{
    public class LogOutResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }
}
