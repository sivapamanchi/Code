using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace BGModern.Models
{
    public class ResortModel : MasterModel
    {
        #region Properties
        public int ID { get; set; }
        public int LegacyID { get; set; }
        public String Name { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String AdvisorBadge { get; set; }
        public String ClubImage { get; set; }
        public String Description { get; set; }
        public String Thumbnail { get; set; }
        public String Summary { get; set; }
        public int ResortPageID { get; set; }
        public String Url { get; set; }
        public String ResortDetail { get; set; }
        public String Phone { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public CaptionedPhotoListModel ResortPhotos { get; set; }
        public AmenityListModel AmenitiesLists { get; set; }
        public CaptionedPhotoListModel ResortFloorPlans { get; set; }
        public ActivityListModel ResortActivities { get; set; }
        public String AccuWeatherCityCode { get; set; }
        public HistoricalWeatherListModel HistoricalWeatherData { get; set; }
        public ResortNotesModel ResortNotesList { get; set; }
        public AreaInformationModel AreaInformation { get; set; }
        public ResortMapModel ResortMap { get; set; }
        public ResortPointsTableModel ResortPointsTable { get; set; }
        public FoodAndBeverageModel FoodAndBeverage { get; set; }
        public AssociationOwnersModel AssociationOwners { get; set; }
        public bool IncludeBorderLine { get; set; }
        public bool IncludePhone { get; set; }
        public bool VacationOrSamplerOwner { get; set; }
        public bool VacationClubOwner { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsOasisLakesCondominiumAssociationMember { get; set; }
        #endregion

        #region URLs to all the Resort's child pages; used for the resort master page's left menu
        public String URLResortDescription { get; set; }
        public String URLResortPhotos { get; set; }
        public String URLResortAmenities { get; set; }
        public String URLResortFloorPlans { get; set; }
        public String URLResortActivities { get; set; }
        public String URLResortNotes { get; set; }
        public String URLResortMapAndDirection { get; set; }
        public String URLResortAreaInformation { get; set; }
        public String URLResortWeather { get; set; }
        public String URLResortFoodAndBeverage { get; set; }
        public String URLResortResortMap { get; set; }
        public String URLResortPointsTable { get; set; }
        public String URLAssociationOwners { get; set; }
        #endregion

        public ResortModel()
        {

        }

        public ResortModel(IPublishedContent page)
            : base(page)
        {

        }
    }
}