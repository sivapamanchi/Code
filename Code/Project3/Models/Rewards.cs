using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using static BGSitecore.Validator.TranslatedValidator;
using System.Web.Mvc;
using BGSitecore.Models.Resort;

namespace BGSitecore.Models
{
    //[SitecoreType(AutoMap = true)]
    public class Rewards : BaseComponent
    {
       //Rewards RailContent/Right panel Elements
        public const string NeedAssistanceId = "{66F73486-822A-4849-BD30-65EE5F99031C}";
        public const string HelpfulLinksId = "{C1BC4227-B0C1-47AA-813B-84D6CE4A4720}";
           
        //Right Panel Fields
        [SitecoreField(FieldName = NeedAssistanceId)]
        public virtual string NeedAssistance { get; set; }

        [SitecoreField(FieldName = HelpfulLinksId)]
        public virtual string HelpfulLinks { get; set; }

        [SitecoreField(FieldName = SitecoreItemReferences.OptionalBannerId)]
        public virtual IEnumerable<Image> PromotionalBanner { get; set; }

       

    }

   
    public class ReferFriend 
    {

        [SitecoreQuery("/sitecore/content//*[@@templatename='Resort Page']", IsRelative = false)]
        public virtual IEnumerable<ResortDetails> AllResorts { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_EmailRequired")]
        [RegularExpressionTranslated(@"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$", ErrorMessage = "Profile_EmailRequired")]
        public string txtEmail { get; set; }

        [SitecoreIgnore]
        [RequiredTranslated(ErrorMessage = "Profile_PhoneNotNumeric")]
        [RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtPhone { get; set; }

        [SitecoreIgnore]
        //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtFirstName { get; set; }

        [SitecoreIgnore]
        //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtLastName { get; set; }

        [SitecoreIgnore]
        //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtCity { get; set; }

        [SitecoreIgnore]
        //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtSalesRep { get; set; }


        [SitecoreIgnore]
        //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
        public string txtMessage { get; set; }
        
        [SitecoreIgnore]
        public string btnSubmit { get; set; }

        [SitecoreIgnore]
        public string txtState { get; set; }

        [SitecoreIgnore]
        public string txtDestination { get; set; }

        [SitecoreIgnore]
        public string TOC { get; set; }

        [SitecoreIgnore]
        public bool IsSendMailChecked { get; set; }

        [SitecoreIgnore]
        public string SuccessMessage { get; set; }


    }

}