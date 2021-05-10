using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Client
{
    public class GetAutoModelsRequestModel : IRequestModel
    {
        public int BatchSize { get; set; }
        public int From { get; set; }
        public string OrderBy { get; set; }
        public string Type { get; set; }
    }
}
