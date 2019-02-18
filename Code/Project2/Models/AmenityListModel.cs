using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class AmenityListModel : MasterModel
    {
        public List<AmenityItemModel> ResortAmentityItems { get; set; }
        public List<AmenityItemModel> ResortVillaItems { get; set; }
        public List<AmenityItemModel> ResortAreaItems { get; set; }

        public AmenityListModel()
            :base()
        {
            ResortAmentityItems = new List<AmenityItemModel>();
            ResortVillaItems = new List<AmenityItemModel>();
            ResortAreaItems = new List<AmenityItemModel>();
        }
    }
}