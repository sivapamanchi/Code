using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class AccountModel
    {

        public int AccNo { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string NextEarnDate { get; set; }
        public string NextEarnAmount { get; set; }

    }
}