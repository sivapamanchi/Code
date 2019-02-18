using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Payments
{
    public class AccountHelpContent : RichText
    {
        public const string DataSourceId = "{74D004CE-23A2-4710-B72F-FF86200FA7C9}";
        
        [SitecoreField(FieldName = DataSourceId)]
        public virtual FeaturedItem DataSource { get; set; }
    }
}