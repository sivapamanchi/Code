using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    [SitecoreType(AutoMap = true)]
    public class ClubAffiliation
    {

        [SitecoreId]
        public virtual Guid Id { get; set; }

        public const string DisplayNameId = "{00E73E70-F0F9-444B-9112-8A9182BD34E6}";
        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        public const string ColorId = "{37874F1C-069C-4CBC-A2A2-1406CE223D0B}";
        [SitecoreField(FieldName = ColorId)]
        public virtual string Color { get; set; }

        public const string DetailsId = "{EC69F266-ED38-4078-A914-4E3CD78E0338}";
        [SitecoreField(FieldName = DetailsId)]
        public virtual string Details { get; set; }
    }
}