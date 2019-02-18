using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Services;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class AlertController : GlassController
    {

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult PageAlerts()
        {
            DebugUtils.StartLogEvent("AlertController.PageAlerts");
            var model = GetLayoutItem<AllAlerts>();
            if (model.AllAlert != null && model.AllAlert.Count() > 0)
            {
                List<Alert> allAllerts = (List<Alert>)model.AllAlert;

                foreach (var alert in allAllerts.Reverse<Alert>())
                {
                    if (!SitecoreUtils.EvaluateRules(alert.AlertRules, ContextItem))
                    {
                        allAllerts.Remove(alert);
                    }
                    else
                    {
                        BlueGreenContext bgContext = new BlueGreenContext();
                        if (alert.AlertText.Contains("{TPNoLongerAvailableDate}"))
                        {
                            if (bgContext.OwnerExpiration != null)
                            {
                                string valueToUpdate = bgContext.OwnerExpiration.Value.AddYears(1).ToString("MM/dd/yyyy");
                                alert.AlertText = alert.AlertText.Replace("{TPNoLongerAvailableDate}", valueToUpdate);
                            }
                        }
                        if (alert.AlertText.Contains("{WaiverCount}"))
                        {
                            ResortService service = new ResortService();
                            var waiverCount = service.OwnerWaivers(FormatUtils.ConvertStringToInt( bgContext.OwnerId));
                            if (waiverCount != null && waiverCount.OwnerWaivers != null)
                            {
                                alert.AlertText = alert.AlertText.Replace("{WaiverCount}", waiverCount.OwnerWaivers.WaiversAvailable);
                            }
                            else
                            {
                                allAllerts.Remove(alert);
                            }
                        }
                        if (alert.AlertText.Contains("{PointsExpirationDate}"))
                        {
                            DateTime? pointsExpirationDate = bgContext?.GetPointsExpireDate();
                            if (pointsExpirationDate != null)
                            {
                                string valueToUpdate = pointsExpirationDate.Value.ToString("MM/dd/yyyy");
                                alert.AlertText = alert.AlertText.Replace("{PointsExpirationDate}", valueToUpdate);
                            }
                        }
                    }
                }
                model.AllAlert = allAllerts;
                DebugUtils.EndLogEvent("AlertController.PageAlerts");

            }
            return View(model);
        }
    }
}