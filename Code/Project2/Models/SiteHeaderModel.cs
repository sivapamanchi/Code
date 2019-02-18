using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class SiteHeaderModel
    {
        public bool IsFixedFlexOrTraditionalOwner { get; set; }
        public string ContractType { get; set; }
        public string NavigatorType { get; set; }
        public string PhoneNumberImage { get; set; }
        public string PhoneNumberAltText { get; set; }
        public string PhoneNumberURL { get; set; }
    }
}