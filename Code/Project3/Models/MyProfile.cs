using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{

    public class MyProfile : BasePage
    {
        public const string ContactInformationTitleId = "{1EBAF6F5-46B3-4F56-8883-3101D88F0CAE}";
        public const string updatemyprofileButtonId = "{EAEED434-F124-4958-941D-DEF6E79F87A9}";
        public const string submitselectedaddressButtonId = "{627747CF-0F83-4829-9122-7AB008BE3B41}";
        public const string RequiredLabeltextId = "{E3EEC13B-A623-41FD-8E4B-DD65C93F15BF}";
        public const string FootnotesId = "{8C08B5B0-0465-43BE-8218-4947F28F4ECF}";
        private const string UnsuccessMessageId = "{315E2C4D-A220-4A69-9F8D-FDA866DDCAD9}";
        private const string SuccessMessageId = "{57EC9BFD-DA0C-49B6-A810-DA54838D7EEA}";

        private const string SuggestionProfileTitleId = "{FC97F17F-E841-4313-ADE9-A4AA4DCAE732}";
        private const string SuggestionProfileDetailId = "{FA7BF425-E66F-40C9-BB12-60B5A7C0A262}";

        private const string ActualAddressTitleId = "{A23680F1-3093-44A0-A9F1-3CCB3A3A506F}";
        private const string SuggestedAddressTitleId = "{7787E485-D3AD-4AE4-99D0-34E86D0E3AF2}";
        private const string AlertInfoDescriptionId = "{3FDA4107-0C64-4B55-9624-9DB60F7800E7}";
        private const string AdditionalCardDescriptionId = "{98D62DF7-C340-4805-99D2-2C7E5B90D2FF}";
        private const string SuccessPageUrlId = "{B88B926D-3625-450F-A6A1-38872FAD0A63}";

        [SitecoreField(FieldName = ContactInformationTitleId)]
        public virtual string ContactInformationTitleLabel { get; set; }

        [SitecoreField(FieldName = updatemyprofileButtonId)]
        public virtual string updatemyprofileButtonLabel { get; set; }

        [SitecoreField(FieldName = submitselectedaddressButtonId)]
        public virtual string submitselectedaddressButtonLabel { get; set; }

        [SitecoreField(FieldName = RequiredLabeltextId)]
        public virtual string RequiredLabeltextLabel { get; set; }

        [SitecoreField(FieldName = FootnotesId)]
        public virtual string FootnotesLabel { get; set; }

        [SitecoreField(FieldName = AlertInfoDescriptionId)]
        [AllowHtml]
        public virtual string AlertInfoDescription { get; set; }

        [SitecoreField(FieldName = AdditionalCardDescriptionId)]
        [AllowHtml]
        public virtual string AdditionalCardDescription { get; set; }

        [SitecoreField(FieldName = SuccessMessageId)]
        [AllowHtml]
        public virtual string SuccessMessage { get; set; }

        [SitecoreField(FieldName = SuggestionProfileTitleId)]
        public virtual string SuggestionProfileLabel { get; set; }

        [SitecoreField(FieldName = SuggestionProfileDetailId)]
        public virtual string SuggestionProfileDetailLabel { get; set; }

        [SitecoreField(FieldName = ActualAddressTitleId)]
        public virtual string ActualAddressTitleLabel { get; set; }

        [SitecoreField(FieldName = SuggestedAddressTitleId)]
        public virtual string SuggestedAddressTitleLabel { get; set; }

        [SitecoreField(FieldName = UnsuccessMessageId)]
        [AllowHtml]
        public virtual string UnsuccessMessage { get; set; }

        [SitecoreField(FieldName = SuccessPageUrlId)]
        public virtual Link SuccessPageUrl { get; set; }

        [SitecoreIgnore]
        public string AddressEntered { get; set; }

        [SitecoreIgnore]
        public string AddressSuggested { get; set; }
        
        // Go Green Section
        public const bool GogreenOptedIn = true;
        public const bool GogreenOptedOut = false;

        [SitecoreIgnore]
        public bool isPaperLessSelected { get; set; }

        [SitecoreIgnore]
        public bool isEligibleForAdditionalMemberCard { get; set; } = false;

        public const string GoGreenTitleId = "{83249DF7-CA4F-443C-A043-33B69908240D}";
        [SitecoreField(FieldName = GoGreenTitleId)]
        public virtual string GoGreenTitle { get; set; }

        public const string GoGreenDescId = "{06CB3A35-59F6-4CEE-90B3-7A5052100625}";
        [SitecoreField(FieldName = GoGreenDescId)]
        public virtual string GoGreenDesc { get; set; }

        public const string GoGreenOptInFormTitleId = "{73E5439A-B7E0-4678-B922-95C815F85F2D}";
        [SitecoreField(FieldName = GoGreenOptInFormTitleId)]
        public virtual string GoGreenOptInFormTitle { get; set; }

        public const string GoGreenOptInId = "{CAE78621-CFDA-4969-8D06-80AD21C868B3}";
        [SitecoreField(FieldName = GoGreenOptInId)]
        public virtual string GoGreenOptIn { get; set; }

        public const string GoGreenOptOutId = "{8984F24C-EC29-488C-989E-71EC27E56706}";
        [SitecoreField(FieldName = GoGreenOptOutId)]
        public virtual string GoGreenOptOut { get; set; }

    }
}