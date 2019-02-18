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
    public class LastMinuteAvailabilityMonth
    {
        public const string DisplayOnSiteId = "{DA8464CC-82F2-4CDA-8CE1-CB3E2385F50A}";
        [SitecoreField(FieldName = DisplayOnSiteId)]
        public virtual bool DisplayOnSite { get; set; }

        public const string MonthId = "{41E81A4B-7E95-4E57-9370-24876F3FA139}";
        [SitecoreField(FieldName = MonthId)]
        public virtual int Month { get; set; }

        public const string YearId = "{2BFE9BAD-3810-47C6-9061-D4C920E9A869}";
        [SitecoreField(FieldName = YearId)]
        public virtual int Year { get; set; }

        public const string DisplayNameId = "{888E5B15-F50F-469A-89F0-7B261A071B5A}";
        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        public const string AvailableResortsId = "{69D79EF0-E962-41B7-85F7-DE12CAB8CB05}";
        [SitecoreField(FieldName = AvailableResortsId)]
        public virtual IEnumerable<ResortDetails> AvailableResorts { get; set; }

        public string BuildItemCss()
        {
            return string.Format("{0}-{1}", DisplayName.ToLower(), Year);
        }

        public bool IsFuture()
        {
            DateTime now = DateTime.Now;
            DateTime current = DateTime.Parse(string.Format("{0}/{1}/{2}", now.Month, 1, now.Year));
            DateTime date = DateTime.Parse(string.Format("{0}/{1}/{2}", Month, 1, Year));

            return date >= current;
        }

        public const string StartDateId = "{FF046875-13AF-4678-869E-49F82CB913EA}";
        [SitecoreField(FieldName = StartDateId)]
        public DateTime StartDate { get; set; }

        public const string EndDateId = "{3AB42CAC-E136-42C1-BED2-728DF34B1868}";
        [SitecoreField(FieldName = EndDateId)]
        public DateTime EndDate { get; set; }
    }
}