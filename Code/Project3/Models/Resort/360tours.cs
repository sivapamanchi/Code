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
    public class tours360
    {

        public const string ToursIdId = "{9EAF6107-1FD4-484A-A6ED-45D9D91F09EA}";
        public const string UnitTypeId = "{949B739F-668E-4C5E-B4AE-64832D7253CE}";

        [SitecoreField(FieldName = ToursIdId)]
        public virtual string ToursId { get; set; }

        [SitecoreField(FieldName = UnitTypeId)]
        public virtual string UnitType { get; set; }

    }
}