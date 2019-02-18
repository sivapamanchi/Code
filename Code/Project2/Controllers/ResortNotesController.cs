using BGModern.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortNotesController : RenderMvcController
    {
        // GET: ResortNotes
        public ActionResult ResortNotes()
        {
            var model = ResortMapper.MapForResortNotes(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}