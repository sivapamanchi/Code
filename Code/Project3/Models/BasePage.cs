using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    [SitecoreType(AutoMap =true)]
    public class BasePage
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreItem]
        public virtual Item InnerItem { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        public virtual Guid TemplateId { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public virtual string FullPath { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.PageRequireLogin)]
        public virtual bool PageRequireAuth { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.PageTitleId)]
        public virtual string PageTitle { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.HeaderTitle)]
        public virtual string HeaderTitle { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.PageSummary)]
        public virtual string PageSummary { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.MainMenuDisplayName)]
        public virtual string MainMenuDisplayName { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.BackGroundImage)]
        public virtual Image BackGroundImage { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.ShowChatNowLinkId)]
        public virtual bool ShowChatNowLink { get; set; }

        [SitecoreField(FieldName = SitecoreReferenceBasePage.DisableCachingId)]
        public virtual bool DisableCaching { get; set; }

        [SitecoreNode(Id = SitecoreItemReferences.FooterConfiguration)]
        public virtual Footer FooterConfiguration { get; set; }

        [SitecoreNode(Id = SitecoreItemReferences.HeaderConfiguration)]
        public virtual Header HeaderConfiguration { get; set; }

        [SitecoreNode(Id = SitecoreItemReferences.SiteConfiguration)]
        public virtual SiteSettings SiteSettings { get; set; }


    }
}