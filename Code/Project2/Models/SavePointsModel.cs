using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class SavePointsModel
    {
        public string CreditCardName { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardCharge { get; set; }
        public string CreditCardType { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Zip { get; set; }
        public string isIntlZip { get; set; }
    }
}