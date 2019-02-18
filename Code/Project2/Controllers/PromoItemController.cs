using BGModern.Mappers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Configuration;

namespace BGModern.Controllers
{
    public class PromoItemController : RenderMvcController
    {
        public ActionResult PromoItem()
        {
            if (Session["BXGOwner"] == null)
            {
                if (Session["_path_info"] != null)
                    Session["_path_info"] = Request.RawUrl;

                Response.Redirect(ConfigurationManager.AppSettings["bxgwebUnsecureURL"] + "default.aspx?sess=timeout", true);
            }

            var model = PromoItemMapper.Map(CurrentPage);
            model = MasterMapper.Map(model, CurrentPage);
            string HostUrl = ConfigurationManager.AppSettings["SitecoreResortUrl"].ToString();
            string SitecoreMapperUrl = string.Empty;
            switch (Request.RawUrl.ToLower().Trim())
            {
                case "/bgmodern/promotions/bgr-qr":
                    SitecoreMapperUrl = "/DOPs/BGR-QR";
                    break;
                case "/bgmodern/promotions/be-a-friend-refer-a-friend-to-bluegreen":
                    SitecoreMapperUrl = "/rewards/sweepstakes";
                    break;
                case "/bgmodern/promotions/bgr-or":
                    SitecoreMapperUrl = "/DOPs/BGR-OR";
                    break;
                case "/bgmodern/promotions/sharing-is-now-more-rewarding":
                    SitecoreMapperUrl = "/featured/Sharing-is-Rewarding";
                    break;
                case "/bgmodern/promotions/bgr-qr/":
                    SitecoreMapperUrl = "/DOPs/BGR-QR";
                    break;
                case "/bgmodern/promotions/be-a-friend-refer-a-friend-to-bluegreen/":
                    SitecoreMapperUrl = "/rewards/sweepstakes";
                    break;
                case "/bgmodern/promotions/bgr-or/":
                    SitecoreMapperUrl = "/DOPs/BGR-OR";
                    break;
                case "/bgmodern/promotions/sharing-is-now-more-rewarding/":
                    SitecoreMapperUrl = "/featured/Sharing-is-Rewarding";
                    break;
            }
            if (SitecoreMapperUrl != string.Empty)
            {
                model.RedirectUrl = HostUrl + SitecoreMapperUrl;
            }
            if (string.IsNullOrWhiteSpace(model.RedirectUrl))
            {
                return View("PromoItem", model);
            }
            else
            {
                return Redirect(model.RedirectUrl);
            }
        }
    }
}