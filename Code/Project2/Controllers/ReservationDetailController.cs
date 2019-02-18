using BGModern.Mappers;
using BGModern.Models;
using System.Configuration;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using System;

namespace BGModern.Controllers
{
#if !DEBUG
    [Authorize]
#endif
    public class ReservationDetailController : RenderMvcController
    {
        public ActionResult ReservationDetail(string reservationNo, string resortNo, string type)
        {
            if (Session["BXGOwner"] == null)
            {
                if (Session["_path_info"] != null)
                    Session["_path_info"] = Request.RawUrl;

                Response.Redirect(ConfigurationManager.AppSettings["bxgwebUnsecureURL"] + "default.aspx?sess=timeout", true);
            }

            ReservationDetailModel detailModel = ReservationDetailMapper.Map(CurrentPage);
            detailModel = MasterMapper.Map(detailModel, CurrentPage);

            if (TempData["DetailGuestData"] != null)
            {
                detailModel = ((ReservationDetailModel)TempData["DetailGuestData"]);
                TempData["DetailGuestData"] = detailModel;
            }
            //else if (TempData["DetailModelData"] != null)
            //{
            //    detailModel = ((ReservationDetailModel)TempData["DetailModelData"]);
            //    TempData["DetailModelData"] = detailModel;
            //}
            else
            {
                if (TempData["ErrorText"] != null)
                {
                    detailModel.ErrorText = TempData["ErrorText"].ToString();
                    TempData.Remove("ErrorText");
                }

                if (TempData["UpdateText"] != null)
                {
                    detailModel.UpdateText = TempData["UpdateText"].ToString();
                    TempData.Remove("UpdateText");
                }

                if (TempData["SubmittedForm"] != null)
                {
                    ReservationDetailModel submittedModel = ((ReservationDetailModel)TempData["SubmittedForm"]);
                    detailModel.MessageText = submittedModel.MessageText;
                    detailModel.SelectedGuest = submittedModel.SelectedGuest;
                    detailModel.GuestFormFirstName = submittedModel.GuestFormFirstName;
                    detailModel.GuestFormLastName = submittedModel.GuestFormLastName;
                    detailModel.GuestFormEmail = submittedModel.GuestFormEmail;
                    detailModel.GuestFormState = submittedModel.GuestFormState;
                    detailModel.GuestFormPhone = submittedModel.GuestFormPhone;
                    detailModel.GuestFormRelationship = submittedModel.GuestFormRelationship;

                    TempData.Remove("SubmittedForm");
                }


                detailModel.ReservationNo = reservationNo;
                detailModel.ResortNo = resortNo;
                detailModel.ReservationCondition = type;

                string pageTitleProperty = string.Empty;
                if (type == "Future")
                {
                    pageTitleProperty = "futureReservationPageTitle";
                }
                else if (type == "Past")
                {
                    pageTitleProperty = "pastReservationPageTitle";
                }

                int? reservationListId = Classes.Utilities.WebConfigContentId.GetValue("reservationListContentId");
                if (reservationListId.HasValue)
                {
                    var reservationList = Umbraco.TypedContent(reservationListId.Value);
                    detailModel.Title = reservationList.GetPropertyValue<string>(pageTitleProperty);
                }
            }

            return View("ReservationDetail", detailModel);
        }
    }
}