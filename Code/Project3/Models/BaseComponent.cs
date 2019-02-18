using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    [SitecoreType(AutoMap =true)]
    public class BaseComponent
    {
        public const string RestrictionRuleId = "{85B5A4F7-0974-4455-85FA-B33FFAF397E0}";

        [SitecoreField(FieldName = RestrictionRuleId)]
        public virtual string RestrictionRule { get; set; }

        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreItem]
        public virtual Item InnerItem { get; set; }

        [SitecoreParent]
        public virtual Item Parent { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        public virtual Guid TemplateId { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public virtual string FullPath { get; set; }

    }
}