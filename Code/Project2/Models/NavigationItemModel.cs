using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class NavigationItemModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public IEnumerable<NavigationItemModel> Children { get; set; }
    }
}