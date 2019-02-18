using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models
{
    [Serializable]
    public class Holiday
    {
        public const string DisplayNameId = "{55EA9575-6140-4B74-A696-C23252A7772C}";
        public const string HolidayDateId = "{1E1A5F14-8AE2-47DB-BD0B-7B5D7BC9B1A3}";
  
        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        [SitecoreField(FieldName = HolidayDateId)]
        public virtual DateTime HolidayDate { get; set; }

    }
}