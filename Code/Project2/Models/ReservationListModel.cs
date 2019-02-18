using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco;
using Umbraco.Core.Models;


namespace BGModern.Models
{
    public class ReservationListModel : MasterModel
    {
        public String Message { get; set; }
        public List<ReservationModel> PendingReservations { get; set; }
        public List<ReservationModel> PastReservations { get; set; }

        public Boolean IsSamplerOwner { get; set; }
        public String HomeProject { get; set; }
        public int ResortNo { get; set; }
        public String PageDisclaimer { get; set; }

        public ReservationDetailModel DetailModel { get; set; }

        public string ErrorText { get; set; }
    }
}