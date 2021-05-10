using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoApi.Utility;
using BLL.ApiModels.Request.Client;
using BLL.ApiModels.Response;
using BLL.ApiModels.Response.Client;
using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    [Route("client")]
    [ApiController]
    [Produces("application/json")]
    public class ClientApiController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientApiController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        [HttpPost]
        [Authorize(Roles = "user,admin")]
        [Route("getclientinfo")]
        public async Task<GetClientInfoResponseModel> GetClientInfo([FromBody] GetClientInfoRequestModel request)
        {
            IResponseModel response = new GetClientInfoResponseModel();
            var userInfo = UserUtility.GetUserInfo(HttpContext, ref response);
            if (!userInfo.UserId.HasValue)
            {
                return response as GetClientInfoResponseModel;
            }
            request.AssociateId = userInfo.UserId.Value;
            response = await _clientService.GetClientInfo(request);
            return response as GetClientInfoResponseModel;
        }

        [HttpPost]
        [Authorize(Roles = "user,admin")]
        [Route("getautomodels")]
        public async Task<GetAutoModelsResponseModel> GetAutoModels([FromBody] GetAutoModelsRequestModel request)
        {
            var response = await _clientService.GetAutoModels(request);
            return response;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("getorderstatistic")]
        public async Task<GetOrderStatisticResponseModel> GetOrderStatistic([FromBody] GetOrderStatisticRequestModel request)
        {
            var response = await _clientService.GetOrderStatistic(request);
            return response;
        }

        // TODO: replace to order controller
        [HttpPost]
        [Authorize(Roles = "user,admin")]
        [Route("order")]
        public async Task<CreateOrderResponseModel> Order([FromBody] CreateOrderRequestModel request)
        {
            IResponseModel response = new CreateOrderResponseModel();
            var userInfo = UserUtility.GetUserInfo(HttpContext, ref response);
            if (!userInfo.UserId.HasValue)
            {
                return response as CreateOrderResponseModel;
            }
            request.AssociateId = userInfo.UserId.Value;
            response = await _clientService.CreateOrder(request);
            return response as CreateOrderResponseModel;
        }

        // TODO: replace to car controller
        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("car")]
        public async Task<CreateCarResponseModel> Car([FromBody] CreateCarRequestModel request)
        {
            IResponseModel response = new CreateOrderResponseModel();
            response = await _clientService.CreateCar(request);
            return response as CreateCarResponseModel;
        }
    }
}
