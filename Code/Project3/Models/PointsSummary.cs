using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class PointsSummary
    {
        public const string TitleId = "{4EFC1E29-EE13-4E06-B052-873804621593}";
        public const string AnnualPointsId = "{76728B82-6A41-41D8-8344-C8C6F77C3EF7}";
        public const string SavedPointsId = "{DD59EA71-7660-4AB8-9E7B-5C3A9F475537}";
        public const string RestrictedPointsId = "{2CE650E2-294A-4893-9ADF-2D9051616F59}";
        public const string AvailablePointsId = "{78F970BB-963D-4FEA-9F7E-69F0FFFF27A2}";
        public const string SummaryRichTextId = "{4664B054-0D90-4619-BA86-EECAB3BB83B3}";

        [SitecoreField(FieldName = TitleId)]
        public virtual string Title { get; set; }

        [SitecoreField(FieldName = AnnualPointsId)]
        public virtual string AnnualPoints { get; set; }

        [SitecoreField(FieldName = SavedPointsId)]
        public virtual string SavedPoints { get; set; }

        [SitecoreField(FieldName = RestrictedPointsId)]
        public virtual string RestrictedPoints { get; set; }

        [SitecoreField(FieldName = AvailablePointsId)]
        public virtual string AvailablePoints { get; set; }

        [SitecoreField(FieldName = SummaryRichTextId)]
        public virtual string SummaryRichText { get; set; }

        [SitecoreIgnore]
        public virtual string AnnualPointsValue { get; set; }
        [SitecoreIgnore]
        public virtual string SavedPointsValue { get; set; }
        [SitecoreIgnore]
        public virtual string RewardsExpiringOn { get; set; }
        [SitecoreIgnore]
        public virtual bool printEnabled { get; set; } = false;
    }
}