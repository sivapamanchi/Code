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
    public class MenuItem 
    {
        public const string MenuNameId = "{16D4A11B-C0D1-4A75-B39E-1533648CAC54}";
        public const string MenuDescriptionId = "{CCB186E3-33C2-4FE0-B35C-688E1530BA32}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField(FieldName = MenuNameId)]
        public virtual string MenuName { get; set; }

        [SitecoreField(FieldName = MenuDescriptionId)]
        public virtual string MenuDescription { get; set; }



    }
}