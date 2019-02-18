using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public enum BookStatusType
    {
        CannotBeDone = 0,
        CanBeBooked = 1,
        CanBeBookedwithSPE = 2,
        CanBeBookedwithFuture = 3,
        CanBeBookedwithFutureSPE = 4
    }

    [Serializable]
    public class ReservationVerification
    {
        public bool isValid { get; set; }

        public string ErrorMessage { get; set; }

        public string RedirectURL { get; set; }

        public string ReservationType { get; set; }

        public DateTime  CheckInDate { get; set; }

        public DateTime  CheckOutDate { get; set; }

        public decimal TotalPointsRequired { get; set; }

        public List<DailyRates> Rates { get; set; }

        public string TotalEligiblePoints { get; set; }
        public bool showFuturePointMessage { get; set; }
        public bool showSavePointsMessage { get; set; }

        public List<PointsStatus> Accounts { get; set; }

    }


    //TODO move this 
    [Serializable]
    public class DailyRates
    {
     
        public string DailyRate { get; set; }
        public string DayName { get; set; }
        public string CalendarDay { get; set; }
        public bool DaySelected { get; set; }
        public string SeasonName { get; set; }
        public string SeasonCss { get; set; }
        public string WeekNumber { get; set; }
        public DateTime CalendarDate { get; set; }
        public string DayNumber { get; set; }
        public string SeasonCode { get; set; }
        public string UnitType { get; set; }
    }

    [Serializable]
    public class PointsStatus
    {

        public string EligibilityMsg { get; set; }
        public string ExpDate { get; set; }
        public string IsEligible { get; set; }
        public string Points { get; set; }
        public string PointsType { get; set; }
    }
}