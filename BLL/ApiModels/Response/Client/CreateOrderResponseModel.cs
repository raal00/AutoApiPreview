using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Client
{
    public class CreateOrderResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }

        public OrderDomain Order { get; set; }
    }
}
