using BLL.ApiModels.Request.Client;
using BLL.ApiModels.Response.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface IClientService
    {
        public Task<GetClientInfoResponseModel> GetClientInfo(GetClientInfoRequestModel request);
        public Task<GetAutoModelsResponseModel> GetAutoModels(GetAutoModelsRequestModel request);
        public Task<GetOrderStatisticResponseModel> GetOrderStatistic(GetOrderStatisticRequestModel request);
        public Task<CreateOrderResponseModel> CreateOrder(CreateOrderRequestModel request);
        public Task<CreateCarResponseModel> CreateCar(CreateCarRequestModel request);
    }
}
