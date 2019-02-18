using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    [Serializable]
    public class SearchParameters
    {
        public const string RESERVATION_TYPE_POINTS = "1";
        public const string RESERVATION_TYPE_BONUSTIME = "2";

        public string ReservationType { get; set; }
       // public bool WheelchairAccessible { get; set; }
        public string Destination { get; set; }
        public string DestinationDisplayName { get; set; }
        public string DestinationBonusTime { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
		public DateTime MonthSearch { get; set; }
        public string ResortId { get; set; }
		public string monthsearchduration { get; set; }
		
		public bool ShowAvailabilityOnly { get; set; }
        public bool ShowResortOnly { get; set; }

        public virtual IEnumerable<Holiday> HolidayRestrictions { get; set; }

        public double Duration
        {
            get
            {
                return (CheckOutDate - CheckInDate).TotalDays;
            }
        }
    }
}