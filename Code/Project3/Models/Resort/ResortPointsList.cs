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
    public class ResortPointList
    {
        [SitecoreQuery("./*[@@templateid='{11FAFFE9-D1EA-4CE5-8153-F1EC32BE9AFC}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<PointsCategory> PointCategory { get; set; }

        [SitecoreQuery("./*[@@templateid='{F0435C3C-2750-4B99-993F-7F4592FC71D8}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ActivityCategory> ActivityCategory { get; set; }
   }
}