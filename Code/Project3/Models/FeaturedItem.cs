using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models
{
    public class FeaturedItem : BasePage
    {

        public const string CaptionId = "{1E820648-40D7-43DD-818D-EFC7551F5025}";
        public const string ContentId = "{F27A7E52-07CA-401D-9A01-4A1F78A60F41}";
        public const string ImageId = "{B311FF9E-F945-45F1-93C6-46796C4D4932}";
        public const string RedirectLinkId = "{8B0ED478-D881-4614-B8E6-F51D94F7AD0A}";
        public const string CategoryId = "{38AE0F4B-D7D3-4E70-A832-602D58B3AF97}";
        public const string ActiveId = "{663DB6D4-606F-4A95-9E39-F29370F8E364}";
        public const string AccessibleHomePageId = "{3C8E1660-4ED5-4A77-BF38-A32315DD61EC}";
        public const string DisplayOnLandingId = "{14CDD22A-D466-4C64-B970-1B45DED83A5D}";
        public const string StartDateId = "{B9BE5D04-DFFB-4788-95A0-9BF6BD08BC7E}";
        public const string EndDateId = "{FDFFDCE0-3891-46DE-809D-C44E3B3AA8D0}";
        public const string GalleryImageCaptionId = "{52EFA5E7-A202-4872-9C27-21804F219B46}";

        [SitecoreField(FieldName = CaptionId)]
        public virtual string Caption { get; set; }

        [SitecoreField(FieldName = ContentId)]
        public virtual string Content { get; set; }

        [SitecoreField(FieldName = ImageId)]
        public virtual BGSitecore.Models.Image FeaturedImage { get; set; }

        [SitecoreField(FieldName = RedirectLinkId)]
        public virtual Link RedirectLink { get; set; }

        [SitecoreField(FieldName = CategoryId)]
        public virtual string Category { get; set; }

        [SitecoreField(FieldName = ActiveId)]
        public virtual bool Active { get; set; }

        [SitecoreField(FieldName = AccessibleHomePageId)]
        public virtual bool AccessibleHomePage { get; set; }

        [SitecoreField(FieldName = DisplayOnLandingId)]
        public virtual bool DisplayOnLanding { get; set; }

        [SitecoreField(FieldName = StartDateId)]
        public virtual DateTime StartDate { get; set; }

        [SitecoreField(FieldName = EndDateId)]
        public virtual DateTime EndDate { get; set; }

        [SitecoreField(FieldName = GalleryImageCaptionId)]
        public virtual string GalleryImageCaption { get; set; }
    }
}