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
    public class Amenities
    {
        public const string templateId = "{8F6CE673-D257-45DB-8D25-FD8F1085A643}";

        public const string DisplayNameId = "{BB29B267-8543-4112-B4D4-66A616B2768B}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }



    }
}