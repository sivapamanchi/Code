using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class OurResortsPagerModel
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int PriorPage { get; set; }
        public Boolean ShowPreviousControl { get; set; }
        public Boolean ShowNextControl { get; set; }
    }
}