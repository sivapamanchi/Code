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
    public class PointsCategory
    {
        public const string NameId = "{10A0D188-FDF3-4BFD-922E-A52BC29343B4}";
        public const string NoteId = "{3612FF6C-A12F-4FC7-B54A-2B3EFA2B1DD8}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public virtual string FullPath { get; set; }


        [SitecoreField(FieldName = NameId)]
        public virtual string Name { get; set; }

        [SitecoreField(FieldName = NoteId)]
        public virtual string Note { get; set; }

        [SitecoreQuery("./*[@@templateid='{35320B8C-77D7-4D3D-8955-1653DC20B5E4}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<PointsWeekDetails> AllPointsDetails { get; set; }

    }
}