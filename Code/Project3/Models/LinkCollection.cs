using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc;
using Sitecore.Links;
using Sitecore.Data.Items;
using Sitecore.Globalization;

namespace BGSitecore.Models
{
    public class LinkCollection : BaseComponent
    {

        public const string TitleId = "{377F120D-E9A9-4252-AED1-1E61DA02F981}";

        [SitecoreField(FieldName = LinkCollection.TitleId)]
        public virtual string Title { get; set; }

        [SitecoreQuery("./*", IsRelative = true)]
        public virtual IEnumerable<RestrictedLink> List { get; set; }
    }
}