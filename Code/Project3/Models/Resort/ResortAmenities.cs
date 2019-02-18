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

    [SitecoreType]
    public class ResortAmenities
    {

        public const string ResortHeaderId = "{5D3E53CF-6C5B-4C21-86EA-DAD6ABA02548}";
        public const string VillatHeaderId = "{8A899AB0-13BA-48E9-B961-167779DDDE67}";
        public const string AreaHeaderId = "{0B909DF1-45BD-49BA-8900-CDC95513D3C1}";
        public const string ResortAmmentiesId = "{D8495D4B-1FAB-431D-9FDF-93B6FF50F6E8}";
        public const string VillaAmmentiesId = "{CEFA9A4D-3178-4A05-9993-CE93411EDCB3}";
        public const string AreaAmmentiesId = "{D187DB7B-F6F1-42B9-B869-76FC433C0803}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = ResortHeaderId)]
        public virtual string ResortHeader { get; set; }

        [SitecoreField(FieldName = VillatHeaderId)]
        public virtual string VillaHeader { get; set; }

        [SitecoreField(FieldName = AreaHeaderId)]
        public virtual string AreaHeader { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Checklist, FieldId = ResortAmmentiesId, Setting = SitecoreFieldSettings.InferType)]
        public virtual IEnumerable<Amenities> ResortAmmenties { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Checklist, FieldId = VillaAmmentiesId, Setting = SitecoreFieldSettings.InferType)]
        public virtual IEnumerable<Amenities> VillaAmmenties { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Checklist, FieldId = AreaAmmentiesId, Setting = SitecoreFieldSettings.InferType)]
        public virtual IEnumerable<Amenities> AreaAmmenties { get; set; }
    }
}