using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

    [SitecoreType]
    public class NearbyResortList : Section
    {
        public const string ResortId = "{4A4A6993-BC22-417C-8995-A26B5F9A5072}";
        public const string DistanceMessageId = "{B07F7CAF-716A-4BEF-BE05-5281F713F95B}";
        public const string NumberToDisplayId = "{FA79BFB6-9887-4D2F-B8F4-92EA8BD5CA60}";

       // [SitecoreField(FieldName = ResortId)]
        public virtual IEnumerable<ResortDetails> Resort { get; set; }

        [SitecoreField(FieldName = DistanceMessageId)]
        public virtual string DistanceMessage { get; set; }

        [SitecoreField(FieldName = NumberToDisplayId)]
        public virtual int NumberToDisplay { get; set; }


    }
}