using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class BannerImage : Image
    {
        public const string BannerUrlId = "{449B6BB1-F340-4CD3-8632-3DC21F5E82B7}";

        [SitecoreField(FieldName = BannerUrlId)]
        public virtual Link BannerUrl { get; set; }

    }
}