using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ReservationModel
    {

        public String ResortName { get; set; }
        public int ProjectStay { get; set; }
        public String ReservationNumber { get; set; }
        public int ResortNo { get; set; }
        public String CheckInDate { get; set; }
        public String CheckOutDate { get; set; }
        public int NumberOfNights { get; set; }
        public String VillaType { get; set; }
        public String ReservationType { get; set; }
        public String Points { get; set; }
        public String PPPStatus { get; set; }

        //TODO Hidden fields
        public String GuestFullName { get; set; }
        public String NumberOfAdults { get; set; }
        public String AmountDue { get; set; }
        public String PolicyPrice { get; set; }
        public String AS400UnitType { get; set; }

    }

}