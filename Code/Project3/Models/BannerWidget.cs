using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class BannerWidget : BaseComponent
    {
        public const string BannerSpeedId = "{5E980C15-1EA9-45B6-82A9-701CEFC7C9BF}";
        public const string AllBannerImagesId = "{BF1103FB-64DF-4578-8181-4D1A13F78AFD}";
        public const string VisibleFromDateId = "{15F0D511-5421-47BE-A051-F381DDFEF585}";
        public const string VisibleToDateId = "{5918DA83-EA5D-4B76-BE6D-291EB029839B}";

        [SitecoreField(FieldName = BannerSpeedId)]
        public virtual string BannerSpeed { get; set; }

        [SitecoreField(FieldName = AllBannerImagesId)]
        public virtual IEnumerable<BannerImage> AllBannerImages { get; set; }

        [SitecoreField(FieldName = VisibleFromDateId)]
        public virtual DateTime VisibleFromDate { get; set; }


        [SitecoreField(FieldName = VisibleToDateId)]
        public virtual DateTime VisibleToDate { get; set; }
    }
}