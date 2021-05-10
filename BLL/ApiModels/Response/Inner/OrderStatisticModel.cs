using BLL.DomainModels;
using System.Collections.Generic;

namespace BLL.ApiModels.Response.Inner
{
    public class OrderStatisticModel
    {
        public AssociateDomain Client { get; set; }
        public List<OrderDomain> Orders { get; set; }
    }
}
