using BGModern.Mappers;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortWeatherController : RenderMvcController
    {
        // GET: ResortWeather
        public ActionResult ResortWeather()
        {
            var model = ResortMapper.MapForWeather(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}