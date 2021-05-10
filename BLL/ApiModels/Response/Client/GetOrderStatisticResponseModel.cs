using BLL.ApiModels.Response.Inner;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Client
{
    public class GetOrderStatisticResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }

        public List<OrderStatisticModel> Statistics { get; set; }
    }
}
