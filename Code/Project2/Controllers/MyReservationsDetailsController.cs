using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BGModern.Models;
using ReservationLibrary;

namespace BluegreenOnline.Controllers
{
    public class MyReservationsDetailsController : Umbraco.Web.Mvc.SurfaceController
    {
        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();

        public ActionResult GetPartialView(string reservationNo, string resortNo)
        {
            MyReservationsDetailsModel detailsModel = new MyReservationsDetailsModel();
            detailsModel.ReservationNo = reservationNo;
            detailsModel.ResortNo = resortNo;

            PopulateResortInfo(detailsModel);
            PopulateReservationInfo(detailsModel);
            PopulateRelationshipTypes(detailsModel);


            return PartialView("ReservationsDetails", detailsModel);
        }

        public void PopulateResortInfo(MyReservationsDetailsModel detailsModel)
        {
            //BGO.ResortsService.ResortsServiceClient reservationAS400 = new BGO.ResortsService.ResortsServiceClient();

            //ResortInfo resortInfo = Utilities.getResortInfo(Convert.ToInt32(detailsModel.ResortNo));
            //detailsModel.ResortName = resortInfo.ResortName;
            //detailsModel.ResortAddress = resortInfo.Address;
            //detailsModel.ResortCityState = String.Concat(resortInfo.City, ", ", resortInfo.State, "    ", resortInfo.PostalCode);
            //detailsModel.ResortPhone = resortInfo.Phone;

            //TODO: Add imgResort.ImageURL
        }

        public void PopulateReservationInfo(MyReservationsDetailsModel detailsModel)
        {
            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];

            BGO.ResortsService.ReservationHistory history = new BGO.ResortsService.ReservationHistory();
            BGO.ResortsService.ReservationHistoryList historyResult = new BGO.ResortsService.ReservationHistoryList();
            BGO.ResortsService.ResortsServiceClient reservationAS400 = new BGO.ResortsService.ResortsServiceClient();
            BGO.ResortsService.ReservationHistoryItem[] histories = null;
            BGO.ResortsService.OwnerID owner = new BGO.ResortsService.OwnerID();

            try
            {
                history.OwnerID = null;
                owner.OwnerVacationNumber = BXGOwner.Arvact;
                history.OwnerID = owner;
                history.SiteName = BGO.ResortsService.Sites.OnlinePoints;
                history.EffectiveDate = DateTime.Now;

                history.SearchHistoryBy = BGO.ResortsService.ReservationHistoryType.Both;
                historyResult = reservationAS400.GetReservationsHistory(history);
                if (historyResult.Success)
                {
                    histories = historyResult.ReservationHistoryItem;
                    foreach (BGO.ResortsService.ReservationHistoryItem reservationItem in histories)
                    {
                        if (reservationItem._ReservationNumber == detailsModel.ReservationNo)
                        {
                            detailsModel.GuestName = reservationItem._GuestFullName;
                            detailsModel.MaxOccupancy = reservationItem._MaximumOccupancy;
                            detailsModel.ReservationType = reservationItem._ReservationType;
                            detailsModel.NumberOfNights = Convert.ToInt32(reservationItem._NumberOfNights);
                            detailsModel.NumberOfAdults = reservationItem._NumberOfAdults;
                            detailsModel.PolicyStatus = reservationItem._PolicyStatus;
                            detailsModel.ConfirmationDate = Convert.ToDateTime(reservationItem._DateConfirmed).ToString();
                            detailsModel.CheckIn = Convert.ToDateTime(reservationItem._CheckInDate).ToString();
                            //detailsModel.ConfirmationDate = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(reservationItem._DateConfirmed));
                            //detailsModel.CheckIn = String.Format("{0:M/d/yyyy}", Convert.ToDateTime(reservationItem._CheckInDate));
                            //detailsModel.CheckOut = reservationItem.  TODO: WHERE DOES THIS COME FROM
                            detailsModel.Amount = Convert.ToDouble(reservationItem._Amount); //TO DO:  IS THIS hfAmount?????????

                            if (detailsModel.ReservationType == "PSE")
                                detailsModel.ResortNo = "85";

                            if (reservationItem._Points == "")
                                detailsModel.Points = "0";
                            else
                                detailsModel.Points = String.Format("{0:#,###}", reservationItem._Points.Trim());

                            //Tax calculations are used for points
                            double tax = Convert.ToDouble(Utilities.getResortInfo(Convert.ToInt32(reservationItem._ProjectStay)).TaxRate);
                            if (detailsModel.NumberOfNights == 1)
                                detailsModel.NumberOfNights = 2;

                            tax = tax * Convert.ToDouble(detailsModel.Amount/detailsModel.NumberOfNights);  //The tax charge daily
                            tax = Math.Round(tax, 2, MidpointRounding.AwayFromZero);  //Round up tax charge to a penny.
                            double total = tax * detailsModel.NumberOfNights + detailsModel.Amount;

                            if (detailsModel.ReservationType == "Bonus Time")
                            {
                                detailsModel.PayType = "Amount:";
                                detailsModel.Points = String.Format("{0:c}", total);  //TODO: locate difference between Me.controls and controls.  Just means this instance right???
                                detailsModel.Points = detailsModel.Amount.ToString();
                            }
                            else if (detailsModel.ReservationType == "Flex")
                            {
                                detailsModel.PayType = "Nights:";
                                detailsModel.Points = detailsModel.NumberOfNights.ToString();
                            }
                            else
                                detailsModel.PayType = "Points:";


                            //TODO: Get Villa Description and Handicap accessible, checkout
                        }
                    }

                }

                if (reservationAS400.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    reservationAS400.Close();
                }



            }
            catch (Exception ex)
            {
                if (reservationAS400.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    reservationAS400.Close();
                }

                StringBuilder errMsg = new StringBuilder();
                errMsg.Append("Error happened fetching Reservations List");
                errMsg.Append(ex.Message);
                sendMail.sendMessage("OLPSupport@bxgcorp.com", "", "Exception Error - Getting Reservation List", errMsg);
            }

            //BGO.ResortsService.ReservationHistoryItem =
        }

        public void PopulateRelationshipTypes(MyReservationsDetailsModel detailsModel)
        {
            List<string> relTypes = new List<string>();
            relTypes.Add("Select Guest Relationship Type");

            foreach (BGO.ResortsService.RelationshipType relationshipType in System.Enum.GetValues(typeof(BGO.ResortsService.RelationshipType)))
            {
                if (relationshipType.ToString() != "None")
                    relTypes.Add(relationshipType.ToString());
            }

            detailsModel.RelationshipTypes = relTypes;
        }

        private void PopulateStates()
        {


        }
    }
}
