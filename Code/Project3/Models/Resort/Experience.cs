using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class Experience
    {
        public const string ExperienceNameId = "{91188680-AC39-4DDF-91E5-230B26827FA4}";
        public const string ReferenceIdId = "{DCB4A8A0-C187-4290-8FE1-67A2F63BA0D3}";

        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = ExperienceNameId)]
        public virtual string ExperienceName { get; set; }

        [SitecoreField(FieldName = ReferenceIdId)]
        public virtual string ReferenceId { get; set; }
    }
}