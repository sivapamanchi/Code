using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class MenuItemModel : MasterModel
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String ItemURL { get; set; }
    }
}