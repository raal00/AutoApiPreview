using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApiModels.Response
{
    public interface IResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
    }
}
