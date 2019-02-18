using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class SideWidgetContent : BaseComponent
    {

        public const string WidgetLinkId = "{2C9FCFCB-9C23-4019-9380-EC0E1474C1F3}";
        public const string WidgetTextId = "{F49E6E9F-D40A-4E7F-9378-D27B0C3FF2FB}";
        public const string SubWidgetTextId = "{0089A7F9-DDDA-4487-8AFC-85E5CC162933}";
        public const string SubWidgetLinkId = "{E9E2F31E-CB6F-4960-BB52-25C1F843E1E4}";
        public const string CssClassId = "{738CFE34-7F84-4B1B-9418-AB80A74811B2}";

        [SitecoreField(FieldName = WidgetLinkId)]
        public virtual Glass.Mapper.Sc.Fields.Link WidgetLink { get; set; }

        [SitecoreField(FieldName = WidgetTextId)]
        public virtual string WidgetText { get; set; }

        [SitecoreField(FieldName = SubWidgetLinkId)]
        public virtual Glass.Mapper.Sc.Fields.Link SubWidgetLink { get; set; }

        [SitecoreField(FieldName = SubWidgetTextId)]
        public virtual string SubWidgetText { get; set; }

        [SitecoreField(FieldName = CssClassId)]
        public virtual string CssClass { get; set; }

        public bool isVisible { get; set; }
    }
}