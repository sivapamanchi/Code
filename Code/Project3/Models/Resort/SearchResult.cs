using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models.ResortService.InventoryCalendarByResortResponse;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class SearchResult: BasePage
    {
        public const string VillaTypeId = "{999CEAAF-8052-45CA-A5DC-2DBF4789F839}";
        public const string SeasonId = "{C70A2EFE-AF7D-47CA-AB77-3F546ED2B0B7}";
        public const string MaxOccupancyId = "{FBF3548E-76A4-4B1C-B074-6743ABCAA919}";
        public const string PointsId = "{46DA30D5-67D5-4C3D-894F-FD2E10F077A8}";
        public const string AvailabilityId = "{1748B7CA-AA61-47F8-8A5B-E40C9A83FF67}";
        public const string DailyrateId = "{C69AFB8D-B626-4D83-84A0-092070AA90AC}";
        public const string TotalPriceId = "{6A2C1C89-30DE-478F-BDDA-71BAC2FD0338}";
        public const string BookNowId = "{61BC9108-BE0B-4F9D-99CB-8D7AEBEF0E0F}";
        public const string ExploreResortId = "{E68A16CF-C49C-461A-8647-4B42FA6A4FC7}";
        public const string PointsLegendId = "{1843E4DF-C087-4EE0-BDDA-A6CB0553FF0F}";
        public const string AvailableId = "{B3D66714-94F9-420B-8A8F-816EBDB21874}";
        public const string ShowResortAvailabilityId = "{CDD7F057-7958-4CFD-8812-82C0169A46BF}";
        public const string InternalErrorMessageId = "{52C4EABB-1659-4FE2-905E-8E9CB40EF371}";
        public const string NoResultFoundmessageId = "{9E68F5B0-0528-4418-AECC-857F9EADF716}";
        public const string NotAllResortUsedId = "{138B36B1-DF25-435D-A026-33551E05E442}";
        public const string Sampler2NightStayMessageId = "{F53EB08A-821B-4E4D-8FBA-34A5FF9146D2}";
        public const string CheckinOutsideSearchWindowBTId = "{0EB6270E-29C2-4071-8E7F-E944148103EF}";
        public const string CheckinOutsideSearchWindowPointsId = "{DD073375-F73A-45D2-89A7-D03E12796A97}";
        public const string SamplerHolidayErrorId = "{DF08D2DC-5AB7-43C0-B6B2-F188E278FC96}";
        public const string SamplerSummerErrorId = "{3539D624-2A99-4CB2-AE0F-2498983EAA67}";
        public const string CheckinBetween48BT = "{1B819908-7153-4BD4-A7C1-8926F436F8B4}";
        public const string BookingDurationErrorId = "{35324044-6C68-4808-B851-E51FC696152B}";
        public const string BookingOneDayMessageBonusTimeId = "{24672637-393B-43B3-B4F5-E6B28860A9C9}";
        public const string BookingOneDayMessagePointsId = "{CFC113C2-FF4D-4932-A1E0-5E5A6FEA3B6E}";
        public const string PanamaCityMessageId = "{52BED65F-B107-4390-AA70-BC096A63E9A6}";
        public const string SuggestionMessageId = "{8B412734-6E0B-42FC-B3ED-C6653A6F4C54}";
        public const string SuggestionAllDestinationId = "{A1F0D6E9-4877-4243-A855-2E0CA12EE044}";
        public const string FilerNoResultFoundId = "{3FCDF460-CBC1-4292-B724-E912A33803F4}";

        [SitecoreField(FieldName = VillaTypeId)]
        public virtual string VillaType { get; set; }

        [SitecoreField(FieldName = SeasonId)]
        public virtual string Season { get; set; }

        [SitecoreField(FieldName = MaxOccupancyId)]
        public virtual string MaxOccupancy { get; set; }

        [SitecoreField(FieldName = PointsId)]
        public virtual string Points { get; set; }

        [SitecoreField(FieldName = AvailabilityId)]
        public virtual string Availability { get; set; }

        [SitecoreField(FieldName = DailyrateId)]
        public virtual string Dailyrate { get; set; }

        [SitecoreField(FieldName = TotalPriceId)]
        public virtual string TotalPrice { get; set; }

        [SitecoreField(FieldName = BookNowId)]
        public virtual string BookNow { get; set; }

        [SitecoreField(FieldName = ExploreResortId)]
        public virtual string ExploreResort { get; set; }

        [SitecoreField(FieldName = PointsLegendId)]
        public virtual string PointsLegend { get; set; }

        [SitecoreField(FieldName = AvailableId)]
        public virtual string Available { get; set; }

        [SitecoreField(FieldName = ShowResortAvailabilityId)]
        public virtual string ShowResortAvailability { get; set; }

        [SitecoreField(FieldName = InternalErrorMessageId)]
        public virtual string InternalErrorMessage { get; set; }

        [SitecoreField(FieldName = NoResultFoundmessageId)]
        public virtual string NoResultFoundmessage { get; set; }

        [SitecoreField(FieldName = NotAllResortUsedId)]
        public virtual string NotAllResortUsedMessage { get; set; }

        [SitecoreField(FieldName = Sampler2NightStayMessageId)]
        public virtual string Sampler2NightStayMessage { get; set; }

        [SitecoreField(FieldName = CheckinOutsideSearchWindowBTId)]
        public virtual string CheckinOutsideSearchWindowBT { get; set; }

        [SitecoreField(FieldName = CheckinOutsideSearchWindowPointsId)]
        public virtual string CheckinOutsideSearchWindowPoints { get; set; }

        [SitecoreField(FieldName = SamplerHolidayErrorId)]
        public virtual string SamplerHolidayError { get; set; }

        [SitecoreField(FieldName = CheckinBetween48BT)]
        public virtual string CheckinBetween48 { get; set; }

        [SitecoreField(FieldName = BookingDurationErrorId)]
        public virtual string BookingDurationError { get; set; }

        [SitecoreField(FieldName = SamplerSummerErrorId)]
        public virtual string SamplerSummerError { get; set; }

        [SitecoreField(FieldName = BookingOneDayMessageBonusTimeId)]
        public virtual string BookingOneDayMessageBonusTime { get; set; }

        [SitecoreField(FieldName = BookingOneDayMessagePointsId)]
        public virtual string BookingOneDayMessagePoints { get; set; }

        [SitecoreField(FieldName = PanamaCityMessageId)]
        public virtual string PanamaCityMessage { get; set; }

        [SitecoreField(FieldName = SuggestionMessageId)]
        public virtual string SuggestionMessage { get; set; }

        [SitecoreField(FieldName = SuggestionAllDestinationId)]
        public virtual string SuggestionAllDestination { get; set; }

        [SitecoreField(FieldName = FilerNoResultFoundId)]
        public virtual string FilerNoResultFound { get; set; }

        public virtual List<ResortDetails> AllResorts { get; set; }
        public virtual List<string> AvailableUnitTypeForDestination { get; set; }

        public virtual List<ResortDetails> AllResortsSecondaryMarket { get; set; }

		public virtual List<SearchMonth> AllSearchMonths { get; set; }
		public virtual List<SearchNumberOfNight> AllSearchNumberOfNights { get; set; }

		public SearchParameters searchParameters { get; set; }

        public IEnumerable<SearchResultResortList> resortList { get; set; }

        public bool ShowRevervationTypeSection { get; set; }

        public DateTime initialCheckInDate { get; set; }

        public DateTime initialCheckOutDate { get; set; }

        public List<SavedSearch> SavedSearches { get; set; }

        public bool SearchParametersNotValid { get; set; }

        public bool ShowInternalError { get; set; }

        public bool ShowSearchParameters { get; set; }

        public bool ShowNotAllResortUsed { get; set; }
        public bool ShowSampler2NightStayMessage { get; set; }
        public bool ShowCheckinOutsideSearchWindowBT { get; set; }
        public bool ShowCheckinMoreThen48BT { get; set; }
        public bool ShowCheckinOutsideSearchWindowPoints { get; set; }
        public bool ShowSamplerHolidayError { get; set; }
        public bool ShowSummerHolidayError { get; set; }
        public bool ShowBookingDurationError { get; set; }
        public bool ShowBooking1DayBonusTimeMessage { get; set; }
        public bool ShowBooking1DayPointsMessage { get; set; }
        public bool ShowPanamaCityMessage { get; set; }

		public bool ShowDateSearchOptions { get; set; }
		public bool ShowSearchBar { get; set; }

        public string CurrentResortName { get; set; }

        public string RawServiceError { get; set; }

        public List<Inventory> Inventories { get; set; }

 //       public List<string> ClosestSuggestedDates { get; set; }
  //      public List<string> AllSuggestedDates { get; set; }

        public bool isShowAvailabilityColumn { get; set; }
        public bool disableBooking { get; set; }

        [SitecoreQuery("/sitecore/content/Data/Owner/Lookup/Room Unit Size/*[@@templateid='{D66DDF18-3B01-4A05-88E9-F9C83E44B6AB}']", IsRelative = false)]
        public virtual IEnumerable<RoomSize> AllRoomSize { get; set; }

    }
}