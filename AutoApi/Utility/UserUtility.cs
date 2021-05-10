using BLL.ApiModels.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutoApi.Utility
{
    public static class UserUtility
    {
        public class UserInfo
        {
            public bool IsAuthenticated { get; set; }
            public int? UserId { get; set; }
        }

        public static bool IsConnected(HttpContext context)
        {
            return context.User.Identity.IsAuthenticated;
        }

        public static UserInfo GetUserInfo(HttpContext context, ref IResponseModel responseModel)
        {
            UserInfo response = new UserInfo();

            if (!context.User.Identity.IsAuthenticated)
            {
                responseModel.ResponseMessage = "Авторизируйтесь, чтобы создать запрос";
                responseModel.IsSuccess = false;
                response.IsAuthenticated = false;
                response.UserId = null;
                return response;
            }
            response.IsAuthenticated = true;
            List<Claim> userClaims = context.User.Claims.ToList();
            var userIdClaim = userClaims.Where(x => x.Type == "userId").FirstOrDefault();

            if (userIdClaim == null)
            {
                responseModel.ResponseMessage = "Не удалось определить номер сотрудника";
                responseModel.IsSuccess = false;
                response.UserId = null;
                return response;
            }
            int userId = int.Parse(userIdClaim.Value);
            response.UserId = userId;
            return response;
        }
    }
}
