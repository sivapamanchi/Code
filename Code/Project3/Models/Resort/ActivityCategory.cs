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
    public class ActivityCategory
    {
        public const string DisplayNameId = "{75082A1A-0CEE-40BD-A297-C991FE8CB041}";
        public const string NoteId = "{7128DDDE-B6DC-4399-838B-ACEE5C7159CB}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public virtual string FullPath { get; set; }


        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string Name { get; set; }

        [SitecoreField(FieldName = NoteId)]
        public virtual string Note { get; set; }


        [SitecoreQuery("./*[@@templateid='{1A57B7DD-173E-41DE-8CA7-4AC2C70A426D}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ActivityDetail> AllActivitiesDetails { get; set; }

    }
}