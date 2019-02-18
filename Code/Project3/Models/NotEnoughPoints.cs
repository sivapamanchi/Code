using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models;
using Glass.Mapper.Sc.Fields;
using Glass.Mapper.Sc.Configuration;

namespace BGSitecore.Models
{
    [SitecoreType]
    public class NotEnoughPoints
    {
        public const string ItemId = "{675A9CD4-B861-4C45-A7F7-BB2B41F20E29}";

        public const string TotalPointsRequiredId = "{4E2D0E4A-52F0-4036-82FC-30C55BDD56DA}";
        public const string TotalEligiblePointsId = "{02087110-218F-404A-89DB-DF71DCCACD46}";
        public const string PointTypeId = "{53322B4C-89F5-4571-B76D-07191933A17E}";
        public const string AvailablePointsId = "{9DD8CADA-DFE5-40CE-BC37-8F8A83D5CF72}";
        public const string ExpirationDateId = "{20416FC9-A569-4596-AE69-85BAFA325795}";
        public const string EligibilityId = "{ED97F406-8774-476F-92DF-1A4300F59D5F}";
        public const string BorrowPointsMessageId = "{E5C0C399-D728-4468-B950-46D96F147E7B}";
        public const string WeekLableId = "{26DB346A-F04B-447B-A510-70A2A6EBE434}";
        public const string NotEligibleErrorId = "{13638A55-5FC0-492C-A1CF-F5441DB334C5}";
        public const string AccoutnStatusErrorId = "{3C49F880-4CD5-4894-9327-AF1FEB404F7E}";
        public const string TechnicalIssueErrorId = "{9F25EF32-7EAC-4744-A758-C45E0BA3D686}";
        public const string ErrorTryAgainId = "{AF7DBE62-36F2-4EAF-96DA-4DCAA6DB8BAE}";
        public const string ErrorPointExpireBeforeCheckoutId = "{031FDF66-4AD8-4672-8320-593E6E4542E1}";
        public const string ReservationExceedsLimitId = "{8FED4ECC-9098-4A9B-BBCC-6F84C851A77C}";
        public const string SavePointsToContinueId = "{6CDFE12C-78F9-42FB-8E20-9BA3010E6E8E}";

        [SitecoreField(FieldName = TotalPointsRequiredId)]
        public virtual string TotalPointsRequired { get; set; }

        [SitecoreField(FieldName = TotalEligiblePointsId)]
        public virtual string TotalEligiblePoints { get; set; }

        [SitecoreField(FieldName = PointTypeId)]
        public virtual string PointType { get; set; }

        [SitecoreField(FieldName = AvailablePointsId)]
        public virtual string AvailablePoints { get; set; }

        [SitecoreField(FieldName = ExpirationDateId)]
        public virtual string ExpirationDate { get; set; }

        [SitecoreField(FieldName = EligibilityId)]
        public virtual string Eligibility { get; set; }

        [SitecoreField(FieldName = BorrowPointsMessageId)]
        public virtual string BorrowPointsMessage { get; set; }

        [SitecoreField(FieldName = WeekLableId)]
        public virtual string WeekLable { get; set; }

        [SitecoreField(FieldName = NotEligibleErrorId)]
        public virtual string NotEligibleError { get; set; }

        [SitecoreField(FieldName = AccoutnStatusErrorId)]
        public virtual string AccoutnStatusError { get; set; }

        [SitecoreField(FieldName = TechnicalIssueErrorId)]
        public virtual string TechnicalIssueError { get; set; }

        [SitecoreField(FieldName = ErrorTryAgainId)]
        public virtual string ErrorTryAgain { get; set; }

        [SitecoreField(FieldName = ErrorPointExpireBeforeCheckoutId)]
        public virtual string ErrorPointExpireBeforeCheckoutMessage { get; set; }

        [SitecoreField(FieldName = ReservationExceedsLimitId)]
        public virtual string ReservationExceedsLimit { get; set; }

        [SitecoreField(FieldName = SavePointsToContinueId)]
        public virtual string SavePointsToContinue { get; set; }

    }
}