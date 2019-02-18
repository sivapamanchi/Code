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
    public class AllAlerts
    {
    
        [SitecoreField(FieldName = SitecoreReferenceBasePage.PageAlertsId)]
        public virtual IEnumerable<Alert> AllAlert { get; set; }

    }
}