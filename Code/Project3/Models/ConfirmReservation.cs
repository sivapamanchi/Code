using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{

    public class ConfirmReservation : BasePage
    {
        public const string InstructionalTextId = "{1D4116A4-C9E7-4490-90C0-00FEE3D512CF}";
        [SitecoreField(FieldName = InstructionalTextId)]
        public virtual string InstructionalText { get; set; }

        public const string SpecialRequestInstructionsId = "{FD549DCC-8745-4DAC-AF2F-DD802DE39EA1}";
        [SitecoreField(FieldName = SpecialRequestInstructionsId)]
        public virtual string SpecialRequestInstructions { get; set; }

        public const string TermsAndConditionsId = "{9E4F78DA-8CC7-4E91-9F14-2C2AB1B91990}";
        [SitecoreField(FieldName = TermsAndConditionsId)]
        public virtual string TermsAndConditionsSampler { get; set; }

        public const string TermsAndConditionsGlobalId = "{9D2EAF22-A5D7-4039-BDFA-F06C37960E24}";
        [SitecoreField(FieldName = TermsAndConditionsGlobalId)]
        public virtual string TermsAndConditionsGlobal { get; set; }

        public const string RequiredFieldsId = "{DDC5F022-6F9A-4DD3-934A-251C5F87D2DC}";
        [SitecoreField(FieldName = RequiredFieldsId)]
        public virtual string RequiredFields { get; set; }

        public const string FootnotesId = "{863ACEC9-56FF-40CD-B90A-0634414E4BBE}";
        [SitecoreField(FieldName = FootnotesId)]
        public virtual string Footnotes { get; set; }

        public const string ReservationInformationId = "{0370854C-4531-42CC-852E-AC592F60A629}";
        [SitecoreField(FieldName = ReservationInformationId)]
        public virtual string ReservationInformation { get; set; }

  
        public const string SpecialRequestsId = "{86C99309-4318-4B4C-93F6-8EF1F95BD27E}";
        [SitecoreField(FieldName = SpecialRequestsId)]
        public virtual string SpecialRequests { get; set; }

  
        public const string ConfirmMyReservationId = "{C86E8C8F-A03B-4857-B343-B02B53778601}";
        [SitecoreField(FieldName = ConfirmMyReservationId)]
        public virtual string ConfirmMyReservation { get; set; }

        public const string TermsAndConditionsAlertId = "{2E352B8F-8E88-4976-90DB-DF7D7CC38428}";
        [SitecoreField(FieldName = TermsAndConditionsAlertId)]
        public virtual string TermsAndConditionsAlert { get; set; }

        public const string ProceedWithoutEmailId = "{01E2FC27-DE95-47AC-A9CE-1444840AFBFD}";
        [SitecoreField(FieldName = ProceedWithoutEmailId)]
        public virtual string ProceedWithoutEmail { get; set; }

        public const string ProceedWithoutEmailNoteId = "{DA0E609D-3CBC-4DD8-A749-606C6D9532C2}";
        [SitecoreField(FieldName = ProceedWithoutEmailNoteId)]
        public virtual string ProceedWithoutEmailNote { get; set; }

        public const string OwnerId = "{EAFB2F90-0271-4A99-A65E-EBE82F2B1A8F}";
        [SitecoreField(FieldName = OwnerId)]
        public virtual string Owner { get; set; }


        public List<string> CreditCardInfoErrors { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardNumber { get; set; }

        [Required]
        public string CreditCardType { get; set; }

        [Required]
        public string CreditCardExpDateMonth { get; set; }

        [Required]
        public string CreditCardExpDateYear { get; set; }


        public string CreditCardVerificationNumber { get; set; }
        public string CreditCardZipCode { get; set; }
        public bool InternationalZipCode { get; set; }
        public string btnSubmit { get; set; }
        public string text_SpecialRequests { get; set; }
    }
}