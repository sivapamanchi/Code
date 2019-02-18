using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BGModern.Classes;
using BGModern.Mappers;
using BGModern.Models;
using Umbraco.Web.Mvc;
using BGModern.Classes.Utilities;

namespace BGModern.Controllers
{
    public class ExperienceFilterController : SurfaceController
    {
        // TODO: Remove the compound state codes from the StateAbbreviations dictionary, then replace this to a call to UmbracoDictionaryMapper.Map
        // with reverse: true as soon as we get the BMOD stage backoffice working again.
        private static Dictionary<string, string> stateAbbreviations = new Dictionary<string, string>
        {
            {"Aruba", "AW"}, {"Bahamas", "BS"}, {"Alabama", "AL"}, {"Alaska", "AK"}, {"Arizona", "AZ"}, {"Arkansas", "AR"}, {"California", "CA"},
            {"Colorado", "CO"}, {"Connecticut", "CT"}, {"Delaware", "DE"}, {"Florida", "FL"}, {"Georgia", "GA"}, {"Hawaii", "HI"}, {"Idaho", "ID"},
            {"Illinois", "IL"}, {"Indiana", "IN"}, {"Iowa", "IA"}, {"Kansas", "KS"}, {"Kentucky", "KY"}, {"Louisiana", "LA"}, {"Maine", "ME"},
            {"Maryland", "MD"}, {"Massachusetts", "MA"}, {"Michigan", "MI"}, {"Minnesota", "MN"}, {"Mississippi", "MS"}, {"Missouri", "MO"},
            {"Montana", "MT"}, {"Nebraska", "NE"}, {"Nevada", "NV"}, {"New Hampshire", "NH"}, {"New Jersey", "NJ"}, {"New Mexico", "NM"},
            {"New York", "NY"}, {"North Carolina", "NC"}, {"North Dakota", "ND"}, {"Ohio", "OH"}, {"Oklahoma", "OK"}, {"Oregon", "OR"},
            {"Pennsylvania", "PA"}, {"Rhode Island", "RI"}, {"South Carolina", "SC"}, {"South Dakota", "SD"}, {"Tennessee", "TN"},{"Texas", "TX"},
            {"Utah", "UT"}, {"Vermont", "VT"}, {"Virginia", "VA"}, {"Washington", "WA"}, {"West Virginia", "WV"}, {"Wisconsin", "WI"}, {"Wyoming", "WY"}
        };

        // GET: ExperienceFilter
        public ActionResult Index()
        {
            throw new NotImplementedException("Index method is not supported");
        }

        public ActionResult GetPartialView()
        {
            var model = new ExperienceFilterModel();
            HydrateModel(model);
            return PartialView("ExperienceFilter", model);
        }

        private void HydrateModel(ExperienceFilterModel model)
        {
            var owner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            var isSamplerOwner = owner.User[0].isSampler;
            var homeProject = owner.User[0].HomeProject;

            model.Destinations = DestinationsFromAS400.GetAvailableDestinations(isSamplerOwner, homeProject, uspSelectResortCityState: true);
            foreach (var destination in model.Destinations)
            {
                destination.State = stateAbbreviations[destination.State];
            }

            model.Experiences = new List<Experience>();

            int experienceDataTypeId = -1;
            string experienceDataTypeIdString = System.Configuration.ConfigurationManager.AppSettings["experienceDataTypeId"];
            if (!string.IsNullOrWhiteSpace(experienceDataTypeIdString) && Int32.TryParse(experienceDataTypeIdString, out experienceDataTypeId))
            {
                DropdownListPrevalueMapper.GetDataTypePrevalues(experienceDataTypeId).ForEach(x => model.Experiences.Add(new Experience
                    {
                        ID = x.ID,
                        Description = x.Value
                    }));
            }
        }
    }
}