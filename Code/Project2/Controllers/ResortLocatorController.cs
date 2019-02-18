using BGModern.Classes;
using BGModern.Models;
using BGModern.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Newtonsoft.Json;
using System.Text;
using System.Data;

namespace BGModern.Controllers
{
    public class ResortLocatorController : Umbraco.Web.Mvc.SurfaceController
    {
        // GET: ResortLocator
        public ActionResult Index()
        {
            throw new NotImplementedException("Index is not implemented for ResortLocatorController");
        }

        [HttpGet]
        public ActionResult GetPartialView()
        {
            ResortLocatorModel model = new ResortLocatorModel();
            HydrateModel(model);

            return PartialView("ResortLocator", model);
        }

        private void HydrateModel(ResortLocatorModel model)
        {
            model.Cities = new List<string>();
            model.Destinations = new Dictionary<string, List<Destination>>();

            var citiesAndStates = new List<Tuple<string, string>>();
            var destinations = new List<Destination>();
            var conn = new BGO.clsDBConnectivity();

            conn.dbCmnd.CommandText = "uspSelectDestination";
            conn.dbCmnd.CommandType = CommandType.StoredProcedure;
            conn.dbCmnd.Parameters.Clear();
            var reader = conn.dbCmnd.ExecuteReader();
            while (reader.Read())
            {
                string city = reader["City"].ToString().Trim();
                string state = reader["StateCode"].ToString().Trim();
                string cityState = GetCityAndStateString(city, state).ToUpper();
                if (!model.Destinations.ContainsKey(cityState))
                {
                    citiesAndStates.Add(Tuple.Create(city, state));
                    model.Destinations[cityState] = new List<Destination>();
                }
            }

            model.Cities = citiesAndStates
                    .OrderBy(x => x.Item2)
                    .ThenBy(x => x.Item1)
                    .Select(x => x.Item1 + ", " + x.Item2)
                    .ToList();

            reader.Close();
            conn.Close();

            IPublishedContent ourResorts = null;
            int ourResortsContentId;
            string ourResortsContentIdString = System.Configuration.ConfigurationManager.AppSettings["ourResortsContentId"];
            if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
            {
                ourResorts = Umbraco.TypedContent(ourResortsContentId);
            }

            if (ourResorts != null)
            {
                var resortList = new List<ResortModel>();
                foreach (IPublishedContent child in ourResorts.Children)
                {
                    var resort = ResortMapper.Map(child);
                    resort.Name = resort.Name.Trim();
                    resortList.Add(resort);
                }

                foreach (var resort in resortList)
                {
                    string city = GetCityAndStateString(resort.City.Trim(), resort.State.Trim()).ToUpper();
                    string resortName;

                    if (string.IsNullOrWhiteSpace(resort.Name))
                    {
                        resortName = string.Format("Resort #{0}", resort.ResortPageID);
                    }
                    else
                    {
                        resortName = resort.Name;
                    }

                    if (resort != null)
                    {
                        if (model.Destinations.ContainsKey(city.ToUpper()))
                            model.Destinations[city.ToUpper()].Add(new Destination(resort.ID, resortName, resort.Url));
                    }
                }
            }

            RemoveEmptyDestinations(model.Cities, model.Destinations);
            
            model.DestinationMap = BuildDestinationMap(model);

            // //loop through the children of Resorts content item and populate the destination member of the model
            //if (ourResorts != null)
            //{
            //    var resortList = new List<ResortModel>();
            //    foreach (IPublishedContent child in ourResorts.Children)
            //    {
            //        resortList.Add(ResortMapper.Map(child));
            //    }

            //    // Build list of cities, then sort
            //    var citiesAndStates2 = new List<Tuple<string, string>>();

            //    foreach (var resort in resortList)
            //    {
            //        string city = GetCityAndStateString(resort.City, resort.State).ToUpper();

            //        if (!model.Destinations.ContainsKey(city))
            //        {
            //            citiesAndStates.Add(Tuple.Create(resort.City, resort.State));
            //            model.Destinations[city] = new List<Destination>();
            //        }
            //    }

            //    model.Cities = citiesAndStates
            //        .OrderBy(x => x.Item2)
            //        .ThenBy(x => x.Item1)
            //        .Select(x => x.Item1 + ", " + x.Item2)
            //        .ToList();

            //    foreach (var resort in resortList)
            //    {
            //        string city = GetCityAndStateString(resort.City, resort.State).ToUpper();
            //        string resortName;

            //        if (string.IsNullOrWhiteSpace(resort.Name))
            //        {
            //            resortName = string.Format("Resort #{0}", resort.ResortPageID);
            //        }
            //        else
            //        {
            //            resortName = resort.Name;
            //        }

            //        if (resort != null)
            //        {
            //            model.Destinations[city].Add(new Destination(resort.ID, resortName, resort.Url));
            //        }
            //    }
            //}

            ////var x = JsonConvert.SerializeObject(model.Destinations);

            //model.DestinationMap = BuildDestinationMap(model);
        }

        private void RemoveEmptyDestinations(List<string> cities, Dictionary<string, List<Destination>> destinations)
        {
            var citiesClone = cities.ToList();

            foreach (var city in citiesClone)
            {
                if (destinations[city.ToUpper()].Count == 0)
                {
                    destinations.Remove(city.ToUpper());
                    cities.Remove(city);
                }
            }
        }

        private string BuildDestinationMap(ResortLocatorModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            for (int i = 0; i < model.Cities.Count; i++)
            {
                string city = model.Cities[i].ToUpper();
                sb.Append(JsonConvert.SerializeObject(model.Destinations[city]));

                if (i < model.Cities.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("]");
            return sb.ToString();
        }

        private string GetCityAndStateString(string city, string state)
        {
            if (string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(state))
            {
                return "(no location)";
            }
            else if (string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(state))
            {
                return state;
            }
            else if (!string.IsNullOrWhiteSpace(city) && string.IsNullOrWhiteSpace(state))
            {
                return city;
            }
            else
            {
                return city + ", " + state;
            }
        }
    }
}