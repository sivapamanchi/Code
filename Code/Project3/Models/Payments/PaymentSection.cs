using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Payments
{
    public class PaymentSection : BasePage
    {
        public const string PrimaryButtonTextId = "{2C883DC8-751E-4776-9010-3580C8935455}";
        public const string PrimaryButtonLinkId = "{ACFC02D2-0790-44B2-B73A-8FA8BC4EBB32}";
        public const string SecondaryButtonLinkTextId = "{AF1C558A-CBB8-417A-8B29-DE41F597AE7D}";
        public const string SecondaryButtonLinkId = "{310C25D0-17EE-4E06-93E1-585BE36BB1FE}";
        public const string ARDATextId = "{CA9B6FF5-09FC-45FA-B820-0A357C7D51DF}";
        public const string FooterNotesId = "{0F027640-3C1B-413F-8E6C-157C66A918EB}";
        public const string ErrorMessageId = "{F254C2FF-3666-4967-BF75-062D0BB65EA6}";
        public const string DoesNotQualityMessageId = "{6620D476-A1EA-47EB-991C-7DC6454F1E02}";
        public const string AccountNotAllowedMessageId = "{753FC491-BD0A-4C80-8A70-2A27AD7F2AE0}";
        public const string PendingPaymentsErrorMessageId = "{58391D3B-0403-47E3-B8CF-06478D7693D6}";
        public const string ViewStatmentMaskedLinkId = "{D7FA3056-667E-4CAA-BCC3-5D39CBFC7F9B}";
        
        

    [SitecoreField(FieldName = PrimaryButtonTextId)]
        public virtual string PrimaryButtonText { get; set; }

        [SitecoreField(FieldName = PrimaryButtonLinkId)]
        public virtual Link PrimaryButtonLink { get; set; }

        [SitecoreField(FieldName = SecondaryButtonLinkTextId)]
        public virtual string SecondaryButtonLinkText { get; set; }

        [SitecoreField(FieldName = SecondaryButtonLinkId)]
        public virtual Link SecondaryButtonLink { get; set; }

        [SitecoreField(FieldName = ARDATextId)]
        public virtual string ARDAText { get; set; }

        [SitecoreField(FieldName = FooterNotesId)]
        public virtual string FooterNotes { get; set; }

        [SitecoreField(FieldName = ErrorMessageId)]
        public virtual string ErrorMessage { get; set; }

        [SitecoreField(FieldName = DoesNotQualityMessageId)]
        public virtual string DoesNotQualityMessage { get; set; }

        [SitecoreField(FieldName = AccountNotAllowedMessageId)]
        public virtual string AccountNotAllowedMessage { get; set; }

        [SitecoreField(FieldName = PendingPaymentsErrorMessageId)]
        public virtual string PendingPaymentsErrorMessage { get; set; }

        [SitecoreField(FieldName = ViewStatmentMaskedLinkId)]
        public virtual Link ViewStatmentMaskedLink { get; set; }
      

    }
}