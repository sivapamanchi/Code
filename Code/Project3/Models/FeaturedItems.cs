using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models
{
    public class FeaturedItems : BasePage
    {

        [SitecoreQuery("/sitecore/content//*[@@templatename='Featured Item Page']", IsRelative = false)]
        public virtual IEnumerable<FeaturedItem> AllFeaturedItems { get; set; }

    }
}