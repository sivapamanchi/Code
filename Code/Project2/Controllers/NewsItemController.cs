using BGModern.Mappers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Configuration;

namespace BGModern.Controllers
{
    public class NewsItemController : RenderMvcController
    {
        public ActionResult NewsItem()
        {
            if (Session["BXGOwner"] == null)
            {
                if (Session["_path_info"] != null)
                    Session["_path_info"] = Request.RawUrl;

                Response.Redirect(ConfigurationManager.AppSettings["bxgwebUnsecureURL"] + "default.aspx?sess=timeout", true);
            }

            var model = NewsItemMapper.Map(CurrentPage);
            model = MasterMapper.Map(model, CurrentPage);

            if (string.IsNullOrWhiteSpace(model.RedirectUrl))
            {
                return View("NewsItem", model);
            }
            else
            {
                return Redirect(model.RedirectUrl);
            }
        }
    }
}