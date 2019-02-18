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

    [SitecoreType]
    public class Video
    {

        public const string VideoUrlId = "{F818BD16-32A3-47CB-86E0-ABCB3AEF4F4C}";


        [SitecoreField(FieldName = VideoUrlId)]
        public virtual string VideoUrl { get; set; }

        public string RemoteVideoUrl { get; set; }



    }
}