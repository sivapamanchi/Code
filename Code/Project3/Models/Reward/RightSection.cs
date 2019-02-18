using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;
using System.Collections.Generic;


namespace BGSitecore.Models.Reward
{
    public class RightSection : BasePage
    {
        [SitecoreQuery("./*[@@templateid='{54DDFD1F-71AA-4AA7-91D6-80EA91DB1B79}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<RichText> AllRichText { get; set; }


        [SitecoreQuery("./*[@@templateid='{75B346B5-C4F6-4ADA-BB82-81F64E160A3B}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<OptionalBanner> AllBanners { get; set; }

        public const string enableRewardsCartId = "{89ED2628-2AD1-4350-8EC2-4098F8A496C3}";
        [SitecoreField(FieldName = enableRewardsCartId)]
        public virtual bool enableRewardsCart { get; set; }

        public bool HideSection { get; set; }
    }

    public class OptionalBanner
    {
        [SitecoreId]
        public virtual ID ItemId { get; set; }

        [SitecoreField(FieldName = "{310DE0D0-D8E5-4F84-931F-E985D189BAB6}")]
        public virtual IEnumerable<Image> PromotionalBanner { get; set; }
    }
}