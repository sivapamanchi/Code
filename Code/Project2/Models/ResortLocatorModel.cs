using BGModern.Classes;
using BGModern.Models;
using BGModern.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class ResortLocatorModel
    {
        public List<String> Cities { get; set; }
        public Dictionary<String, List<Destination>> Destinations { get; set; }
        public string DestinationMap { get; set; }
        public Destination SelectedDestination { get; set; }
    }
}