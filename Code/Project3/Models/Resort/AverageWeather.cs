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
    public class AverageWeather
    {

        public const string MonthId = "{6930B05D-5724-4FEB-ACCF-A08DC2548EAA}";
        public const string HighId = "{2C6E76B6-CD82-40D7-BE58-83C6A807612F}";
        public const string LowId = "{6EBD2CCA-1302-41B4-BE5E-783BE50DDEE4}";
        public const string PrecipitationId = "{20511CB4-D15B-417F-B27E-82BB1A4A04B3}";
       [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = MonthId)]
        public virtual string Month { get; set; }

        [SitecoreField(FieldName = HighId)]
        public virtual string High { get; set; }

        [SitecoreField(FieldName = LowId)]
        public virtual string Low { get; set; }

        [SitecoreField(FieldName = PrecipitationId)]
        public virtual string Precipitation { get; set; }
    }
}