using System.Web.Mvc;
using Glass.Mapper.Sc.Web.Mvc;
using System.Linq;
using Sitecore.Rules;
using System.Xml.Linq;
using BGSitecore.Models.OwnerProxy;
using System.Collections.Generic;
using BGSitecore.Models;
using BGSitecore.Components;
using System;

namespace BGSitecore.Controllers
{
    public class OwnerProxyController : GlassController
    {
        
        public ActionResult OwnerProxySection()
        {
            var model = GetLayoutItem<OwnerProxySections>();

           // var isValidVotingPeriod = Components.DBManager.isValidVotingPeriod();
            

            //TODO mode this code in central place to evaluate rules
            foreach (OwnerProxySection section in model.AllSections)
            {

                bool showUsingRules = true;

                if (section.InnerItem.Fields["{85B5A4F7-0974-4455-85FA-B33FFAF397E0}"] != null)
                {
                    string rule = section.InnerItem.Fields["{85B5A4F7-0974-4455-85FA-B33FFAF397E0}"].Value;
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

        /// <summary>
        /// Initial Check for Knowing valid Voting period,Designated Owner and User standing
        /// </summary>
        /// <param name="none"></param>
        /// <returns>Json Response</returns>
        [HttpPost]
        public ActionResult ValidateVotingRightsForOwner()
        {

            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "0";  //default to error

            if (jsonResponse.errors.Count <= 0)
            {
                try
                {
                    BlueGreenContext bgContext = new BlueGreenContext();
                    double ZeroDollars = Convert.ToDouble(0);
                   
                    //Check For Valid Voting Period
                    if (!DBManager.isValidVotingPeriod())
                    {
                        jsonResponse.RetCode = "invalidvotingperiod";
                        jsonResponse.errors.Add("The Voting Period is not currently open. Please visit during a Voting Period.");
                    }
                    //Check whether designated voter
                    else if (bgContext.bxgOwner != null && !DBManager.isDesignatedVoter(bgContext.bxgOwner.Arvact))
                    {
                        jsonResponse.RetCode = "designatedOwner";
                        jsonResponse.errors.Add("Your account is not eligible for online voting. Please print and mail in a paper ballot");

                    }
                    else if (bgContext.bxgOwner != null && !bgContext.bxgOwner.userInGoodStanding)
                    {
                        jsonResponse.RetCode = "notgoodstanding";
                        jsonResponse.errors.Add("Due to the status of your account, you are not currently eligible to vote. Please call 800-456-2582 for assistance.");
                    }
                    else if (bgContext.bxgOwner != null && bgContext.bxgOwner.ReservationDuePaymentBalance > ZeroDollars && bgContext.bxgOwner.InstallmentPlan[0].InstallmentStatus != "IP")
                    {
                        jsonResponse.RetCode = "notgoodstanding";
                        jsonResponse.errors.Add("Due to the status of your account, you are not currently eligible to vote. Please call 800-456-2582 for assistance.");
                    }
                }
                catch (Exception ex) {
                    jsonResponse.RetCode = "-1";
                    jsonResponse.errors = new List<string>();
                    jsonResponse.errors.Add("Internal error try again later.");
                   // jsonResponse.errors.Add("Message:" +ex.Message + "Stack:" + ex.StackTrace + "InnerMess:" +ex.InnerException);
                }

            }
            return Json(jsonResponse);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="candididateName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmProxyVoting(string candididateName , bool isWriteinCandidate)
        {

            JsonResponse jsonResponse = new JsonResponse();
            jsonResponse.errors = new List<string>();
            jsonResponse.RetCode = "0";  //default to error

            if (jsonResponse.errors.Count <= 0)
            {
                //Check For Valid Voting Period
                if (!DBManager.isValidVotingPeriod())
                {
                    jsonResponse.RetCode = "invalidvotingperiod";
                    jsonResponse.errors.Add("The Voting Period is not currently open. Please visit during a Voting Period.");
                }
                else {
                    var response = DBManager.CastVoteByName(candididateName.Trim(), isWriteinCandidate);
                    if (!response)
                    {
                        jsonResponse.RetCode = "-1";
                        jsonResponse.errors.Add("Vote Could not be submitted.Please try Again.");

                    }
                }
            }
            return Json(jsonResponse);
        }
    }
}