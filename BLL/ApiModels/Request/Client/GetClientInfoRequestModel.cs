using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Request.Client
{
    public class GetClientInfoRequestModel : IRequestModel
    {
        public int AssociateId { get; set; }
    }
}
