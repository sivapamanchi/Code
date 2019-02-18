using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace BGSitecore.Models
{
    public class Header
    {
        public const string HeaderImageId = "{7C3DA708-2969-47E8-8E20-C47EAE70EC39}";

        [SitecoreField(FieldName = HeaderImageId)]
        public virtual Image HeaderImage { get; set; }

    }
}