using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models
{
    [Serializable]
    public class SummerMonth
    {
        public const string FromDateIn = "{4355C9E2-CCA3-44BF-864B-E686135031DB}";
        public const string ToDateId = "{6938006B-052B-4AB4-B975-A15B3E7CAB1E}";
        public const string AllowBookingResortListId = "{EBCA405E-476B-4DAB-B477-2BE7B44A5E13}";
  
        [SitecoreField(FieldName = FromDateIn)]
        public virtual DateTime FromDate { get; set; }

        [SitecoreField(FieldName = ToDateId)]
        public virtual DateTime ToDate { get; set; }

        [SitecoreField(FieldName = AllowBookingResortListId)]
        public virtual IEnumerable<ResortDetails> AllowBookingResortList { get; set; }

    }
}