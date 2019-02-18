using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;

namespace BGSitecore.Models
{

    //[SitecoreType(TemplateId = Constants.Templates.NavigationRoot)]
    public class Footer : BasePage
    {

        public const string AuthenticatedFooterId = "{7DD90ED4-2AE2-484C-825C-930137F8BD81}";
        public const string AnonymousFooterId = "{EBA106F3-E0E8-4C14-9156-2F471FD82466}";

        public const string SocialMediaLinksId = "{0E342882-555C-4610-BCEB-A968286467CC}";
        public const string CopyrightMessageId = "{24A01150-0E28-4C3A-B0E4-1B7CF483092C}";
        public const string TrademarkMessageId = "{CF86D622-5F1F-46B3-A641-3415F6C2C1E3}";
        public const string IsAnonymousId = "{861445A2-8102-434F-9502-8C393679D99D}";
        public const string FooterImageId = "{36A88EA8-C5C9-42DD-A302-AB3C216EAF5F}";
        public const string HousingOppertunityImageId = "{D3C6F7F8-6F7F-40DD-AF4A-CA9D3C8F8F76}";
        public const string AdvertizingMaterialId = "{89749D2B-C675-4098-92EF-22647486BD1D}";
    
        //[SitecoreQuery("ancestor-or-self::*[@#Include in breadcrumb#='1']", IsRelative = true)]
        //public virtual IEnumerable<Breadcrumb> Ancestors { get; set; }

        [SitecoreField(FieldName = SocialMediaLinksId)]
        public virtual string SocialMediaLinks { get; set; }

        [SitecoreField(FieldName = CopyrightMessageId)]
        public virtual string CopyrightMessage { get; set; }

        [SitecoreField(FieldName = TrademarkMessageId)]
        public virtual string TrademarkMessage { get; set; }

        [SitecoreField(FieldName = IsAnonymousId)]
        public virtual string IsAnonymous { get; set; }

        [SitecoreField(FieldName = FooterImageId)]
        public virtual Image FooterImage { get; set; }

        [SitecoreField(FieldName = HousingOppertunityImageId)]
        public virtual Image HousingOppertunityImage { get; set; }

        [SitecoreField(FieldName = AdvertizingMaterialId)]
        public virtual string AdvertizingMaterial { get; set; }

        [SitecoreChildren(InferType = true)]
        public virtual IEnumerable<MenuItem> Children { get; set; }

    }
}