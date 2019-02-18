using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
    [Serializable]
    public class ReservationParameters
    {

        public List<string> ValidationError { get; set; }


        public int OwnerId { get; set; }
        public int ResortId { get; set; }
        public int ProjectNumber { get; set; }
        public string UnitType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfNightChanged { get; set; }
        public string ReservationType { get; set; }
        public int PointsRequired { get; set; }
        public double BT_TotalCost { get; set; }
        public string SeasonCode { get; set; }
        public decimal PPPCost { get; set; }
        public decimal SPECost { get; set; }
        public string DailyPrice { get; set; }
        public string TaxRate { get; set; }
        public int MaxOccupancy { get; set; }
        public string wheelchairaccessible { get; set; }
        public string Itinerary { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string WarningMessage { get; set; }

        public string btnSubmit { get; set; }

        //Credit Card fields
        public bool hasCreditcard { get; set; }
        // [RequiredTranslated(ErrorMessage = "Profile_BillingNameRequired")] TFS-62805
        public string CreditCard_Name { get; set; }

        // [RequiredTranslated(ErrorMessage = "Profile_BillingCardNumberRequired")] TFS-62805
        public string CreditCard_Number { get; set; }

        public string CreditCard_Type { get; set; }
        public string CreditCard_ExpDateMonth { get; set; }
        public string CreditCard_ExpDateYear { get; set; }

        // [RequiredTranslated(ErrorMessage = "Profile_BillingCvvNumberRequired")] TFS-62805
        public string CreditCard_VerificationNumber { get; set; }

        // [RequiredTranslated(ErrorMessage = "Profile_BillingZipCodeRequired")] TFS-62805
        public string CreditCard_ZipCode { get; set; }

        public bool CreditCard_InternationalZipCode { get; set; }

       
        //Guest Fields
        public bool hasGuest { get; set; }
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

        //Address Info Fields
        public bool hasAddress { get; set; }
        public string Address_LastName { get; set; }
        public string Address_FirstName { get; set; }
        public string Address_AddressLine1 { get; set; }
        public string Address_AddressLine2 { get; set; }
        public string Address_AddressLine3 { get; set; }
        public string Address_City { get; set; }
        public string Address_State { get; set; }
        public string Address_ZipCode { get; set; }
        public string Address_Country { get; set; }

        public string Address_EmailAddress { get; set; }

        public string Address_PhoneNumber { get; set; }

        public string Address_AlternatePhoneNumber { get; set; }

        public int Duration { get; set; }
        public bool AcceptTermsAndConditions { get; set; }
        
    }
}