using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class Room: BasePage
    {
        public const string UnitTypeId = "{D8753A0F-3BE6-4BDA-B5BA-3A7F5176AB2F}";
        public const string ViewId = "{EC7C2B52-F32B-45E6-B261-17ED44C99FF1}";
        public const string DescriptionId = "{B949AA65-0821-4A7D-80E9-542A4078B2BE}";
        public const string BluegreenUnitCodeId = "{2416CBDE-84C1-4B90-BB05-A2771967999B}";
        public const string CheckInDayId = "{FD878D5B-D6BC-4ADE-8F5E-A0040EA7DB9B}";

        [SitecoreField(FieldName = UnitTypeId)]
        public virtual string UnitType { get; set; }

        [SitecoreField(FieldName = ViewId)]
        public virtual string ViewTitle { get; set; }

        [SitecoreField(FieldName = DescriptionId)]
        public virtual string RoomDescription { get; set; }

        [SitecoreField(FieldName = BluegreenUnitCodeId)]
        public virtual string BluegreenUnitCode { get; set; }

        [SitecoreField(FieldName = CheckInDayId)]
        public virtual string CheckInDay { get; set; }

    }
}