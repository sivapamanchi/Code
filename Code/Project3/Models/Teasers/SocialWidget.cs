using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Teasers
{
    [SitecoreType]
    public class SocialWidget : BaseComponent
    {
        public const string UserHandleId = "{7FFEE4A2-3B7C-4525-AF4A-1D2BDDB8CA18}";
        public const string TitleId = "{27217369-7031-4FFE-B42D-4D8640831278}";

        [SitecoreField(FieldName = UserHandleId)]
        public virtual string UserHandle { get; set; }
     
        [SitecoreField(FieldName = TitleId)]
        public virtual string Title { get; set; }

        [SitecoreIgnore]
        public virtual bool IsAllowed { get; set; } = false;
    }
}