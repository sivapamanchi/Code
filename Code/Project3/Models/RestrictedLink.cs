using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class RestrictedLink
    {
        public const string LinkTitleId = "{423D289A-C3E4-459B-83B7-FA6333A9FC41}";
        public const string LinkUrlId = "{1F791A22-7AAB-4439-B636-DE0AD4E9CB2B}";
        public const string OwnerTypeId = "{95278610-1829-4039-B229-AB5C7AC9C774}";
        
        [SitecoreItem]
        public virtual Item InnerItem { get; set; }

        [SitecoreField(FieldName = RestrictedLink.LinkTitleId)]
        public virtual string LinkTitle { get; set; }

        [SitecoreField(FieldName = RestrictedLink.LinkUrlId)]
        public virtual Link LinkUrl { get; set; }

        [SitecoreField(FieldName = RestrictedLink.OwnerTypeId)]
        public virtual string OwnerType { get; set; }
    }
}