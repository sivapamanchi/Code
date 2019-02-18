using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models.ResortService.InventoryCalendarByResortResponse;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class SearchResultMonth
    {
        public string resortId { get; set; }
        public string checkInDate { get; set; }
        public string HandicapAccessible { get; set; }
        public List<string> WebUnitCode { get; set; }
    }
}