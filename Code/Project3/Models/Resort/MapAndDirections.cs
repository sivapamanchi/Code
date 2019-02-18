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

    [SitecoreType(AutoMap = true)]
    public class MapAndDirections
    {
        public const string LatitudeId = "{F0C651F1-288A-40A5-A85C-348E9D7F484B}";
        [SitecoreField(FieldName = LatitudeId)]
        public virtual string Latitude { get; set; }

        public const string LongitudeId = "{5A498051-E8CF-4DD0-84E9-A85D410071AC}";
        [SitecoreField(FieldName = LongitudeId)]
        public virtual string Longitude { get; set; }

        public const string MapZoomId = "{DBFB80AB-0B29-4741-B1DD-2DC685FD6C0C}";
        [SitecoreField(FieldName = MapZoomId)]
        public virtual int MapZoom { get; set; }

        public const string GetDirectionButtonId = "{81CCBAA4-2107-41AE-8896-0F4A94AC62C8}";
        [SitecoreField(FieldName = GetDirectionButtonId)]
        public virtual string GetDirectionButton { get; set; }

        public const string DirectionMessageId = "{F291B551-B39D-45AE-8AE7-B5166B98AE01}";
        [SitecoreField(FieldName = DirectionMessageId)]
        public virtual string DirectionMessage { get; set; }
    }
}