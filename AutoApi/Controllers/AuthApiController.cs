using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.ApiModels.Request.Auth;
using BLL.ApiModels.Response.Auth;
using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    [Route("auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthApiController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        [Route("logout")]
        public async Task<LogOutResponseModel> LogoutAsync([FromBody] LogOutRequestModel request)
        {
            var response = await _authService.LogOut(request);
            if (response.IsSuccess)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return response;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResponseModel> LoginAsync([FromBody] LoginRequestModel request)
        {
            LoginResponseModel response = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                response = new LoginResponseModel();
                response.IsSuccess = false;
                response.ResponseMessage = "Вы уже вошли в систему";
                return response;
            }
            try 
            { 
                response = await _authService.Login(request);
            }
            catch (Exception er)
            {
                throw er;
            }
            
            if (response.IsSuccess)
            {
                var claims = new List<Claim>() {
                    new Claim("userId", response.Associate.Id.ToString()),
                    new Claim(ClaimTypes.Role, response.Role.RoleSystemName),
                    new Claim(ClaimTypes.NameIdentifier, response.Associate.FirstName)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, 
                                                                   ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                try
                {
                    await HttpContext.SignInAsync(claimsPrincipal);
                }
                catch (Exception er)
                {
                    response.IsSuccess = false;
                    response.ResponseMessage = er.Message;
                }
            }
            return response;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<SignInResponseModel> signin([FromBody] SignInRequestModel request)
        {
            SignInResponseModel response = null;
            try
            {
                response = await _authService.SignIn(request);
            }
            catch(Exception er)
            {
                throw er;
            }

            if (response.IsSuccess)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, response.Role.RoleSystemName),
                    new Claim("userId", response.Associate.Id.ToString()),
                    new Claim(ClaimTypes.Role, response.Role.RoleSystemName),
                    new Claim(ClaimTypes.NameIdentifier, response.Associate.FirstName)
                };

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                try
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
                }
                catch (Exception er)
                {
                    response.ResponseMessage = er.Message;
                    response.IsSuccess = false;
                }
            }
            return response;
        }

        [HttpGet]
        [Route("isauthenticated")]
        public async Task<bool> IsAuthenticated()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
