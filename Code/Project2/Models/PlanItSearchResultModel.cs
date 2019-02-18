using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class PlanItSearchResultModel
    {
        public string UnitsAvailable { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int LengthOfStay { get; set; }
        public string ProjectNumber { get; set; }
        public string UnitCode { get; set; }
        public string UnitType { get; set; }
        public string UnitNumber { get; set; }
        public string RoomDescription { get; set; }
        public string ViewDescription { get; set; }
        public bool HandicapAccessible { get; set; }
        public int MaximumOccupants { get; set; }
        public string SeasonName { get; set; }
        public int TotalPoints { get; set; }
        public double DailyRate { get; set; }
        public double TotalPrice { get; set; }
        public double TaxRate { get; set; }
        public List<BGO.availabilityservice.DailyRatesTwoWeeks> DailyRates;
    }
}