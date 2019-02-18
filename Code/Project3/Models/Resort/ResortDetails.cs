using BGSitecore.Models.Resort;
using BGSitecore.Models.ResortService.SearchReservationResponse;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class ResortDetails : BasePage
    {
        public const string ResortNameId = "{A3035AD3-070C-4933-B557-D53757B6BCD8}";
        public const string ResortIdId = "{38F7A43A-47E1-46C7-8BDE-96D5FD4279C2}";
        public const string ResortSummaryId = "{45ECB6DA-B7C9-4705-95CC-BB2848751230}";
        public const string ResortImagesId = "{0FF75AAC-D793-4738-8AED-67C210D0D7AA}";
        public const string PhoneNumberId = "{014A4A6B-5409-41C1-ACA0-3200BDE5B29C}";
        public const string ClubAffiliationId = "{192DE2B1-487B-44D7-B418-7ABF693F8E0A}";
        public const string ExperiencesId = "{F4120C1F-BF8D-4610-916C-58BC68604BA3}";
        public const string AccuWeatherCityCodeId = "{46DF7E76-B3C3-4A08-BBCB-A754D3D7E88A}";
        public const string ReviewIdId = "{167349E9-06B5-46D2-839E-46F4481D8038}";
        public const string FullWeekResortId = "{6ED09CB9-4A81-4617-BFCA-597A1BC641BD}";
        public const string IsFeaturedId = "{FBB95432-B96D-4605-A60B-1F569038B57F}";
       // public const string OwnerOnlyId = "{B8E8D742-D01D-426B-BE1E-F33385606A7E}";
        public const string MainResortImageId = "{7FF44739-0E6D-49E1-A044-6698C5EE6EFC}";
        public const string HomePageBackgroundImageId = "{9A1951AD-BDEA-4EFA-A85A-9B27D9E68590}";
        public const string IsActiveId = "{94135E76-D321-41CA-826B-6A3F1B0361A4}";
        public const string ResortTypeId = "{2FDDEDB3-135B-434B-BBD7-9CD280889BDA}";
        public const string TSWSiteNumberId = "{AD9A4BBD-0FC5-422A-861A-CC7DAEBE17D8}";
        public const string DatabaseIdId = "{D2C836FC-B090-4DF7-8D16-529792721DF7}";
        public const string ReservationPhoneNumberId = "{C04ED0BF-FA0E-4A4E-8A04-BF7A96B2F4DD}";
        public const string ShowGatewayWidgetId = "{882F1A9F-44A4-4425-913E-9C63C1D77D44}";
        public const string AllowSamplerSummerBookingId = "{2E592C33-2E8B-4F8C-BD22-0C591C895F66}";
        public const string TaxRateId = "{54EFC5CE-58FD-40FB-84E3-13045AC5D18D}";

        //Points fields
        public const string AllowPointsBookingId = "{0D53791A-39A0-487E-AEC3-FCEA4942EBB9}";
        public const string MinimumNightStayPointsId = "{99CFEC39-8543-4F5A-B66B-358974669808}";
        public const string MaximumNightStayPointsId = "{4B942D7B-8C6A-4B46-AE0E-CFDFA13E5666}";
        public const string AdvanceSearchWindowPointsId = "{C8089828-B96D-4884-9892-EE55F966FBCA}";
        public const string MinimumNightChargePointsId = "{A5B0D1EA-4A62-4E38-BA0E-6FA684FAF64D}";
        public const string RestrictionsPointsId = "{EB78FBB1-A949-46C3-8525-391E4A63FDFC}";
        public const string TwoNightChargeForOneNightStayPTSId = "{03E7A948-9B55-4ECE-B4B0-7FBCBDA768F9}";


        //Bonus Time fields
        public const string AllowBonusTimeBookingId = "{A1661BE7-6748-4E64-A6EB-ACF7618A9550}";
        public const string MinimumNightStayBonusTimeId = "{D54B85AA-35A6-4262-8483-F274CA4621EE}";
        public const string MaximumNightStayBonusTimeId = "{D3E662F4-C930-46A9-B114-B8B03F939F2A}";
        public const string AdvanceSearchWindowBonusTimeId = "{5AE26A67-84B7-44B6-A39F-83C1D8A496F2}";
        public const string MinimumNightChargeBonusTimeId = "{B3B903F4-250F-4C24-B3A2-5EF9DE64AEE2}";
        public const string RestrictionsBonusTimeId = "{044BD494-E1D3-4E4F-B6DB-6CAB4548D041}";
        public const string TwoNightChargeForOneNightStayBTId = "{0BFB5BB7-FE85-4821-98B2-FCC5582E67B7}";

        //Visibility
        public const string ShowInDestinationsId = "{3C0C1DC3-9947-472B-9991-A9805882C0D0}";

        [SitecoreField(FieldName = ResortNameId)]
        public virtual string ResortName { get; set; }

        [SitecoreField(FieldName = ResortIdId)]
        public virtual int ResortId { get; set; }

        [SitecoreField(FieldName = AccuWeatherCityCodeId)]
        public virtual string AccuWeatherCityCode { get; set; }

        [SitecoreField(FieldName = IsActiveId)]
        public virtual bool IsActive { get; set; }

        [SitecoreField(FieldName = ResortSummaryId)]
        public virtual string ResortSummary { get; set; }

        [SitecoreField(FieldName = PhoneNumberId)]
        public virtual string PhoneNumber { get; set; }

        [SitecoreField(FieldName = ResortImagesId)]
        public virtual Image ResortImages { get; set; }

        [SitecoreField(FieldName = ReviewIdId)]
        public virtual string ReviewId { get; set; }

        [SitecoreField(FieldName = FullWeekResortId)]
        public virtual bool FullWeekResort { get; set; }

        [SitecoreField(FieldName = ReservationPhoneNumberId)]
        public virtual string ReservationPhoneNumber { get; set; }

        [SitecoreField(FieldName = ShowGatewayWidgetId)]
        public virtual bool ShowGatewayWidget { get; set; }

        [SitecoreField(FieldName = TaxRateId)]
        public virtual double TaxRate { get; set; }

        [SitecoreField(FieldName = AllowSamplerSummerBookingId)]
        public virtual bool AllowSamplerSummerBooking { get; set; }

        [SitecoreField(FieldName = IsFeaturedId)]
        public virtual bool IsFeatured { get; set; }

        [SitecoreField(FieldName = ResortTypeId)]
        public virtual string ResortType { get; set; }

        [SitecoreField(FieldName = TSWSiteNumberId)]
        public virtual string TSWSiteNumber { get; set; }

        [SitecoreField(FieldName = DatabaseIdId)]
        public virtual string DatabaseId { get; set; }

        //Points
        [SitecoreField(FieldName = AllowPointsBookingId)]
        public virtual bool AllowPointsBooking { get; set; }

        [SitecoreField(FieldName = MinimumNightStayPointsId)]
        public virtual int MinimumNightStayPoints { get; set; }

        [SitecoreField(FieldName = MaximumNightStayPointsId)]
        public virtual int MaximumNightStayPoints { get; set; }

        [SitecoreField(FieldName = AdvanceSearchWindowPointsId)]
        public virtual int AdvanceSearchWindowPoints { get; set; }

        [SitecoreField(FieldName = MinimumNightChargePointsId)]
        public virtual string MinimumNightChargePoints { get; set; }

        [SitecoreField(FieldName = RestrictionsPointsId)]
        public virtual string RestrictionsPoints { get; set; }

        [SitecoreField(FieldName = TwoNightChargeForOneNightStayPTSId)]
        public virtual bool TwoNightChargeForOneNightStayPTS { get; set; }
      

        //Bonus Time
        [SitecoreField(FieldName = AllowBonusTimeBookingId)]
        public virtual bool AllowBonusTimeBooking { get; set; }

        [SitecoreField(FieldName = MinimumNightStayBonusTimeId)]
        public virtual int MinimumNightStayBonusTime { get; set; }

        [SitecoreField(FieldName = MaximumNightStayBonusTimeId)]
        public virtual int MaximumNightStayBonusTime { get; set; }

        [SitecoreField(FieldName = AdvanceSearchWindowBonusTimeId)]
        public virtual int AdvanceSearchWindowBonusTime { get; set; }

        [SitecoreField(FieldName = MinimumNightChargeBonusTimeId)]
        public virtual string MinimumNightChargeBonusTime { get; set; }

        [SitecoreField(FieldName = RestrictionsBonusTimeId)]
        public virtual string RestrictionsBonusTime { get; set; }

        [SitecoreField(FieldName = TwoNightChargeForOneNightStayBTId)]
        public virtual bool TwoNightChargeForOneNightStayBT { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Droplink, FieldId = MainResortImageId, Setting = SitecoreFieldSettings.InferType)]
        public virtual ResortImage MainResortImage { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Droplink, FieldId = HomePageBackgroundImageId, Setting = SitecoreFieldSettings.InferType)]
        public virtual ResortImage HomePageBackgroundImage { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Droplink, FieldId = ClubAffiliationId, Setting = SitecoreFieldSettings.InferType)]
        public virtual ClubAffiliation ClubAffiliation { get; set; }


        [SitecoreField(FieldType = SitecoreFieldType.Checklist, FieldId = ExperiencesId, Setting = SitecoreFieldSettings.InferType)]
        public virtual IEnumerable<Experience> Experiences { get; set; }

        //Visibility
        [SitecoreField(FieldName = ShowInDestinationsId)]
        public virtual bool ShowInDestinations { get; set; }

        //TODO address Object
        public const string AddressLine1Id = "{70C35537-554F-46D0-9348-ED1F5867CABC}";
        public const string AddressLine2Id = "{472D094C-B2AA-461B-9165-A8841DDD59DE}";
        public const string ZipCodeId = "{25AE128C-BD25-401A-95F0-5A1B35462D61}";
        public const string LatitudeId = "{9C7FE197-271C-4108-BA04-4301673EA080}";
        public const string LongitudeId = "{6BB1146D-2A34-4F2D-BAC3-B4031BB945D5}";
        public const string StateId = "{C8EACDB8-9410-46C0-9C7C-0D54DD81531F}";
        public const string CityId = "{CFC8C6E3-9FF3-4A08-A32E-3950D51BC305}";
        public const string ResortRegionId = "{CB7FC755-D3A3-48C8-B5E8-341316D52A13}";

        [SitecoreField(FieldName = AddressLine1Id)]
        public virtual string AddressLine1 { get; set; }

        [SitecoreField(FieldName = AddressLine2Id)]
        public virtual string AddressLine2 { get; set; }

        [SitecoreField(FieldName = ZipCodeId)]
        public virtual string ZipCode { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.Droplink, FieldId = ResortRegionId, Setting = SitecoreFieldSettings.InferType)]
        public virtual Lookup ResortRegion { get; set; }

        [SitecoreField(FieldName = LatitudeId)]
        public virtual string Latitude { get; set; }


        [SitecoreField(FieldName = LongitudeId)]
        public virtual string Longitude { get; set; }

        [SitecoreField(FieldName = CityId)]
        public virtual string City { get; set; }

        [SitecoreField(FieldType = SitecoreFieldType.DropTree, FieldId = StateId, Setting = SitecoreFieldSettings.InferType)]
        public virtual State State { get; set; }

        [SitecoreQuery("./Projects/*[@@templatename='Project Content']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<Project> Projects { get; set; }

        [SitecoreQuery("./Important Notes/*[@@templatename='Important Note']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<ImportantNote> ImportantNoteList { get; set; }

        public GeoCoordinate resortGeoCoordinate
        {
            get
            {
                Double lat = string.IsNullOrEmpty(Latitude) ? 0 : Double.Parse(Latitude);
                Double longitude = string.IsNullOrEmpty(Longitude) ? 0 : Double.Parse(Longitude);

                return new GeoCoordinate(lat, longitude);
            }
        }

        public int Distance { get; set; }


        /// <summary>
        /// Builds the address of the resort
        /// </summary>
        /// <returns></returns>
        public string BuildPostalAddress()
        {
            StringBuilder result = new StringBuilder();
            result.Append(!string.IsNullOrEmpty(this.AddressLine1) ? this.AddressLine1 : "");
            result.AppendFormat(" {0}", !string.IsNullOrEmpty(this.AddressLine2) ? this.AddressLine1 : "");
            result.AppendFormat(" {0}", !string.IsNullOrEmpty(this.City) ? this.City : "");
            result.AppendFormat(" {0}", this.State!=null ? this.State.Code : "");
            result.AppendFormat(" {0}", this.State != null ? this.State.Country : "");
            result.AppendFormat(" {0}", !string.IsNullOrEmpty(this.ZipCode) ? this.ZipCode : "");
            return result.ToString();
        }


    }
}