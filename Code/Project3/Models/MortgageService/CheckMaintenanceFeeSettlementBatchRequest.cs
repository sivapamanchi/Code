using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class CheckMaintenanceFeeSettlementBatchRequest
    {
        public string arvact { get; set; }
        public string projNum { get; set; }
        public string date { get; set; }
    }
}