using System;
using System.Web.Mvc;
using BGModern.Models;
using BGO.OwnerWS;

namespace BGModern.Controllers
{
    public class OwnersPaletteController : Umbraco.Web.Mvc.SurfaceController
    {
        
        public ActionResult OwnersPalette()
        {
            // This is where we'd authenticate with the owner service to receive our Owner object
            OwnerModel model = new OwnerModel();
            model.BxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            HydrateModel(model);
            return PartialView("OwnersPalette", model);
        }

        private void HydrateModel(OwnerModel owner)
        {
            if (null == HttpContext.Session["ownername"])
            {
                owner.FullName = "unknown"; // do not default to any name
            }
            else
            {
                owner.FullName = HttpContext.Session["ownername"].ToString();
            }

            owner.FullName = owner.BxgOwner.fullName;
            owner.IsAccountExpired = owner.BxgOwner.AccountExpired;
            owner.IsPendingOwner = ((string)Session["IsPendingOwner"]) == "TRUE";
            owner.IsTravelerPlusEligible = owner.BxgOwner.TravelerPlusMembership.IsTravelerPlusEligible;
            owner.HomeProject = owner.BxgOwner.User[0].HomeProject;
            owner.OwnerContractType = (string)Session["OwnerContractType"];
            bool btEnabled = false;
            try
            {
                bool.TryParse(Session["BonusTimeEnabled"].ToString(), out btEnabled);
            }
            catch
            {
                btEnabled = false;
            }
            owner.BonusTimeEnabled = btEnabled;
            if (Session["OwnerHomeResortWeeks"] == null)
            {
                owner.OwnerHomeResortWeeks = "0";
            }
            else
            {
                try
                {
                    owner.OwnerHomeResortWeeks = Session["OwnerHomeResortWeeks"].ToString();
                }
                catch
                {
                    owner.OwnerHomeResortWeeks = "0";
                }
            }

            if (owner.OwnerContractType == "Vacation Club" || owner.OwnerContractType == "Sampler")
            {
                owner.DisplayPointsDetail = true;
            }

            if (owner.OwnerContractType != "Sampler")
            {
                owner.PaymentBalance = Convert.ToDecimal(owner.BxgOwner.PaymentBalance);
            }

            if (owner.OwnerContractType == "Vacation Club")
            {
                owner.OwnershipLevel = owner.BxgOwner.membershipLevelDesc;
            }
            else if (owner.OwnerContractType == "Sampler")
            {
                if (owner.HomeProject == "51")
                {
                    owner.OwnershipLevel = "Sampler";
                }
                else if (owner.HomeProject == "52")
                {
                    owner.OwnershipLevel = "Sampler 24";
                }
            }

            if (owner.BxgOwner.OwnerExpiration != null)
            {
                owner.TravelerPlusExpiration = DateTime.Parse(owner.BxgOwner.OwnerExpiration);
            }

            if (Session["PalettReload"] != null && Convert.ToBoolean(Session["PalettReload"]))
            { 
                OwnerWS1SoapClient OwnerServiceProxy = new OwnerWS1SoapClient();
                BGO.OwnerWS.Points ownerTotalPoints = new BGO.OwnerWS.Points();
                BGO.OwnerWS.Owner bxgOwnerTemp = OwnerServiceProxy.Authenticate(Session["LoginEmail"].ToString(), Session["LoginPassword"].ToString());
                ownerTotalPoints=OwnerServiceProxy.getTotalPoints(owner.BxgOwner.Arvact);

                if (NeedsUpdatedOwnerPointValues(owner.BxgOwner, ownerTotalPoints))
                {
                    owner.BxgOwner.PointsTotal = ownerTotalPoints.PointsTotal;
                    owner.BxgOwner.PointsTotalAnnual = ownerTotalPoints.PointsTotalAnnual;
                    owner.BxgOwner.PointsTotalRestricted = ownerTotalPoints.PointsTotalRestricted;
                    owner.BxgOwner.PointsTotalSaved = ownerTotalPoints.PointsTotalSaved;
                    owner.BxgOwner.PointsTotalFuture = ownerTotalPoints.PointsTotalFuture;

                    NewPointsHandler(owner.BxgOwner);
                }
            }

            if (owner.DisplayPointsDetail)
            {
                owner.AvailablePoints = owner.BxgOwner.PointsTotal;
                owner.AnnualPoints = owner.BxgOwner.PointsTotalAnnual;
                owner.SavedPoints = owner.BxgOwner.PointsTotalSaved;
                owner.FuturePoints = owner.BxgOwner.PointsTotalFuture;
                owner.RestrictedPoints = owner.BxgOwner.PointsTotalRestricted;
                owner.EncoreDividends = Convert.ToInt32(owner.BxgOwner.EncoreBenefits.DividendsBalance);
            }

        
        }

        private bool NeedsUpdatedOwnerPointValues(BGO.OwnerWS.Owner OldOwner, BGO.OwnerWS.Points NewOwnerPoints)
        {
            bool needsUpdated = false;

            if (OldOwner.PointsTotal != NewOwnerPoints.PointsTotal)
                needsUpdated = true;
            if (OldOwner.PointsTotalAnnual != NewOwnerPoints.PointsTotalAnnual)
                needsUpdated = true;
            if (OldOwner.PointsTotalRestricted != NewOwnerPoints.PointsTotalRestricted)
                needsUpdated = true;
            if (OldOwner.PointsTotalSaved != NewOwnerPoints.PointsTotalSaved)
                needsUpdated = true;
            if (OldOwner.PointsTotalFuture != NewOwnerPoints.PointsTotalFuture)
                needsUpdated = true;

            return needsUpdated;
        }

        private void NewPointsHandler(BGO.OwnerWS.Owner bxgOwner)
        {
            try
            {
                Session["PalettReload"] = false;

                //Populate the Owner object and Session variable.
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerAnnualPoints = bxgOwner.PointsTotalAnnual.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerSavedPoints = bxgOwner.PointsTotalSaved.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerRestrictedPoints = bxgOwner.PointsTotalRestricted.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerTotalPoints = bxgOwner.PointsTotal.ToString();
                BGO.BluegreenOnline.Bluegreenowner.CurrentOwner.OwnerPaymentBalance = bxgOwner.PaymentBalance.ToString();

                Session["BXGOwner"] = bxgOwner;
            }
            catch(Exception ex)
            {
            
            }
        }
    }
}