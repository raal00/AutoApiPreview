using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Auth
{
    public class SignInResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }

        public AssociateDomain Associate { get; set; }
        public RoleDomain Role { get; set; }
    }
}
