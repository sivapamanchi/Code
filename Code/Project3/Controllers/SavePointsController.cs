using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.OwnerService.OwnerPointsResponse;
using BGSitecore.OwnerService;
using BGSitecore.Services;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Nancy.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class SavePointsController : GlassController
    {

        [HttpPost]
        public ActionResult SubmitSaveMyPoint(string CCName, string CCNumber, string CVV, string ExpMonth, string ExpYear, string ZipCode, string cctype, bool InternationalZipCode)
        {
            SaveMyPointsResponse response = new SaveMyPointsResponse();

            ReservationParameters model = new ReservationParameters();
            model.CreditCard_ExpDateMonth = ExpMonth;
            model.CreditCard_ExpDateYear = ExpYear;
            model.CreditCard_Name = CCName;
            model.CreditCard_Number = CCNumber;
            model.CreditCard_VerificationNumber = CVV;
            model.CreditCard_ZipCode = ZipCode;
            model.CreditCard_InternationalZipCode = InternationalZipCode;

            BlueGreenContext bgContext = new BlueGreenContext();
            model.CreditCard_Type = FormatUtils.ConvertCreditCard(cctype);
           
 
            var listOfError = ValidationUtils.GetCreditCardViolations(model);
            if (listOfError.Count() <= 0)
            {
                BGSitecore.Models.OwnerService.OwnerSavePointsElectRequest.OwnerSavePointsElectRequest request = new BGSitecore.Models.OwnerService.OwnerSavePointsElectRequest.OwnerSavePointsElectRequest();

                request.Identifier = bgContext.OwnerId;

                
                request.AgentID = "OWNER";
                
                request.NameOnCard = model.CreditCard_Name;
                request.Amount = bgContext.GetSavePointsFee;

                request.CreditCardInfo = new BGSitecore.Models.OwnerService.OwnerSavePointsElectRequest.CreditCardInfo();
                request.CreditCardInfo.CreditCardNumber = model.CreditCard_Number;
                request.CreditCardInfo.CreditCardExpirationDate = ReservationUtils.GetExpDate(model.CreditCard_ExpDateMonth, model.CreditCard_ExpDateYear);
                request.CreditCardInfo.CreditCardType = model.CreditCard_Type;
                request.CreditCardInfo.CreditCardCVV = model.CreditCard_VerificationNumber;

                ProfileService service = new ProfileService();
                
                var pointResponse = service.OwnerSavePointsElect(request);
                if (pointResponse != null && pointResponse.RetCode == "0")
                {
                    response.RetCode = "0";
                    //TODO move this code
                    bgContext.bxgOwner.AnnualPointsExpiration.SavePointsEligible = false;
                    bgContext.bxgOwner.AnnualPointsExpiration.SavePointsFee = "0.00";
                    bgContext.bxgOwner.AnnualPointsExpiration.SavePointsMessage = "";

                    //save owner object with updated annualpoints expiration
                    Session["BXGOwner"] = bgContext.bxgOwner;
                    response.AuthorizationNumber = pointResponse.AuthorizationNumber;                   
                    OwnerUtils.SetContextToReloadPalett();

                }
                else
                {
                    response.RetCode = "-1";
                    response.errors = new List<string>();
                    response.errors.Add( "Internal error try again later.");

                }

            }
            else
            {
                response.RetCode = "-1";
                response.errors = new List<string>();
                foreach (var item in listOfError)
                {
                    response.errors.Add(item.ErrorMessage);
                }
               
                return Json(response);

            }

            return Json(response);
        }

    }
}