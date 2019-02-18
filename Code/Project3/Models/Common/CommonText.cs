using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace BGSitecore.Models.Common
{
    [SitecoreType(AutoMap = true)]
    public class CommonText : BaseComponent
    {
       
        public const string RichTextId = "{8AEDC622-8065-4FD6-9A52-53770E57A67D}";
        [SitecoreField(FieldName = RichTextId)]
        public virtual string DisplayName { get; set; }
    }
}