using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models.ResortService.InventoryCalendarByResortResponse;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class SearchResultMonthList
    {
        public bool isSummerMonth { get; set; }
        public bool ShowInternalError { get; set; }
        public List<SearchResultMonth> allResults{ get; set; }
}
}