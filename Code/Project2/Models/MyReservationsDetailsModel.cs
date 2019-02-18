using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class MyReservationsDetailsModel
    {
        //dropdowns
        public List<string> RelationshipTypes { get; set; }
        public List<string> States { get; set; }

        //Reservation Details
        public string ReservationNo { get; set; }
        public string GuestName { get; set; }
        public string MaxOccupancy { get; set; }
        public string NumberOfAdults { get; set; }
        public string ReservationType { get; set; }
        public string PolicyStatus { get; set; }
        public string ConfirmationDate { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Points { get; set; }
        public string PayType { get; set; }
        public int NumberOfNights { get; set; }
        public double Amount { get; set; }

        //Resort Details
        public string ResortNo { get; set; }
        public string ResortName { get; set; }
        public string ResortAddress { get; set; }
        public string ResortCityState { get; set; }
        public string ResortPhone { get; set; }
    }
}