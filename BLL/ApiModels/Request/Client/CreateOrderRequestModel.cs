using BLL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Client
{
    public class CreateOrderRequestModel : IRequestModel
    {
        public int AssociateId { get; set; }
        public OrderDomain Order { get; set; }
    }
}
