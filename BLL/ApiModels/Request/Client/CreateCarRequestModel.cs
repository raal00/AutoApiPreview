using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Client
{
    public class CreateCarRequestModel : IRequestModel
    {
        public CarDomain Car { get; set; }
    }
}
