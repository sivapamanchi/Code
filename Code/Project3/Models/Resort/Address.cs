using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class Address
    {
        public const string AddressLine1Id = "{70C35537-554F-46D0-9348-ED1F5867CABC}";
        public const string AddressLine2Id = "{472D094C-B2AA-461B-9165-A8841DDD59DE}";
        public const string ZipCodeId = "{25AE128C-BD25-401A-95F0-5A1B35462D61}";
        public const string LatitudeId = "{9C7FE197-271C-4108-BA04-4301673EA080}";
        public const string LongitudeId = "{6BB1146D-2A34-4F2D-BAC3-B4031BB945D5}";
        public const string StateId = "{C8EACDB8-9410-46C0-9C7C-0D54DD81531F}";
        public const string CityId = "{CFC8C6E3-9FF3-4A08-A32E-3950D51BC305}";

        [SitecoreField(FieldName = AddressLine1Id)]
        public virtual string AddressLine1 { get; set; }

        [SitecoreField(FieldName = AddressLine2Id)]
        public virtual string AddressLine2 { get; set; }

        [SitecoreField(FieldName = ZipCodeId)]
        public virtual string ZipCode { get; set; }

        [SitecoreField(FieldName = LatitudeId)]
        public virtual string Latitude { get; set; }

        [SitecoreField(FieldName = LongitudeId)]
        public virtual string Longitude { get; set; }

        [SitecoreField(FieldName = CityId)]
        public virtual string City { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.DropTree, FieldId = StateId, Setting = SitecoreFieldSettings.InferType)]
        public virtual State State { get; set; }
    }
}