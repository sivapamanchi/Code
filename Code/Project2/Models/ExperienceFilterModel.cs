using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BGModern.Classes;

namespace BGModern.Models
{
    public class ExperienceFilterModel
    {
        public IList<Destination> Destinations { get; set; }
        public IList<Experience> Experiences { get; set; }
    }
}