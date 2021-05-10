using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response.Client
{
    public class CreateCarResponseModel : IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }

        public CarDomain Car { get; set; }
    }
}
