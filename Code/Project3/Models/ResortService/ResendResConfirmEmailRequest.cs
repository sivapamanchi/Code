using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.ResendResConfirmEmail
{


    public class Reservation
    {
        public string OwnerID { get; set; }
        public string OwnerAccountNumber { get; set; }
        public string ProjectNumber { get; set; }
        public string ReservationNumber { get; set; }
        public string ReservationType { get; set; }
        public string EmailAddress { get; set; }
        public string AgentName { get; set; }
        public string Comments { get; set; }
        public string Delivery { get; set; }
        public string ConfType { get; set; }
        public string Delete { get; set; }
        public string ResStatus { get; set; }
        public string ProcessType { get; set; }
        public string InsertDate { get; set; }
    }

    public class ResendResConfirmEmailRequest
    {
        public Reservation Reservation { get; set; }
    }


}