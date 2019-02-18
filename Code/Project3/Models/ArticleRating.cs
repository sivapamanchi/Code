using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using static BGSitecore.Validator.TranslatedValidator;
using System.Web.Mvc;

namespace BGSitecore.Models
{
    public class ArticleRating
    {

        public const string RatingRequestDetailsId = "{2518237D-75F1-4DD0-B4F2-1BBE38B4CE0E}";
        public const string GrayedOutFeedbackTextId = "{630A2BC8-54AE-42F7-BA87-68ADEBFD66E6}";
        public const string RatingSubmitButtonTextId = "{264A9356-8797-43BC-86B2-38D074653189}";
        public const string RatingCharacterLimitId = "{B5D1741C-7471-4D56-88C9-B462E68D54CF}";
        public const string RatingSuccessMessageId = "{510539BD-3AFC-4DC7-9590-671AECDB336E}";

        public const string ContactUsTextId = "{AE05D74A-59AC-4503-B17F-A61CE99EAACF}";

        public const string ContactUsLinkId = "{C3F7E9E7-A8E0-4E1F-A6C4-9837069A34EF}";

        [SitecoreField(FieldName = RatingRequestDetailsId)]
        public virtual string RatingRequestDetails { get; set; }

        [SitecoreField(FieldName = GrayedOutFeedbackTextId)]
        public virtual string GrayedOutFeedbackText { get; set; }

        [SitecoreField(FieldName = RatingSubmitButtonTextId)]
        public virtual string RatingSubmitButtonText { get; set; }

        [SitecoreField(FieldName = RatingCharacterLimitId)]
        public virtual string RatingCharacterLimit { get; set; }

        [SitecoreField(FieldName = RatingSuccessMessageId)]
        public virtual string RatingSuccessMessage { get; set; }

        [SitecoreField(FieldName = ContactUsTextId)]
        public virtual string ContactUsText { get; set; }

        [SitecoreField(FieldName = ContactUsLinkId)]
        public virtual string ContactUsLink { get; set; }


        public string FeedbackReference { get; set; } = string.Empty;

        public string ArticleTitle { get; set; } = string.Empty;

        public string ArticlePath { get; set; } = string.Empty;
        public string UserReference { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public int ThumbsRating { get; set; } 
        public string ReviewDetails { get; set; } = string.Empty;
        public DateTime ReviewDatetime { get; set; }

        public bool FeedbackSaved { get; set; }
        

    }
       
}