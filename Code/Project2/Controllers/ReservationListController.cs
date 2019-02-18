using BGModern.Mappers;
using BGModern.Models;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System;
using System.Configuration;

namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class ReservationListController : RenderMvcController
    {
        private BGO.OwnerWS.Owner BXGOwner = new BGO.OwnerWS.Owner();
        private BGO.OwnerWS.OwnerWS1SoapClient OwnerService = new BGO.OwnerWS.OwnerWS1SoapClient();

        public ActionResult ReservationList()
        {
            if (Session["BXGOwner"] == null)
            {
                if (Session["_path_info"] != null)
                    Session["_path_info"] = Request.RawUrl;

                Response.Redirect(ConfigurationManager.AppSettings["bxgwebUnsecureURL"] + "default.aspx?sess=timeout", true);
            }

            BXGOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];

            ReservationListModel myReservations = ReservationListMapper.Map(CurrentPage);
            myReservations = MasterMapper.Map(myReservations, CurrentPage);

            HydrateModel(myReservations);

            string reservationNo = String.Empty;
            string resortNo = String.Empty;

            if (TempData["ReservationNo"] != null && TempData["ResortNo"] != null)
            {
                reservationNo = TempData["ReservationNo"].ToString();
                resortNo = TempData["ResortNo"].ToString();
            }

            if (reservationNo != String.Empty && resortNo != String.Empty)
            {
                ReservationDetailModel detailsModel = new ReservationDetailModel();
                detailsModel.ReservationNo = reservationNo;
                detailsModel.ResortNo = resortNo;
                myReservations.DetailModel = detailsModel;
            }

            return View(myReservations);
        }

        private void InitializePageView(ReservationListModel model)
        {

            //if ((BXGOwner.User[0] != null))
            //{
            //    model.IsSamplerOwner = BXGOwner.User[0].isSampler;
            //    model.HomeProject = BXGOwner.User[0].HomeProject;
            //}
            //else
            //{
            //    model.IsSamplerOwner = false;
            //    model.HomeProject = "0";
            //}

            //if (model.IsSamplerOwner == true)
            //    model.HidePanelChoice = true;

            // model.HidePaymentInfo = true;

            //if (Session["OwnerContractType"].ToString() == "Sampler")
            //{
            //    model.HideAccountInfo = true;
            //    model.HideSavePointsButton = true;
            //}

            //if (Request.QueryString["display"] == null)
            //    model.HidePanelReminder = true;

        }

        private void HydrateModel(ReservationListModel model)
        {
            //PopulateReservationHistory(model);
            //PopulateAcctContractInfo(model);

            //GetMyPointsList(model);
        }

        private void getStates()
        {
            //ddlState.Items.Clear();
            //clsDBConnectivity C = new clsDBConnectivity();
            //SqlClient.SqlDataReader dbDataReader = default(SqlClient.SqlDataReader);
            //ListItem liUS = new ListItem();

            //liUS.Value = "";
            //liUS.Text = "Select a State";
            //ddlState.ClearSelection();
            //ddlState.Items.Add(liUS);

            //C.dbCmnd.CommandText = "uspGetCountriesbyRegions";
            //C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;
            //dbDataReader = C.dbCmnd.ExecuteReader();

            //while (dbDataReader.Read())
            //{
            //    ListItem li = new ListItem();

            //    li.Value = dbDataReader("StateID");
            //    li.Text = dbDataReader("StateDescription");

            //    ddlState.Items.Add(li);

            //}

            //ddlState.SelectedIndex() = false;

            //dbDataReader.Close();
            //C.Close();
            //C = null;
        }



    }
}