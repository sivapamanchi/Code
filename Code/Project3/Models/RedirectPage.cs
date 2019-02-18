using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using SC = Sitecore;

namespace BGSitecore.Models
{
    public class RedirectPage
    {
        [SitecoreField(FieldName = SitecoreReferenceMenuItem.ExternalUrl)]
        public virtual Link RedirectUrl { get; set; }
    }
}