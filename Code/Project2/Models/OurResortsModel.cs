using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class OurResortsModel : MasterModel
    {
        public int ResortCount { get; set; }
        public List<ResortModel> OurResorts { get; set; }
        public OurResortsPagerModel PagerModel { get; set; }
        public int Page { get; set; }
        public string FilterCity { get; set; }
        public string FilterState { get; set; }
        public string FilterExperience { get; set; }
        public bool VacationClubOwner { get; set; }
        public bool VacationClubOrSamplerOwner { get; set; }

        public OurResortsModel()
        {
            OurResorts = new List<ResortModel>();
        }
    }
}