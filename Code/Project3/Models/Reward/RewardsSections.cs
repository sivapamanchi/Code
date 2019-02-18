using BGSitecore.Models;
using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models.Reward
{
    public class RewardsSections : BaseComponent
    {
   
        [SitecoreQuery("./*[@@templateid='{B3C38DB1-FB3C-40D0-B23C-045FEBBAB775}']", IsRelative = true)]
        public virtual IEnumerable<Section> AllSections { get; set; }

        [SitecoreIgnore]
        public virtual List<CartItem> cartItemsList { get; set; }

        [SitecoreIgnore]
        public virtual CartTotal CartTotal { get; set; } = new CartTotal();

    }
}