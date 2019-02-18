using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

    [SitecoreType(AutoMap = true)]
    public class ResortMap :Image
    {
        public string RemoteImageUrl { get; set; }

    }
}