using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BGSitecore.Models.Common
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public HttpStatusCode CustomHttpStatusCode { get; set; }
    }
}