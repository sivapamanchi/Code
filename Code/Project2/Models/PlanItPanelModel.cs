using BGModern.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class PlanItPanelModel
    {
        public bool IsEnabled { get; set; }
        public string SiteRoot { get; set; }
        public bool OnHomePage { get; set; }
        public bool ShowAvailabilityCount { get; set; }
        public IList<Destination> Destinations;
        public IList<int> NumberOfNights;
        public IList<DateTime> Months;
        public int ReservationType;
        public Boolean WheelchairAccessible;
        public bool IsSamplerOwner { get; set; }
        public string HomeProject { get; set; }
        public string FullWeekResorts { get; set; }
        public string PointsDestinations { get; set; }
        public string BonusTimeDestinations { get; set; }

        // Search parameters from other pages
        public string InitialDestination { get; set; }
        public int InitialLOS { get; set; }
    }
}