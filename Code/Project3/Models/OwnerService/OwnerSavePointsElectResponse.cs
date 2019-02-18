using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerSavePointsElectResponse
{

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class OwnerSavePointsElectResponse
    {
        public string ResponseCode { get; set; }
        public string AuthorizationNumber { get; set; }
        public string TransactionID { get; set; }
        public string Message { get; set; }
        public string RetCode { get; set; }
        public List<Error> Errors { get; set; }
    }

}