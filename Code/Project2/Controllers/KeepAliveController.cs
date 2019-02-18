using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class KeepAliveController : SurfaceController
    {
        // GET: KeepAlive
        public ActionResult KeepAlive()
        {
            return Content("OK");
        }
    }
}