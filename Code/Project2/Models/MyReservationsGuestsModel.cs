using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class MyReservationsGuestsModel
    {
        public string GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestCategory { get; set; }

        public MyReservationsGuestsModel CreateGuest(string GuestId, string GuestName, string GuestCategory)
        {
            MyReservationsGuestsModel guest = new MyReservationsGuestsModel();
            guest.GuestId = GuestId;
            guest.GuestName = GuestName;
            guest.GuestCategory = GuestCategory;

            return guest;
        }
    }
}