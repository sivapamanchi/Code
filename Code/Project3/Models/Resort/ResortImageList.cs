using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

  //  [SitecoreType]
    public class ResortImageList
    {

      //  [SitecoreQuery("./*", IsRelative = true)]
        public  IEnumerable<ResortImage> ResortImages { get; set; }

        public string classname { get; set; }
        public string RemoteImageUrl { get; set; }


    }
}