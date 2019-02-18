using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;
using BGSitecore.Models.Resort;
using BGSitecore.Models.ResortService.ReservationsList;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class ResortPreviewDetails
    {
 
        public const string VillaTypeHeaderId = "{AD728C54-C873-41BA-AD43-DE2EC5B59B5E}";
        public const string CheckinHeaderId = "{7C5EC1AB-7F7B-42EA-A0C2-1B5ADD06927D}";
        public const string CheckouHeaderId = "{A747C86B-6B92-4FE7-A97D-2A749D3F9A47}";
        public const string MaxoccupanceHeaderId = "{61F5E830-EB73-4189-8BBD-625960B65CC6}";
        public const string AmountHeaderId = "{F59D584E-F97A-4D80-8364-03CB0AED6533}";
        public const string ImportantNotesId = "{F4C3EE48-82D7-4E4E-B5BF-0D10685F4387}";
        public const string PointsId = "{4B7EB59E-D336-4B7D-B6A1-6151C820365A}";
        public const string SleepsUpToId = "{A8B229CB-BE2C-4D4F-8A5B-66D3E5239F1F}";
      
        [SitecoreField(FieldName = VillaTypeHeaderId)]
        public virtual string VillaTypeHeader { get; set; }

        [SitecoreField(FieldName = CheckinHeaderId)]
        public virtual string CheckinHeader { get; set; }

        [SitecoreField(FieldName = CheckouHeaderId)]
        public virtual string CheckouHeader { get; set; }

        [SitecoreField(FieldName = MaxoccupanceHeaderId)]
        public virtual string MaxoccupanceHeader { get; set; }

        [SitecoreField(FieldName = AmountHeaderId)]
        public virtual string AmountHeader { get; set; }

        [SitecoreField(FieldName = ImportantNotesId)]
        public virtual string ImportantNotesLabel { get; set; }

        [SitecoreField(FieldName = PointsId)]
        public virtual string PointsLabel { get; set; }

        [SitecoreField(FieldName = SleepsUpToId)]
        public virtual string SleepsUpTolabel { get; set; }

        public ResortDetails ResortDetail { get; set; }

        public Reservation ActiveReservation { get; set; }

        public bool HandicapAccessible { get; set; }

    }
}