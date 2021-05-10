using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Client
{
    public class GetAutoModelsResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }

        public List<CarDomain> Cars { get; set; }
    }
}
