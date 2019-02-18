using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{

    public class GuestInfo
    {
        public const string OwnerId = "{C388B478-1961-41E4-9596-B702A494E4EF}";
        public const string GuestCheckingInId = "{0FFBC572-9255-4020-9A79-921D1556BDC5}";
        public const string NumberOfGuestId = "{807621F3-CC1F-49E4-9EF2-D630BA66879E}";
        public const string SpecialRequestId = "{6A36F898-D547-4565-BFC3-2E8BF223F797}";
        public const string NewGuestInformationId = "{7BC517EC-3A46-47E1-BE63-953CA084E056}";
        public const string GuestFirstNameId = "{FB7FC829-A854-4C9A-8599-9ECB4021086A}";
        public const string GuestLastNameId = "{4028C4B7-389D-4C03-AF2A-09BCC7541055}";
        public const string GuestEmailId = "{E545867D-08D2-415E-94F3-CB39DB90F4A4}";
        public const string GuestPhoneNumberId = "{C36B49BA-BB43-4570-BDF6-5D7A4A2BE17B}";
        public const string GuestCityId = "{7A44B1A4-7331-4E0A-B6CF-5ABD8F40A001}";
        public const string GuestStateId = "{D9B8A6F7-5D18-4DAB-A30F-2ED9147BBBF8}";
        public const string GuestRelationshipId = "{6954CEE4-AE9F-47E4-BEED-A5D297418761}";
        public const string GuestAddNewGuestId = "{6F5D9366-4090-454A-AAAE-E556BD7C6E65}";
        public const string CancelLabelId = "{ED979FB8-055D-4E39-A84E-F16E1099BC2A}";

        [SitecoreField(FieldName = OwnerId)]
        public virtual string Owner { get; set; }

        [SitecoreField(FieldName = GuestCheckingInId)]
        public virtual string GuestCheckingIn { get; set; }

        [SitecoreField(FieldName = NumberOfGuestId)]
        public virtual string NumberOfGuest { get; set; }

        [SitecoreField(FieldName = NewGuestInformationId)]
        public virtual string NewGuestInformation { get; set; }

        [SitecoreField(FieldName = GuestFirstNameId)]
        public virtual string GuestFirstName { get; set; }

        [SitecoreField(FieldName = GuestLastNameId)]
        public virtual string GuestLastName { get; set; }

        [SitecoreField(FieldName = GuestEmailId)]
        public virtual string GuestEmail { get; set; }

        [SitecoreField(FieldName = GuestPhoneNumberId)]
        public virtual string GuestPhoneNumber { get; set; }

        [SitecoreField(FieldName = GuestCityId)]
        public virtual string GuestCity { get; set; }

        [SitecoreField(FieldName = GuestStateId)]
        public virtual string GuestState { get; set; }

        [SitecoreField(FieldName = GuestRelationshipId)]
        public virtual string GuestRelationship { get; set; }

        [SitecoreField(FieldName = GuestAddNewGuestId)]
        public virtual string GuestAddNewGuest { get; set; }

        [SitecoreField(FieldName = CancelLabelId)]
        public virtual string CancelLabel { get; set; }

        //Guest Fields
        public string Guest_AddNew { get; set; }
        public string Guest_FirstName { get; set; }
        public string Guest_LastName { get; set; }
        public string Guest_Email { get; set; }
        public string Guest_PhoneNumber { get; set; }
        public string Guest_City { get; set; }
        public string Guest_State { get; set; }
        public string Guest_Relationship { get; set; }
        public string Guest_Selected { get; set; }
        public string Guest_NumberOfGuest { get; set; }
        public string text_SpecialRequests { get; set; }
    }
}