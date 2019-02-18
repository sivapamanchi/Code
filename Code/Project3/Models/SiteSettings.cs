using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;

namespace BGSitecore.Models
{
    public class SiteSettings
    {
        public const string SignInPageId = "{DD68F99F-D703-49EC-AF34-8A43C2AD0AB7}";
        public const string SignInWaitPageId = "{205DA7D4-C51F-48C6-8411-371850DBEC05}";
        public const string SignInHelpPageId = "{3F214EDA-2AA1-4D9D-9B93-E4A2B7ACCC8F}";
        public const string SingInLearnMorePageId = "{181429F5-11E3-4CAE-ADCD-314E4B24F478}";
        public const string ForgotPasswordPageId = "{EABB0562-3930-4DC7-87F1-BF0BCACCDE2D}";
        public const string RegistrationPageId = "{D2850EAE-530F-4D8C-9338-A7A1362544AB}";
        public const string SignOutPageId = "{77FB6E57-0B80-43E4-ADBB-44E2BE9CBC3F}";
        public const string IndexPageId = "{A994D9F5-A04E-4502-8CEA-2774BAC2AA31}";
        public const string RemoteImageUrlId = "{7886D9FE-C3D6-4EF2-958D-8CE17F3E87B9}";
        public const string RemoteVideoUrlId = "{607063E7-F6AD-455C-967B-96C2C4B0BCF9}";
        public const string ReservationConfirmationPageId = "{AB753712-FECF-4E59-AC26-4D2BD34B052C}";
        public const string PointsProtectionPlanPageId = "{0A1AEF3B-C6EB-4D39-AB39-8ED280086D93}";
        public const string PremierWaitListDetailPageId = "{069A8B98-5F42-48E0-A70A-BABD4DB45B47}";
        
        [SitecoreField(FieldName = SignInPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SignInPage { get; set; }

        [SitecoreField(FieldName = SignInWaitPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SignInWaitPage { get; set; }

        [SitecoreField(FieldName = SignInHelpPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SignInHelpPage { get; set; }

        [SitecoreField(FieldName = ForgotPasswordPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link ForgotPasswordPage { get; set; }

        [SitecoreField(FieldName = RegistrationPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link RegistrationPage { get; set; }

        [SitecoreField(FieldName = SignOutPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link SignOutPage { get; set; }

        [SitecoreField(FieldName = IndexPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link IndexPage { get; set; }

        [SitecoreField(FieldName = PremierWaitListDetailPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link PremierWaitListDetailPage { get; set; }

        [SitecoreField(FieldName = RemoteImageUrlId)]
        public virtual string RemoteImageUrl { get; set; }

        [SitecoreField(FieldName = RemoteVideoUrlId)]
        public virtual string RemoteVideoUrl { get; set; }

        [SitecoreField(FieldName = ReservationConfirmationPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link ReservationConfirmationPage { get; set; }

        [SitecoreField(FieldName = PointsProtectionPlanPageId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link PointsProtectionPlanPage { get; set; }
    }
}