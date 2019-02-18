using BGSitecore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class CheckMaintenanceFeeSettlementBatchResponse
    {
        public string Records { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}