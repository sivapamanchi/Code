using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Reward
{
    public class RightPanel : BaseComponent
    {
        [SitecoreQuery("./*[@@templateid='{DD579E2A-C84E-4BED-A1B4-8DE184EC8A6B}']", IsRelative = true)]
        public virtual IEnumerable<RightSection> RightSections { get; set; }

        [SitecoreIgnore]
        public virtual int RewardCartItemCount { get; set; }
    }
}