using BGO.OwnerWS;
using BGSitecore.Models.ResortService.GetUnitInfoResponse;
using BGSitecore.Models.ResortService.ReservationsList;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;
using System.Collections.Generic;
using System.Text;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
    public class ReservationConfirmation : BasePage
    {
        public const string IntroductionTextId = "{C6FAD24C-81D3-4C30-9356-439C34AB7E45}";
        [SitecoreField(FieldName = IntroductionTextId)]
        public virtual string IntroductionText { get; set; }

        public const string FootnotesId = "{D877F57D-B3AB-436F-A34A-380BC5E33F8A}";
        [SitecoreField(FieldName = FootnotesId)]
        public virtual string Footnotes { get; set; }

        public const string ImportantNotesId = "{4AD385CF-FBCD-4FE7-A9C0-CCEEEAA9FC9C}";
        [SitecoreField(FieldName = ImportantNotesId)]
        public virtual string ImportantNotes { get; set; }

        public const string ResortNotesId = "{608C38CA-6E52-4CE3-8862-8F34CC737C77}";
        [SitecoreField(FieldName = ResortNotesId)]
        public virtual string ResortNotes { get; set; }

        public const string ResortNotesBodyId = "{3107721B-70A9-42D8-BAD0-DD79B845B449}";
        [SitecoreField(FieldName = ResortNotesBodyId)]
        public virtual string ResortNotesBody { get; set; }

        public const string CancellationPolicyPointsId = "{19EC4892-EE39-4392-8A43-EF27F0EBB8CC}";
        [SitecoreField(FieldName = CancellationPolicyPointsId)]
        public virtual string CancellationPolicyPoints { get; set; }

        public const string CancellationPolicyBonusTimeId = "{E68297F6-627F-460E-A853-AAF4CE516FD5}";
        [SitecoreField(FieldName = CancellationPolicyBonusTimeId)]
        public virtual string CancellationPolicyBonusTime { get; set; }

        public const string ReservationInformationId = "{8BADC1E3-4533-44FB-A0E3-879A3F54D928}";
        [SitecoreField(FieldName = ReservationInformationId)]
        public virtual string ReservationInformation { get; set; }

        public const string OwnerId = "{F19B4B18-164F-43B7-B470-07CBFFAD8A7F}";
        [SitecoreField(FieldName = OwnerId)]
        public virtual string Owner { get; set; }

        public const string GuestCheckingInId = "{6B6C74E6-26D1-4AB0-ABDE-722B8AE2E30B}";
        [SitecoreField(FieldName = GuestCheckingInId)]
        public virtual string GuestCheckingIn { get; set; }

        public const string NumberOfGuestsId = "{58C8B712-8856-4BAE-84E2-2C0B9D46A7FB}";
        [SitecoreField(FieldName = NumberOfGuestsId)]
        public virtual string NumberOfGuests { get; set; }

        public const string SpecialRequestsId = "{5D701C80-2F87-42CD-AF42-0EA28EBE2879}";
        [SitecoreField(FieldName = SpecialRequestsId)]
        public virtual string SpecialRequests { get; set; }

        public const string EmailVacationReservationInformationTitleId = "{AA87B85B-4470-4E52-BC36-D7CFABBF3907}";
        [SitecoreField(FieldName = EmailVacationReservationInformationTitleId)]
        public virtual string EmailVacationReservationInformationTitle { get; set; }

        public const string EmailToId = "{855E7507-0269-4EBA-AFDD-EB5922418F8D}";
        [SitecoreField(FieldName = EmailToId)]
        public virtual string EmailTo { get; set; }

        public const string EmailToInstructionsId = "{3FFAE9DA-67EF-4012-A89A-2FD07AECB177}";
        [SitecoreField(FieldName = EmailToInstructionsId)]
        public virtual string EmailToInstructions { get; set; }

        public const string EmailSubjectLineId = "{7A78C2DD-DBD7-4F7A-8612-E54F9F1A35DC}";
        [SitecoreField(FieldName = EmailSubjectLineId)]
        public virtual string EmailSubjectLine { get; set; }

        public const string EmailMessageId = "{65C4C50F-4F3F-448C-ADD3-BC617D4F62F1}";
        [SitecoreField(FieldName = EmailMessageId)]
        public virtual string EmailMessage { get; set; }

        public const string SendEmailWithItineraryId = "{D49B9D73-2DC2-49C1-98F9-82223329B34F}";
        [SitecoreField(FieldName = SendEmailWithItineraryId)]
        public virtual string SendEmailWithItinerary { get; set; }

        public const string SendVacationReservationInformationId = "{7F99231D-095A-4C95-83DB-EFA75723FBE3}";
        [SitecoreField(FieldName = SendVacationReservationInformationId)]
        public virtual string SendVacationReservationInformation { get; set; }

        public const string EmailRequiredFieldsId = "{463CAA69-63C7-466C-8C5D-D29D61E1BDBA}";
        [SitecoreField(FieldName = EmailRequiredFieldsId)]
        public virtual string EmailRequiredFields { get; set; }

        public const string EditReservationInformationId = "{345982E1-83CA-4327-9283-2FF8634235A1}";
        [SitecoreField(FieldName = EditReservationInformationId)]
        public virtual string EditReservationInformation { get; set; }

        public const string SeeAllReservationsId = "{B37A0581-EDE5-4415-AD2D-B9240515670D}";
        [SitecoreField(FieldName = SeeAllReservationsId)]
        public virtual Link SeeAllReservations { get; set; }

        public const string AddThisReservationToMyCalendarId = "{CD7A18DA-5F51-44E4-A049-2809F4EEA203}";
        [SitecoreField(FieldName = AddThisReservationToMyCalendarId)]
        public virtual string AddThisReservationToMyCalendar { get; set; }

        public const string PrintMyReservationInformationId = "{DE0181C9-D915-4646-B3A4-0C88F540EAE1}";
        [SitecoreField(FieldName = PrintMyReservationInformationId)]
        public virtual string PrintMyReservationInformation { get; set; }

        public const string HeaderCancelReservationId = "{C34BB2D2-C085-40F8-989C-9514408E2858}";
        [SitecoreField(FieldName = HeaderCancelReservationId)]
        public virtual Link HeaderCancelReservation { get; set; }

        public const string HeaderReservationConfirmedId = "{949D0368-AF45-4DB3-AE3C-F3FD0A664378}";
        [SitecoreField(FieldName = HeaderReservationConfirmedId)]
        public virtual string HeaderReservationConfirmed { get; set; }

        public const string HeaderConfirmationNumberId = "{225E10EE-2463-475E-82AC-2A2C54C29879}";
        [SitecoreField(FieldName = HeaderConfirmationNumberId)]
        public virtual string HeaderConfirmationNumber { get; set; }

        public const string HeaderPointsUsedId = "{311F418B-28CE-4CEC-84B8-B512446BF856}";
        [SitecoreField(FieldName = HeaderPointsUsedId)]
        public virtual string HeaderPointsUsed { get; set; }

        public const string HeaderPointsProtectedId = "{1F691DDF-8894-461D-9444-EC47EF011892}";
        [SitecoreField(FieldName = HeaderPointsProtectedId)]
        public virtual string HeaderPointsProtected { get; set; }

        public const string HeaderPointsNotProtectedId = "{EC0A1FC6-CCC0-4392-99AA-4C3D42B0337B}";
        [SitecoreField(FieldName = HeaderPointsNotProtectedId)]
        public virtual string HeaderPointsNotProtected { get; set; }

        public const string HeaderProtectPointsNowId = "{FB8CA75C-FA9D-43D5-901E-EB656198FD41}";
        [SitecoreField(FieldName = HeaderProtectPointsNowId)]
        public virtual Link HeaderProtectPointsNow { get; set; }

        public const string HeaderNotesId = "{81DE2618-BEE0-4F9D-BF06-14D0ED298C3D}";
        [SitecoreField(FieldName = HeaderNotesId)]
        public virtual string HeaderNotes { get; set; }

        public const string HeaderNoReservationFoundId = "{BEC3103E-F6D5-4834-ABA5-298BBCB2DB69}";
        [SitecoreField(FieldName = HeaderNoReservationFoundId)]
        public virtual string HeaderNoReservationFound { get; set; }

        public const string NewGuestInformationId = "{5BB1D51E-23D7-4BD5-890F-0CF638CD3306}";
        [SitecoreField(FieldName = NewGuestInformationId)]
        public virtual string NewGuestInformation { get; set; }

        public const string NewGuestFirstNameId = "{2D6F49BE-0720-4136-BFAC-72E59278D646}";
        [SitecoreField(FieldName = NewGuestFirstNameId)]
        public virtual string NewGuestFirstName { get; set; }

        public const string NewGuestLastNameId = "{BCEEB0C8-AF2D-40DB-91D7-8FA0CB304800}";
        [SitecoreField(FieldName = NewGuestLastNameId)]
        public virtual string NewGuestLastName { get; set; }

        public const string NewGuestEmailId = "{803C093F-EF47-4AC6-93FE-8CA9FBFC314F}";
        [SitecoreField(FieldName = NewGuestEmailId)]
        public virtual string NewGuestEmail { get; set; }

        public const string NewGuestTelephoneNumberId = "{0EAD9D27-BB30-4145-864D-08877D417047}";
        [SitecoreField(FieldName = NewGuestTelephoneNumberId)]
        public virtual string NewGuestTelephoneNumber { get; set; }

        public const string NewGuestCityId = "{A48A4D80-A77B-4C35-893E-F768E24ACD32}";
        [SitecoreField(FieldName = NewGuestCityId)]
        public virtual string NewGuestCity { get; set; }

        public const string NewGuestStateId = "{8AE877E7-0A08-48C6-AAE4-3B533EA56DE0}";
        [SitecoreField(FieldName = NewGuestStateId)]
        public virtual string NewGuestState { get; set; }

        public const string NewGuestRelationshipId = "{10269708-82D4-445A-B915-41FC8A5C43BC}";
        [SitecoreField(FieldName = NewGuestRelationshipId)]
        public virtual string NewGuestRelationship { get; set; }

        public const string NewGuestUpdateReservationInformationId = "{CE2CDE0E-81EE-4EE2-81F3-96C1B7FAEB63}";
        [SitecoreField(FieldName = NewGuestUpdateReservationInformationId)]
        public virtual string NewGuestUpdateReservationInformation { get; set; }

        public const string NewGuestCancelId = "{51B99D75-CA21-407D-85E7-15B1997B4C69}";
        [SitecoreField(FieldName = NewGuestCancelId)]
        public virtual string NewGuestCancel { get; set; }

        public const string NewGuestAddNewGuestId = "{119ACE5B-F922-4BD8-A576-7D85C3B9947E}";
        [SitecoreField(FieldName = NewGuestAddNewGuestId)]
        public virtual string NewGuestAddNewGuest { get; set; }

        public const string NewGuestRequiredFieldsId = "{3C2049BD-E60C-4D74-837A-DE8434406184}";
        [SitecoreField(FieldName = NewGuestRequiredFieldsId, Setting = SitecoreFieldSettings.RichTextRaw)]
        public virtual string NewGuestRequiredFields { get; set; }

        public const string EmailorPhoneMissingMessageId = "{E3FD5EF2-BCF8-48BD-A398-91E664537334}";
        [SitecoreField(FieldName = EmailorPhoneMissingMessageId)]
        public virtual string EmailorPhoneMissingMessage { get; set; }

        public const string ProceedWithoutEmailLinkId = "{3A2BD24B-185F-444A-8BAE-9DA164ECB8F7}";
        [SitecoreField(FieldName = ProceedWithoutEmailLinkId)]
        public virtual Link ProceedWithoutEmailLink { get; set; }

        public const string UpdateSuccessfulMessageId = "{80EF3E2B-13E3-4B11-A4EA-E934721D18DB}";
        [SitecoreField(FieldName = UpdateSuccessfulMessageId)]
        public virtual string UpdateSuccessfulMessage { get; set; }


        public Reservation ActiveReservation { get; set; }
        public GetUnitInfoResponse UnitInfo { get; set; }
        public Owner BxgOwner { get; set; }
        public double TotalOrderAmount { get; set; }
        public double taxTotal { get; set; }
        public List<BGSitecore.Models.ResortService.ScreeningBookReservationResponse.Guest> allGuest { get; set; }

        //Guest Fields
        public string Guest_AddNew { get; set; }
        public string Guest_Id { get; set; }
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

        public bool CanCancelReservation()
        {
            bool result = false;

            if (ActiveReservation != null)
            {
                if (ActiveReservation.ReservationType != "Q" && ActiveReservation.ReservationType != "V" && ActiveReservation.ReservationType != "F" 
                    && ActiveReservation.ReservationType != "X" && ActiveReservation.ReservationType != "G")
                {
                    if (!ReservationUtils.isExchangeReservation(ActiveReservation.ExchangeCode))
                    {
                        DateTime checkinDate = DateTime.Parse(FormatUtils.GetDate(ActiveReservation.CheckInDate));
                        result = checkinDate >= DateTime.Now;
                    }
                }
                
            }
            return result;
        }

        public bool CanEditReservation()
        {
            bool result = false;

            if (ActiveReservation != null)
            {
                if (ActiveReservation.ReservationType != "G" && ActiveReservation.ReservationType != "Q" && ActiveReservation.ReservationType != "V" 
                && ActiveReservation.ReservationType != "F" && ActiveReservation.ReservationType != "X")
                {
                    DateTime checkinDate = DateTime.Parse(FormatUtils.GetDate(ActiveReservation.CheckInDate));

                    if (!ReservationUtils.isExchangeReservation(ActiveReservation.ExchangeCode))
                    {
                        result = checkinDate >= DateTime.Now;
                    }
                   
                }

            }
            return result;
        }

        public bool CanProtectPoints()
        {
            return ReservationUtils.CanProtectPoints(ActiveReservation);
        }

    }
}