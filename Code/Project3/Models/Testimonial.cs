using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class Testimonial: BaseComponent
    {
        public const string ContentId = "{A9A28847-BC88-4BEA-9D8B-6C6A15099428}";
        [SitecoreField(FieldName = Testimonial.ContentId)]
        public virtual string Content { get; set; }

        public const string SignatureId = "{1DBAB1AA-0592-402C-ACB5-4833D0DBD46D}";
        [SitecoreField(FieldName = Testimonial.SignatureId)]
        public virtual string Signature { get; set; }

    }
}