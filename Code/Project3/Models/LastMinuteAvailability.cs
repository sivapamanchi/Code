using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class LastMinuteAvailability : BasePage
    {
        public const string ContentsId = "{D405941C-9FA3-4B8A-B9A2-6E9CC796FCAE}";
        [SitecoreField(FieldName = ContentsId)]
        public virtual string Contents { get; set; }

        public const string ReservationId = "{D553FD0A-AC83-4552-8B41-DEBBC5892F1E}";
        [SitecoreField(FieldName = ReservationId)]
        public virtual Link Reservation { get; set; }

        public const string FootnotesId = "{7D159CC0-D6BD-4F7D-9B1B-03C4D9CE567C}";
        [SitecoreField(FieldName = FootnotesId)]
        public virtual string Footnotes { get; set; }

        [SitecoreQuery("./*", IsRelative = true)]
        public virtual IEnumerable<LastMinuteAvailabilityMonth> Children { get; set; }

    }
}