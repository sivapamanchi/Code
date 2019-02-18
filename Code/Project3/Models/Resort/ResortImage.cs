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
    public class ResortImage : Image
    {
        public const string UnitTypeId = "{BBB3C8C3-0245-424C-BBCF-B6C5C32B79CC}";
        public const string ShowInSliderId   = "{E500C5BE-ECC8-498A-A7CC-8C6BD5CDCA90}";
      

        [SitecoreField(FieldName = UnitTypeId)]
        public virtual string UnitType { get; set; }

        [SitecoreField(FieldName = ShowInSliderId)]
        public virtual bool ShowInSlider { get; set; }

    }
}