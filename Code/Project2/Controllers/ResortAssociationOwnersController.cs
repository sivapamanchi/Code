using BGModern.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace BGModern.Controllers
{
    public class ResortAssociationOwnersController : RenderMvcController
    {
        // GET: ResortAssociationOwners
        public ActionResult ResortAssociationOwners()
        {
            var model = ResortMapper.MapForAssociationOwners(CurrentPage.Parent);

            model = MasterMapper.Map(model, CurrentPage.Parent);

            return View(model);
        }
    }
}