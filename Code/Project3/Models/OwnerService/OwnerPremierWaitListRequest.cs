using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerPremierWaitListRequest
{
    public class RestOwnerPremierWaitListRequest
    {
        public string RequestID { get; set; }
        public string Arvact { get; set; }
        public string ResortName { get; set; }
        public string Status { get; set; }
        public string DateArrivalFrom { get; set; }
        public string DateArrivalTo { get; set; }
        public string DateEnteredFrom { get; set; }
        public string DateEnteredTo { get; set; }
    }

}