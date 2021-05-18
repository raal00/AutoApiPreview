using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Client
{
    public class GetOrderStatisticRequestModel : IRequestModel
    {
        public DateTime From { get; set; } = DateTime.Parse("2020.01.01");
        public DateTime To { get; set; } = DateTime.Now.AddDays(1);
    }
}
