using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class Wysiwyg: BaseComponent
    {
        public const string TitleId = "{0A83A6B3-E1DE-4949-82E2-B5105415C94F}";
        public const string ContentId = "{7221BC8E-4F3A-4D03-B642-3BF814BC303B}";

        [SitecoreField(FieldName = Wysiwyg.TitleId)]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = Wysiwyg.ContentId)]
        public virtual string Content { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

    }
}