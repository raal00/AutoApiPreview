using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Client
{
    public class GetClientInfoResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }

        public AssociateDomain Client { get; set; }
        public List<OrderDomain> Orders { get; set; }
    }
}
