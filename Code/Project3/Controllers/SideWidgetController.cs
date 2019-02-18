using BGSitecore.Components;
using BGSitecore.Models;
using BGSitecore.Models.Teasers;
//using BGSitecore.Models.Teasers;
using BGSitecore.Utils;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Controllers
{
    public class SideWidgetController : GlassController
    {

        public ActionResult ShowWidget()
        {

            var model = GetLayoutItem<SideWidget>();
            BlueGreenContext userContext = new BlueGreenContext();
            if (SitecoreUtils.EvaluateRules(model.RestrictionRule, model.InnerItem))
            {
                foreach (var item in model.WidgetContents)
                {
                    item.isVisible = true;
                    if (SitecoreUtils.EvaluateRules(item.RestrictionRule, item.InnerItem))
                    {
                        if (item.WidgetLink != null)
                        {
                            item.WidgetLink.Url = UrlMapper.Map(item.WidgetLink.Url);
                        }
                        if (item.SubWidgetLink != null)
                        {
                            item.SubWidgetLink.Url = UrlMapper.Map(item.SubWidgetLink.Url);
                        }
                        if (item.SubWidgetText != null)
                        {
                            item.SubWidgetText = UpdateText(item.SubWidgetText, userContext);

                        }
                        if (item.SubWidgetLink != null && !string.IsNullOrEmpty(item.SubWidgetLink.Text))
                        {
                            item.SubWidgetLink.Text = UpdateText(item.SubWidgetLink.Text, userContext);

                        }

                    }

                    else
                    {
                        item.isVisible = false;
                    }
                }
            }
            else
            {
                model.WidgetContents = null;
                //model = null;
            }
            return View(model);

        }

        private string UpdateText(string originalString, BlueGreenContext userContext)
        {

            if (originalString != null)
            {
                originalString = originalString.Replace("{availablepoints}", userContext.GetPoints().ToString("N0"));
                if (originalString.Contains("{futurepoints}") && userContext.GetFuturePoints() == 0)
                {
                    originalString = "";
                }
                else
                {
                    originalString = originalString.Replace("{futurepoints}", userContext.GetFuturePoints().ToString("N0"));
                }
                originalString = originalString.Replace("{ownershiplevel}", userContext.GetOwnershipLevel());
                originalString = originalString.Replace("{bluegreenrewards}", FormatUtils.FormatPoints(userContext.GetRewards().ToString()));
                originalString = originalString.Replace("{expirationdate}", UiUtils.ConvertDateToString(userContext.OwnerExpiration));
                originalString = originalString.Replace("{paymentbalance}", userContext.GetBalance().ToString("C"));

                if (!string.IsNullOrEmpty(userContext.GetAvailableWeek()))
                {
                    originalString = originalString.Replace("{availableweeks}", userContext.GetAvailableWeek());

                }
            }

            return originalString;
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }

        public ActionResult TwitterWidget()
        {
            var model = GetDataSourceItem<SocialWidget>();

            if (model != null && SitecoreUtils.EvaluateRules(model.RestrictionRule, model.InnerItem))
            {
                model.IsAllowed = true;
            }
            return View(model);
        }

        public ActionResult FacebookWidget()
        {
            var model = GetDataSourceItem<SocialWidget>();
            if (model != null & SitecoreUtils.EvaluateRules(model.RestrictionRule, model.InnerItem))
            {
                model.IsAllowed = true;
            }
            return View(model);
        }
    }
}