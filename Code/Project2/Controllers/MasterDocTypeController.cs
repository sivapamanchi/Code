using BGModern.Mappers;
using BGModern.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class MasterDocTypeController : RenderMvcController
    {
        protected ViewResult View(MasterModel model)
        {
            return this.View(null, model);
        }

        protected ViewResult View(string view, MasterModel model)
        {
            var root = CurrentPage.AncestorOrSelf(1);

            model.SiteName = root.GetPropertyValue<string>("siteName");
            if (string.IsNullOrWhiteSpace(model.SiteName))
            {
                model.SiteName = root.Name;
            }

            model.Title = CurrentPage.GetPropertyValue<string>("title");
            if (string.IsNullOrWhiteSpace(model.Title))
            {
                model.Title = CurrentPage.Name;
            }

            model.MainNavigation = mapNavigation(root).ToList();

            return base.View(view, model);
        }

        private IEnumerable<NavigationItemModel> mapNavigation(IPublishedContent content)
        {
            return from c in content.Children
                   where c.IsVisible()
                   // Use the mapper to map items:
                   select NavigationItemMappers.Map(
                   new NavigationItemModel()
                   {
                       // Map children the same way as we have already mapped parents:
                       Children = mapNavigation(c)
                   }, c, CurrentPage);
        }
    }
}
