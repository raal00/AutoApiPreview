using BLL.ApiModels.Request.Auth;
using BLL.ApiModels.Response.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface IAuthService
    {
        public Task<LoginResponseModel> Login(LoginRequestModel request);
        public Task<LogOutResponseModel> LogOut(LogOutRequestModel request);
        public Task<SignInResponseModel> SignIn(SignInRequestModel request);
    }
}
