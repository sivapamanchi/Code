using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class ActivityDetail
    {
        public const string DisplayNameId = "{283836DC-C806-436A-AAEE-8872D60FAB71}";
        public const string CssClassId = "{F2187DAC-3A2A-4738-B7D0-07C737BC97D6}";
        public const string PointsId = "{AD7C48FC-ED62-44C5-821B-EEF92C38ABAA}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public virtual string FullPath { get; set; }

        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        [SitecoreField(FieldName = PointsId)]
        public virtual string Points { get; set; }

        [SitecoreField(FieldName = CssClassId)]
        public virtual string CssClass { get; set; }

    }
}