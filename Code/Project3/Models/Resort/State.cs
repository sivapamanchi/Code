using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    [SitecoreType(AutoMap = true)]
    public class State
    {

        [SitecoreId]
        public virtual Guid Id { get; set; }

        public const string DisplayNameId = "{B57A28C2-1E21-46F3-901E-A0192344659D}";
        public const string CodeId = "{DB3425E8-745D-4BF4-A184-FB9DED7B2703}";
        public const string CountryId = "{6FEB222E-2207-459E-9EEA-9444BBA37F51}";


        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        [SitecoreField(FieldName = CodeId)]
        public virtual string Code { get; set; }

        [SitecoreField(FieldName = CountryId)]
        public virtual string Country { get; set; }

       
    }
}