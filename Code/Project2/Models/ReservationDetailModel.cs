using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OptionDropDownList;

namespace BGModern.Models
{
    public class ReservationDetailModel : MasterModel
    {
        public String Title { get; set; }
        public String Message { get; set; }

        //dropdowns
        public List<int> Occupancy { get; set; }
        public List<string> RelationshipTypes { get; set; }
        public List<String> GuestTypes { get; set; }
        public List<OptionGroupItem> GuestList { get; set; }
        public List<SelectListItem> States { get; set; }

        //Reservation Details
        public string Amount { get; set; }
        public string ReservationNo { get; set; }
        public string GuestId { get; set; }
        public string GuestName { get; set; }
        public string GuestType { get; set; }
        public string MaxOccupancy { get; set; }
        public string NumberOfAdults { get; set; }
        public string ReservationCondition { get; set; }
        public string ReservationType { get; set; }
        public string PolicyStatus { get; set; }
        public string PolicyPrice { get; set; }
        public string ConfirmationDate { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Points { get; set; }
        public string PayType { get; set; }
        public int NumberOfNights { get; set; }
        public int SelectedOccupancy { get; set; }

        //Resort Details
        public string CancellationPolicy { get; set; }
        public string Description { get; set; }
        public string ResortNo { get; set; }
        public string ResortName { get; set; }
        public string ResortAddress { get; set; }
        public string ResortCityState { get; set; }
        public string ResortImageLink { get; set; }
        public string ResortLink { get; set; }
        public string ResortPhone { get; set; }
        public string VillaSize { get; set; }
        public string VillaDescription { get; set; }

        //Guest Form Details
        public bool GuestFormVisible { get; set; }
        public string[] Names { get; set; }
        public string LastName { get; set; }
        public string GuestFormFirstName { get; set; }
        public string GuestFormLastName { get; set; }
        public string GuestFormEmail { get; set; }
        public string GuestFormCity { get; set; }
        public string GuestFormPhone { get; set; }
        public string GuestFormState { get; set; }
        public string GuestFormRelationship { get; set; }

        public bool CanCancel { get; set; }
        public bool CanUpdate { get; set; }
        public bool Updating { get; set; }
        public string ButtonUpdateText { get; set; }
        public string PostType { get; set; }
        public string SelectedGuest { get; set; }
        public string SelectedGuestGroup { get; set; }
        public string SelectedGuestRelationship { get; set; }
        public string SelectedGuestState { get; set; }
        public int SelectedGuestId { get; set; }

        public string ErrorText { get; set; }
        public string MessageText { get; set; }
        public string UpdateText { get; set; }

        public bool ExchangeVisible { get; set; }
    }
}