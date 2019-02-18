using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BGSitecore.Models.Common
{
    public class CustomResponse<T> : BaseResponse
    {
        public T Payload { get; set; }
    }
}