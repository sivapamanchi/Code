using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BGModern.Classes;

namespace BGModern.Models
{
    public enum ReservationType
    {
        Points,
        BonusTime
    }

    public enum SearchType
    {
        Destination,
        Date
    }

    public class PlanItSearchModel
    {
        // Owner info
        public bool IsSamplerOwner { get; set; }
        public string HomeProject { get; set; }
        public bool AllowBookIt { get; set; }

        // Search parameters
        public SearchType SearchType { get; set; }
        public ReservationType ReservationType { get; set; }
        public string DestinationCity { get; set; }
        public string DestinationState { get; set; }
        public IList<int> DestinationResortIDs { get; set; }
        public int ReservationMonth { get; set; }
        public int ReservationYear { get; set; }
        public int LengthOfStay { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public bool IsWheelchairAccessible { get; set; }
        public bool LimitToCurrentResort { get; set; }

        public string SelectedResortId { get; set; }
        //public string UnitTypeCodes { get; set; }

        // Search results
        public ValidationSummary ValidationSummary { get; set; }
        public ValidationSummary TemporaryValidationSummary { get; set; }
        public List<UnitType> AvailableUnitTypes { get; set; }
        public List<ResortStateModel> AvailableStates { get; set; } // Resorts by state, for date search
        public List<ResortInfoModel> AvailableResorts { get; set; } // Straight list of resorts, for destination search
        public List<ResortFilterModel> FilterResorts { get; set; }
        public List<string> CheckInDates { get; set; }              // Convenient list for highlighting check-in dates on the calendar
        public string MaxSearchDate { get; set; }
        public List<AvailableUnitTypesByDateAndResortModel> ResortUnitTypesByDate { get; set; } // list for identifying available unit types by check-in dates

        public PlanItSearchModel()
        {

        }
    }
}