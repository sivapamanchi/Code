using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using System.Linq;
using System;
using Sitecore.Rules;
using System.Xml.Linq;
using BGSitecore.Models.Reward;
using BGSitecore.Services;
using BGSitecore.Models.ReferralService;
using BGSitecore.Models;
using System.Collections.Generic;
using BGSitecore.Components;
using System.Data;
using BGSitecore.Models.RewardService;
using BGSitecore.Utils;
using BGSitecore.Models.Resort;

namespace BGSitecore.Controllers
{
    public class RewardsController : GlassController
    {

        public ActionResult GetRedeemRewards()
        {
            var redeemRewards = GetLayoutItem<Rewards>();
            return View(redeemRewards);
        }

        public ActionResult GetRightPanel()
        {
           
            var rightPanel = GetLayoutItem<RightPanel>();
           
            foreach (RightSection section in rightPanel.RightSections)
            {
                bool showUsingRules = true;
                if (section.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                {
                    string rule = section.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                    if (!string.IsNullOrEmpty(rule))
                    {
                        var rules = RuleFactory.ParseRules<RuleContext>(section.InnerItem.Database, XElement.Parse(rule));
                        var ruleContext = new RuleContext()
                        {
                            Item = section.InnerItem
                        };

                        if (rule.Any() && rules.Rules.Count() > 0)
                        {
                            showUsingRules = rules.Rules.First().Evaluate(ruleContext);
                        }
                    }
                }
                section.HideSection = !showUsingRules;
                if (showUsingRules)
                {
                    BlueGreenContext bgContext = new BlueGreenContext();
                    if (bgContext != null && bgContext.bxgOwner != null && bgContext.IsAuthenticated)
                    {
                        int cartItemsQuantityCount = DBManager.GetAllCartItems(bgContext.bxgOwner.Arvact).AsEnumerable().Sum(x => x.Field<int>("Quantity"));
                        rightPanel.RewardCartItemCount = cartItemsQuantityCount;
                    }
                    else { rightPanel.RewardCartItemCount = 0; }
                }
            }
            return View(rightPanel);
        }

        public ActionResult RewardsSection()
        {            
            var model = GetLayoutItem<RewardsSections>();
            
            //TODO make it as Rule at later time
            CheckForTravelerPlusMembershipAndRedirect(model);

            //TODO mode this code in central place to evaluate rules
            foreach (Models.Reward.Section section in model.AllSections)
            {

                bool showUsingRules = true;

             
                if (section.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                {
                    string rule = section.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                    if (!string.IsNullOrEmpty(rule))
                    {
                        var rules = RuleFactory.ParseRules<RuleContext>(section.InnerItem.Database, XElement.Parse(rule));
                        var ruleContext = new RuleContext()
                        {
                            Item = section.InnerItem
                        };

                        if (rule.Any() && rules.Rules.Count() > 0)
                        {
                            showUsingRules = rules.Rules.First().Evaluate(ruleContext);
                        }
                    }
                }
                section.HideSection = !showUsingRules;

                //Evaluate Rules For any rich text If Any
                if (section.AllRichText != null && section.AllRichText.Any())
                {
                    List<RichText> allRichTextItems = section.AllRichText.Select(x => x).ToList();
                    section.AllRichText = FilterRichTextBasedOnRules(allRichTextItems);
                }

                // Evaluate Rules For any Section Gallery Items If Any
                if (section.AllImages != null && section.AllImages.Any())
                {
                    List<FeaturedItem> allGalleryImages = section.AllImages.Select(x => x).ToList();
                    section.AllImages = RemoveUnwantedImagesBasedOnRules(allGalleryImages);
                }

                // Evaluate Rules For any Section Featured Folder Items If Any
                if (section.FolderWithFeaturedItems != null && section.FolderWithFeaturedItems.AllFeaturedItems.Any())
                {
                    List<FeaturedItem> allFeaturedItems = section.FolderWithFeaturedItems.AllFeaturedItems.Select(x=>x).ToList();                    
                    section.FolderWithFeaturedItems.AllFeaturedItems = RemoveUnwantedImagesBasedOnRules(allFeaturedItems);
                }

            }
            
            return View(model);
        }
        private IEnumerable<FeaturedItem> RemoveUnwantedImagesBasedOnRules(List<FeaturedItem> allImageItems)
        {
            var allFeaturedItems = allImageItems;
            foreach (var item in allFeaturedItems.Reverse<FeaturedItem>())
            {
                if (item.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                {
                    string rule = item.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                    if (!SitecoreUtils.EvaluateRules(rule, item.InnerItem))
                    {
                        allFeaturedItems.Remove(item);
                    }
                }
            }
            return allFeaturedItems.AsEnumerable();
        }

        private IEnumerable<RichText> FilterRichTextBasedOnRules(List<RichText> allRichTextItems)
        {
            var FilteredRichTextItems = allRichTextItems;
            foreach (var item in FilteredRichTextItems.Reverse<RichText>())
            {
                if (item.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                {
                    string rule = item.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                    if (!SitecoreUtils.EvaluateRules(rule, item.InnerItem))
                    {
                        FilteredRichTextItems.Remove(item);
                    }
                }
            }
            return FilteredRichTextItems.AsEnumerable();
        }

        private void CheckForTravelerPlusMembershipAndRedirect(RewardsSections model)
        {
            // Check for Travelerplus Expiry .Copied Logic from Legacy TravelerPlus Home page
            if (model.InnerItem.ID.ToString().Equals("{536E4650-A734-4D8B-AFF6-62518271C2B5}", StringComparison.CurrentCultureIgnoreCase))
            {

                BlueGreenContext bgContext = new BlueGreenContext();
                string targetUrl = string.Empty;

                if (bgContext != null && bgContext.GetOwnerType() == "Travelerplus")
                {
                    if (bgContext.IsTPExpired && bgContext.IsAccountExpired )
                    {
                        targetUrl = UrlMapper.Map("https://www.bluegreenowner.com/TravelerPlus/owner/ownerrenewal.aspx");
                        Response.RedirectPermanent(targetUrl, true);
                    }
                }
                else {
                    targetUrl = UrlMapper.Map("https://www.bluegreenowner.com/owner/vcTravelerPlus.aspx");
                    Response.RedirectPermanent(targetUrl, true);
                }
            }
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult RedeemRewardsConfirm()
        {
            List<CartItem> confirmedRewards = new List<CartItem>();
            if (Session["ConfirmedRewards"] != null)
            {
                confirmedRewards = (List<CartItem>)Session["ConfirmedRewards"]; //GetAllCartItems();
            }
            
            return View(confirmedRewards);
        }

        public ActionResult RedeemSection()
        {
            BlueGreenContext bgContext = new BlueGreenContext();
            //Just make it Null always when page loads
            if (Session["ConfirmedRewards"] != null)
            {
                Session["ConfirmedRewards"] = null;
            }

            var model = GetLayoutItem<RewardsSections>();
            if (model != null)
            {
                model.cartItemsList = GetAllCartItems();
                var encoreAvailRewards = (bgContext != null && bgContext.bxgOwner != null && bgContext.bxgOwner.EncoreBenefits != null) ? bgContext.bxgOwner.EncoreBenefits.DividendsBalance : 0;
                model.CartTotal = new CartTotal()
                {
                    AvailableRewards = encoreAvailRewards,
                    TotalRewardsinCart = (model.cartItemsList.Count > 0) ? model.cartItemsList.Select(t => t.SubTotal).Sum() : 0,
                    AvailablePoints = encoreAvailRewards - ((model.cartItemsList.Count > 0) ? model.cartItemsList.Select(t => t.SubTotal).Sum() : 0)
                };
            }

            //TODO mode this code in central place to evaluate rules
            foreach (Models.Reward.Section section in model.AllSections)
            {

                bool showUsingRules = true;
                if (bgContext.bxgOwner != null)
                {
                    section.MaintenanceFee = string.Format("{0:c}", bgContext.bxgOwner.PaymentBalance);
                    section.BalanceDue = string.Format("{0:c}", bgContext.bxgOwner.PaymentBalance);
                }

                if (section.InnerItem.Fields[BaseComponent.RestrictionRuleId] != null)
                {
                    string rule = section.InnerItem.Fields[BaseComponent.RestrictionRuleId].Value;
                    if (!string.IsNullOrEmpty(rule))
                    {
                        var rules = RuleFactory.ParseRules<RuleContext>(section.InnerItem.Database, XElement.Parse(rule));
                        var ruleContext = new RuleContext()
                        {
                            Item = section.InnerItem
                        };

                        if (rule.Any() && rules.Rules.Count() > 0)
                        {
                            showUsingRules = rules.Rules.First().Evaluate(ruleContext);
                        }
                    }
                }
                section.HideSection = !showUsingRules;
                               
            }

            return View(model);
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public List<CartItem> GetAllCartItems()
        {
            BlueGreenContext bgContext = new BlueGreenContext();
            
            
            
            List<CartItem> itemsForOwner = new List<CartItem>();
         
            if (bgContext.bxgOwner != null)
            {
                DataTable table = DBManager.GetAllCartItems(bgContext.bxgOwner.Arvact);
               
                itemsForOwner = table.AsEnumerable()
                .Select(row => new CartItem
                {
                    Arvact = Convert.ToString(row["ARVACT"]),
                    EncoreOwnerID = Convert.ToString(row["EncoreOwnerID"]),
                    ItemID = Convert.ToString(row["ItemID"]),
                    ItemName = Convert.ToString(row["ItemName"]),
                    TransactionType = Convert.ToString(row["TransactionType"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    SubTotal = Convert.ToDecimal(row["SubTotal"]),                   
                }).ToList();

            }
            
            return itemsForOwner;
        }


        ///<summary>
        ///Compares Validity of each item from itemsTobeValidated to sourceData List With Sitecore Values
        ///</summary>
        ///<param name="itemsTobeValidated"></param>
        ///<param name="sourceData"></param>
        /// <returns>JsonResponse</returns> 
        public JsonResponse validateCartItems(List<CartItem> itemsTobeValidated , List<CartItem> sourceData , bool isUpdate = false)
        {
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "1";
            Sitecore.Data.Database dbContext = Sitecore.Context.Database;

            BlueGreenContext bgContext = new BlueGreenContext();
            var encoreAvailRewards = (bgContext != null && bgContext.bxgOwner != null && bgContext.bxgOwner.EncoreBenefits != null) ? bgContext.bxgOwner.EncoreBenefits.DividendsBalance : 0;
            CartTotal CartTotal = new CartTotal()
            {
                AvailableRewards = encoreAvailRewards,
                TotalRewardsinCart = (sourceData.Count > 0) ? sourceData.Select(t => t.SubTotal).Sum() : 0,
                AvailablePoints = encoreAvailRewards - ((sourceData.Count > 0) ? sourceData.Select(t => t.SubTotal).Sum() : 0)
            };

            //Loop to check Whether each item has Actual price listed in Sitecore
            foreach (CartItem itemToBeAdded in itemsTobeValidated)
            {
                // If the item price/SubTotal is zero do not allow to add to reward cart
                if ( !isUpdate && (itemToBeAdded.UnitPrice == 0 || itemToBeAdded.SubTotal == 0))
                {
                    jsonResponse.RetCode = "1";
                    jsonResponse.errors.Add("Please try to add/update valid cart item");
                }

                if (!itemToBeAdded.ItemID.Equals("MF"))
                {
                    // verify that the unit price matches the actual unit price defined in Sitecore and has not been altered
                    Sitecore.Data.Items.Item thisItem = dbContext.GetItem(itemToBeAdded.ItemID);
                    string itemUnitPrice = thisItem.Fields["Item Value"].Value.Replace(",", "");
                    if (!itemToBeAdded.UnitPrice.Equals(Convert.ToDecimal(itemUnitPrice)))
                    {
                        // somebody tampered with the data manipulating the unit price
                        jsonResponse.RetCode = "1";
                        jsonResponse.errors.Add("Validation error: Unit Price Invalid");
                    }
                }
               
                if (itemToBeAdded.SubTotal != itemToBeAdded.Quantity * itemToBeAdded.UnitPrice)
                {
                    // somebody tampered with the data manipulating the unit price
                    jsonResponse.RetCode = "1";
                    jsonResponse.errors.Add("Validation error: Sub Total Tampered");
                }

                // Check whether you already have one Error generated if so , break loop to avoid duplicate Error on Page
                if (jsonResponse.errors.Any()) { break; }
            }
            // Execute the Code Only if there are no Errors from previous checks
            // This Code is for Case where List of Items come in through Update Process and Available points go as negative
            if (!jsonResponse.errors.Any()) {

                if (CartTotal.AvailablePoints >= 0 && !isUpdate)
                {
                    
                    decimal subTotalOfSingleItem = 0;
                    var itemInCartAlready = sourceData.Where(x => itemsTobeValidated.Any(item => x.ItemID.Contains(item.ItemID)));
                    if (itemInCartAlready.Any() && itemsTobeValidated.Any() && !itemsTobeValidated.First().ItemID.Equals("MF"))
                    {
                        // Hotfix Issue : Not enuf points - subTotalOfSingleItem = (subtotalOfItemWhichIsbeingAdded -subTotalFromDatabaseForThatItem)
                        var sumOfTotalFromIncomingList = itemsTobeValidated.Select(t => t.SubTotal).Sum();
                        var sumOfTotalFromDb = itemInCartAlready.Select(z => z.SubTotal).Sum();
                        subTotalOfSingleItem = sumOfTotalFromIncomingList > sumOfTotalFromDb ? sumOfTotalFromIncomingList - sumOfTotalFromDb : sumOfTotalFromDb - sumOfTotalFromIncomingList;
                    }
                    else {
                        subTotalOfSingleItem = (itemsTobeValidated.Count > 0) ? itemsTobeValidated.Select(t => t.SubTotal).Sum() : 0;
                    }
                    
                    if (subTotalOfSingleItem <= CartTotal.AvailablePoints)
                    {
                        jsonResponse.RetCode = "0";
                    }
                    else {
                        jsonResponse.RetCode = "1";
                        string customError = isUpdate ? "Your cart cannot be updated because the total required rewards exceeds the amount of available rewards you have earned. The quantity in cart has been adjusted to the prior quantity submitted." : "The selected item cannot be added to your cart because the total required rewards exceeds the amount of available rewards you have earned.";
                        jsonResponse.errors.Add(customError);
                    }

                }
                else if (CartTotal.AvailablePoints >= 0 && isUpdate)
                {
                    jsonResponse.RetCode = "0";
                }
                else
                {
                    string customError = string.Empty;
                    jsonResponse.RetCode = "1";
                    // This is to match JS message
                    customError = isUpdate ? "Your cart cannot be updated because the total required rewards exceeds the amount of available rewards you have earned. The quantity in cart has been adjusted to the prior quantity submitted." : "The selected item cannot be added to your cart because the total required rewards exceeds the amount of available rewards you have earned.";
                    jsonResponse.errors.Add(customError);
                }
            }
            return jsonResponse;
        }

        public ActionResult PointsSection()
        {
            DebugUtils.StartLogEvent("RewardsController.PointsSection");
            var model = GetLayoutItem<PointsSummary>();
            model = OwnerUtils.GetPointsSummary(model);
            DebugUtils.EndLogEvent("RewardsController.PointsSection");
            return View(model);
        }

        #region "Ajax Get Methods"

        [HttpGet]
        public ActionResult ReferFriend()
        {
            var referFriend = GetLayoutItem<ReferFriend>();
            return View(referFriend);
        }

        
        [HttpGet]
        public ActionResult GetAllCartItemsAsync()
        {
          
            return PartialView("RewardsCart", GetAllCartItems());
        }

        [HttpGet]
        public ActionResult GetCartTotalAsync()
        {
            var cartItems = GetAllCartItems();
            BlueGreenContext bgContext = new BlueGreenContext();
            var cartTotalResult = new CartTotal()
            {
                AvailableRewards = (bgContext.bxgOwner !=  null && bgContext.bxgOwner.EncoreBenefits != null) ? bgContext.bxgOwner.EncoreBenefits.DividendsBalance : 0,
                TotalRewardsinCart = (cartItems.Count > 0) ? cartItems.Select(t => t.SubTotal).Sum() : 0,
            };

            cartTotalResult.AvailablePoints = cartTotalResult.AvailableRewards - cartTotalResult.TotalRewardsinCart;

            return PartialView("RewardsCartTotal", cartTotalResult);
        }

        #endregion

        #region "Ajax Post Methods"

        [HttpPost]
        public ActionResult DeleteRewardsCart(string json)
        {
            ActionResult result = Json(new { Response = "Error" });
            try
            {
                

                var cartItemTobeDeleted = new CartItem
                {
                    ItemID = Request.QueryString["rC"],
                    Arvact = Request.QueryString["oA"]

                };

                if (DBManager.DeleteRewardCartItem(cartItemTobeDeleted)) {
                    result = Json(new { Response = "Success" });
                }

            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Unexpected exception deleting cart item", ex);
            }

            return result;
        }

        [HttpPost]
        public ActionResult CalculateMaintenenceCalculate(string amountwithdividents)
        {

            bool bIsValid = true;           
            double dblDollarAmountWithDividends = 0;
            double dblAmountPaidWithDividends = 0;
            double dblBalanceDue = 0;
            bool validateInputStatus = true;
            string cashvalue = "";
            double lblMaintenanceBalanceDue = 0;
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "0";  //default to error
            BlueGreenContext bgContext = new BlueGreenContext();

            try
            {
                // Null Check + Invalid characters + numbers < 1 // Invalid Input
                if (string.IsNullOrEmpty(amountwithdividents) ||  !double.TryParse(amountwithdividents, out dblDollarAmountWithDividends) || Convert.ToDouble(amountwithdividents) < 1)
                {
                    jsonResponse.RetCode = "10";  
                    jsonResponse.errors.Add("Please enter only numbers (0-9) in this field.");
                    validateInputStatus = false;
                }
                else
                {                  
                    amountwithdividents = amountwithdividents.Replace("$", "");
                                      
                    //if (!isCartItemValid(Convert.ToDecimal(amountwithdividents)))
                    //{
                    //    jsonResponse.RetCode = "15";  // Invalid Input
                    //    jsonResponse.errors.Add("The selected item cannot be added to your cart because the total required rewards exceeds the amount of available rewards you have earned.");
                    //}
                    //else
                    //{

                        dblDollarAmountWithDividends = Convert.ToDouble(amountwithdividents);

                        //Amount Paid With Dividends
                        dblAmountPaidWithDividends = dblDollarAmountWithDividends / 100;
                        cashvalue = dblAmountPaidWithDividends.ToString("#0.00");

                        //Balance Due
                        if (bgContext.bxgOwner.PaymentBalance > 0)
                        {
                            dblBalanceDue = bgContext.bxgOwner.PaymentBalance;
                            lblMaintenanceBalanceDue = dblBalanceDue - dblAmountPaidWithDividends;
                        }

                    //}
                    

                }
            }
            catch (Exception ex)
            {
                jsonResponse.RetCode = "11";  // Invalid Input
                validateInputStatus = false;
                jsonResponse.errors.Add("Unexpected exception occured ,Please try again ");
            }

            return Json(new {JsonResponse= jsonResponse, MaintenanceBalanceDue = lblMaintenanceBalanceDue, cashvalue = cashvalue }, JsonRequestBehavior.AllowGet);
        }

       
        [HttpPost]      
        public ActionResult ReferFriend(string FirstName,string LastName,string EmailId,string PhoenNumber,string City,string State,string Destination,string Message, bool IsSendMailChecked)
        {
           
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "1";  //default to error
            Models.ReferFriend objRefFri = new Models.ReferFriend();
            objRefFri.txtFirstName = FirstName;
            objRefFri.txtLastName = LastName;
            objRefFri.txtEmail = EmailId;
            objRefFri.txtPhone = PhoenNumber;
            objRefFri.txtDestination = Destination;
            objRefFri.txtCity = City;
            objRefFri.txtState = State;
            objRefFri.txtMessage = Message;
            objRefFri.IsSendMailChecked = IsSendMailChecked;

            SaveReferralResponse objResponse = new SaveReferralResponse();
            ReferralService objReferal = new ReferralService();
            objResponse = objReferal.ExecuteSaveReferral(objRefFri);

            if (objResponse.Success=="true")
            {               
                jsonResponse.RetCode = "0";
            }
            else
            {
                if (objResponse.Errors.Count>0)
                {
                    jsonResponse.errors.Add(objResponse.Errors[0].ShortText);
                }
                else
                {
                    jsonResponse.errors.Add("Error While Submitting Referral");
                }
                
            }

            return Json(jsonResponse, JsonRequestBehavior.AllowGet);
                        
        }

        [HttpPost]
        public ActionResult PlaceOrderForGiftCards()
        {
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "1";  //default to error
           

            try {
                var requestObject = GetAllCartItems();

                RewardResponse objResponse = new RewardResponse();
                RewardsService objReward = new RewardsService();
                objResponse = objReward.SendDataToProcess(requestObject);

                if (objResponse.Errors.Count > 0)
                {
                    jsonResponse.errors.Add(objResponse.Errors[0].ShortText);
                }
                else if (objResponse.Account.Memberships.BluegreenRewardsMembership.Returns.Count > 0)
                {
                    if(objResponse.Account.Memberships.BluegreenRewardsMembership.Returns[0].RetCode == "1")
                    {
                        jsonResponse.RetCode = "0";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(objResponse.Account.Memberships.BluegreenRewardsMembership.Returns[0].CodeDesc))
                        {
                            jsonResponse.errors.Add(objResponse.Account.Memberships.BluegreenRewardsMembership.Returns[0].CodeDesc);
                        }
                        else
                        {
                            jsonResponse.errors.Add("Error While Redeeming Rewards. Please Try Again.");
                        }

                    }
                }
            }
            catch (Exception ex) {
                jsonResponse.errors.Add("Error: "+ ex.Message);
            }
            
            return Json(jsonResponse);
        }

        [HttpPost]
        public ActionResult AddItemToRewardCart(CartItem itemToBeAdded)
        {

            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "1";          
            bool success = false;
           
            List<CartItem> lstOfItemToBeAdded = new List<CartItem>();
            lstOfItemToBeAdded.Add(itemToBeAdded);
            List<CartItem> cartItems = GetAllCartItems();

            jsonResponse = validateCartItems(lstOfItemToBeAdded, cartItems);
            if (!jsonResponse.errors.Any())
            {
                success = DBManager.InsertUpdateCartItems(lstOfItemToBeAdded);
                if (success){
                    jsonResponse.RetCode = "0";
                }
                else{
                    jsonResponse.errors.Add("Error While Adding to Cart. Please Try again.");
                }
            }
            return Json(jsonResponse);
        }
        
        [HttpPost]
        public ActionResult UpdateToRewardCart(List<CartItem> itemsCombined)
        {
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "1";
            bool success = false;
           
            jsonResponse = validateCartItems(itemsCombined, itemsCombined,true);
            if (!jsonResponse.errors.Any())
            {
                success = DBManager.InsertUpdateCartItems(itemsCombined, true);
                if (success)
                {
                    jsonResponse.RetCode = "0";
                }
                else
                {
                    jsonResponse.errors.Add("Error While Adding to Cart. Please Try again.");
                }
            }
            else if(itemsCombined.Where(x => x.Quantity == 0).Any()) {

                var itemsToBeDeleted = itemsCombined.Where(x => x.Quantity == 0).ToList();
                success = DBManager.InsertUpdateCartItems(itemsToBeDeleted, true);
                if (success)
                {
                    jsonResponse.RetCode = "0";
                }
                else
                {
                    jsonResponse.errors.Add("Error While Adding to Cart. Please Try again.");
                }
            }
            return Json(jsonResponse);
        }

        [HttpPost]
        public ActionResult ReferralsResendOffers(List<ReferFriend> itemsToBeReferred)
        {
            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "1";

            try {

                ReferralService objReferal = new ReferralService();
                BlueGreenContext bgContext = new BlueGreenContext();

                var filteredReferralsWithEmails = itemsToBeReferred.Where(x => !string.IsNullOrEmpty(x.txtEmail) && x.txtEmail != "");
                if (filteredReferralsWithEmails.Any())
                {
                    foreach (ReferFriend eachReferer in filteredReferralsWithEmails)
                    {
                        SendDataToResponsysReq requestObject = new SendDataToResponsysReq();
                        SaveReferralResponse emailResponse = new SaveReferralResponse();
                        if (bgContext != null && bgContext.bxgOwner != null)
                        {
                            requestObject = objReferal.PrepareResponsys(eachReferer, bgContext);
                            emailResponse = objReferal.SubmitResponsys(requestObject);
                        }
                        if (emailResponse.Status == "true")
                        {
                            jsonResponse.RetCode = "0";
                        }
                        else
                        {
                            if (emailResponse.Errors != null && emailResponse.Errors.Count > 0)
                            {
                                Sitecore.Diagnostics.Log.Error("Unexpected exception while resending offer(ReferralsResendOffers)", emailResponse.Errors[0].ShortText);
                            }
                            jsonResponse.errors.Add("Oops! Something went wrong. Please try sending again in a few minutes. If the issue persists, please call Owner Services at 866-362-6733.");
                            break;
                        }
                    }
                }
                else {
                    jsonResponse.errors.Add("Please Stop Tampering Html Data !!!");
                }
                
            }
            catch (Exception ex) {

                jsonResponse.RetCode = "1";
                jsonResponse.errors.Add("Oops! Something went wrong. Please try sending again in a few minutes. If the issue persists, please call Owner Services at 866-362-6733.");

                Sitecore.Diagnostics.Log.Error("Unexpected exception while resending offer(ReferralsResendOffers)", ex);
            }
            
            return Json(jsonResponse);
        }
        #endregion

    }
}