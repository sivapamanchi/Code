using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models
{
    public class Error404 : BasePage
    {
        public const string SubTitleId = "{2DD8AC55-A5A6-4AA3-89C9-1366487E2BF8}";
        [SitecoreField(FieldName = SubTitleId)]
        public virtual string SubTitle { get; set; }

        public const string ContentId = "{BB40959E-C016-40B7-94F8-2E357DC669BE}";
        [SitecoreField(FieldName = ContentId)]
        public virtual string Content { get; set; }

    }
}
