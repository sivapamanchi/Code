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
    public class SignInHelpCollection: BasePage
    {
        [SitecoreQuery("./*", IsRelative = true)]
        public virtual IEnumerable<SignInHelp> List { get; set; }
    }
}