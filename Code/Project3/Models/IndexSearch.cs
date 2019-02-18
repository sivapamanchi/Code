using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    [SitecoreType(TemplateId = "{EFE90C81-A513-46E0-85BF-3385589FDDAD}")]
    public class IndexSearch : BasePage
    {
        public const string BackgroundImageListId = "{A2EA7BF7-690D-4BD5-B4B8-B80FA62EDCCC}";
        public const string EnableLegacySearchId = "{FB442FFE-8ED4-4EDD-9E84-A17DC8FFC6FD}";
        public const string TextLinkId = "{FA3D7001-D14D-4C23-8A08-E8A2E39B172C}";
        public const string RedirectLinkId = "{D8DA91B1-637D-4660-A5A9-7AD469F2D4C4}";

        [SitecoreField(FieldName = BackgroundImageListId)]
        public virtual IEnumerable<Image> RandomBackgroundImageList { get; set; }

        [SitecoreField(FieldName = EnableLegacySearchId)]
        public virtual bool EnableLegacySearch { get; set; }

        [SitecoreField(FieldName = TextLinkId)]
        public virtual string TextLink { get; set; }

        [SitecoreField(FieldName = RedirectLinkId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link RedirectLink { get; set; }

        [SitecoreIgnore]
        public Image BackgroundImage { get; set; }

        public SearchResult searchOptions { get; set; }

        public string UseBackgroundImageUrl { get; set; }

        public string BackgroundImageCaption { get; set; }

        public DateTime initialCheckInDate { get; set; }

        public DateTime initialCheckOutDate { get; set; }

        public List<SavedSearch> SavedSearches { get; set; }

    }
}