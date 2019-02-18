using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//This code was copied from the ReservationLibrary.dll
//This class is required to pass data from the MyReservation page to the Cancel page that is still in Legacy

using System.Diagnostics;

namespace ReservationLibrary
{
    [Serializable]
    public class ResortInfo
    {

        public string Address { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Desc { get; set; }
        public string ImageName { get; set; }
        public string MaxOccupancy { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string ResortID { get; set; }
        public string ResortName { get; set; }
        public string ShortDesc { get; set; }
        public string State { get; set; }
        public string TaxRate { get; set; }
        public string Zip { get; set; }
    }
}