using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Client
{
    public class GetOrderStatisticRequestModel : IRequestModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
