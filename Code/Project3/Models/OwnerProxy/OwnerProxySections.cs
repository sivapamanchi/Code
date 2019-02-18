using BGSitecore.Models;
using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models.OwnerProxy
{
    public class OwnerProxySections : BaseComponent
    {
   
        [SitecoreQuery("./*[@@templateid='{B3C38DB1-FB3C-40D0-B23C-045FEBBAB775}']", IsRelative = true)]
        public virtual IEnumerable<OwnerProxySection> AllSections { get; set; }

    }

    //[SitecoreType(AutoMap = true)]
    //public class ReferFriendChild 
    //{
       
    //    //Form Elements 
    //    [SitecoreIgnore]
    //    [RequiredTranslated(ErrorMessage = "Profile_EmailRequired")]
    //    [RegularExpressionTranslated(@"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$", ErrorMessage = "Profile_EmailRequired")]
    //    public string txtEmail { get; set; }

    //    [SitecoreIgnore]
    //    [RequiredTranslated(ErrorMessage = "Profile_PhoneNotNumeric")]
    //    [RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
    //    public string txtPhone { get; set; }

    //    [SitecoreIgnore]
    //    //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
    //    public string txtFirstName { get; set; }

    //    [SitecoreIgnore]
    //    //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
    //    public string txtLastName { get; set; }

    //    [SitecoreIgnore]
    //    //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
    //    public string txtCity { get; set; }

    //    [SitecoreIgnore]
    //    //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
    //    public string txtSalesRep { get; set; }


    //    [SitecoreIgnore]
    //    //[RegularExpressionTranslated(@"([0-9]+)", ErrorMessage = "Profile_PhoneNotNumeric")]
    //    public string txtMessage { get; set; }


    //    [SitecoreIgnore]
    //    public string btnSubmit { get; set; }

    //    public string State { get; set; }

    //    // [SitecoreField(FieldName = UnsuccessMessageId)]
    //    [AllowHtml]
    //    public virtual string UnsuccessMessage { get; set; }

    //}
}