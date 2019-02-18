using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using SC = Sitecore;

namespace BGSitecore.Models
{
    public class MenuItem
    {
        // Base SiteCore attributes
        public virtual string Name { get; set; }
        public virtual Item Parent { get; set; }
        public virtual string Url { get; set; }
        public bool ShowMenu { get; set; }

        [SitecoreItem]
        public virtual Item InnerItem { get; set; }

        // BG Specific attributes
        [SitecoreField(FieldName = SitecoreReferenceMenuItem.MainMenuDisplayName)]
        public virtual string MainMenuDisplayName { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceMenuItem.ItemType)]
        public virtual string ItemType { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceMenuItem.ExternalUrl)]
        public virtual Link ExternalUrl { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceMenuItem.ShowInNavigation)]
        public virtual string ShowInNavigation { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceMenuItem.AdditionalNavigationRules)]
        public virtual string AdditionalNavigationRules { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.PageSummary)]
        public virtual string PageSummary { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceMenuItem.Divider)]
        public virtual bool Divider { get; set; }

        public virtual IEnumerable<MenuItem> Children { get; set; }

        public Image Logo { get; set; }
        public string RemoteImageUrl { get; set; }

        public string TopMenu = "<li class='has-flyout open-right'><a href='#'>TBD</a></li>";
        public string MyBlueGreen = "<li class='has-flyout open-right'><a href='#'>TBD</a></li>";

        public string ToHtml(string filter)
        {
            int itemCount = 0;
            return MenuUtils.BuildBootstrapMenu(this, filter, ref itemCount);
        }
    }
}