using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerPremierWaitListResponse
{

    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

    public class OwnerPremierWaitList
    {
        public int RequestID { get; set; }
        public string ConfirmationNumber { get; set; }
        public string ARVACT { get; set; }
        public string OwnerName { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string PremierLevel { get; set; }
        public string DateOfRequest { get; set; }
        public string ResortName { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string VillaSize { get; set; }
        public string NoOfGuests { get; set; }
        public string SpecialRequest { get; set; }
        public string Status { get; set; }
        public string InternalNotes { get; set; }
        public string DateCompleted { get; set; }
        public string AgentName { get; set; }
    }

    public class RestOwnerPremierWaitListResponse
    {
        public List<Error> Errors { get; set; }
        public List<OwnerPremierWaitList> OwnerPremierWaitList { get; set; }
    }


}