using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class ResortSections : BaseComponent
    {
        [SitecoreQuery("./*[@@templateid='{95BD385D-453E-440E-B18B-27DC19D849F9}']", IsRelative = true)]
        public virtual IEnumerable<Section> AllSections { get; set; }

        [SitecoreField(FieldName = ResortDetails.ResortNameId)]
        public virtual string ResortName { get; set; }
    }
}