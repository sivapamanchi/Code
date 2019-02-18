using BGModern.Classes;
using BGModern.Mappers;
using BGModern.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Umbraco.Core.Models;

namespace BGModern.Controllers
{
    public class ResortQuickSelectController : Umbraco.Web.Mvc.SurfaceController
    {
        // GET: ReportQuickSelect
        public ActionResult ResortQuickSelect()
        {
            ResortQuickSelectModel model = new ResortQuickSelectModel();

            model.Destinations = new List<Destination>();

            // loop through the children of Resorts content item and populate the destination member of the model
            if (Umbraco.TypedContent(1088) != null)
            {
                foreach (IPublishedContent child in Umbraco.TypedContent(1088).Children)
                {
                    ResortModel resort = ResortMapper.Map(child);

                    if (resort != null)
                    {
                        model.Destinations.Add(new Destination(resort.ID, resort.Name, resort.Url));
                    }
                }
            } List<SitecoreResorts> Resorts = new List<SitecoreResorts>();

            // Add Resorts to the list.
            Resorts.Add(new SitecoreResorts() { ResortName = "Eilan Hotel & Spa ", ResortId = 138, ResortUrl = "/our-resorts/Eilan-Hotel---Spa" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Blue Ridge Village ", ResortId = 136, ResortUrl = "/our-resorts/Blue-Ridge-Village" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Blue Water Resort at Cable Beach ", ResortId = 113, ResortUrl = "/our-resorts/Blue-Water-Resort-at-Cable-Beach" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Bluegreen at Atlantic Palace", ResortId = 102, ResortUrl = "/our-resorts/Bluegreen-at-Atlantic-Palace" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Bluegreen at TradeWinds", ResortId = 126, ResortUrl = "/our-resorts/Bluegreen-at-TradeWinds" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Bluegreen Club 36 ", ResortId = 95, ResortUrl = "/our-resorts/Bluegreen-Club-36" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Bluegreen Club La Pension", ResortId = 104, ResortUrl = "/our-resorts/Bluegreen-Club-La-Pension" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Bluegreen Odyssey Dells", ResortId = 97, ResortUrl = "/our-resorts/Bluegreen-Odyssey-Dells" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Bluegreen Wilderness Club at Big Cedar ", ResortId = 2, ResortUrl = "/our-resorts/Bluegreen-Wilderness-Club-at-Big-Cedar" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Carolina Grande", ResortId = 71, ResortUrl = "/our-resorts/Carolina-Grande" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Casa del Mar Beach Resort ", ResortId = 26, ResortUrl = "/our-resorts/Casa-del-Mar-Beach-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Christmas Mountain Village ", ResortId = 3, ResortUrl = "/our-resorts/Christmas-Mountain-Village" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Cibola Vista Resort and Spa", ResortId = 115, ResortUrl = "/our-resorts/Cibola-Vista-Resort-and-Spa" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Club Lodges at Trillium", ResortId = 134, ResortUrl = "/our-resorts/Club-Lodges-at-Trillium" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Daytona SeaBreeze", ResortId = 63, ResortUrl = "/our-resorts/Daytona-SeaBreeze" });
            Resorts.Add(new SitecoreResorts() { ResortName = "King 583", ResortId = 135, ResortUrl = "/our-resorts/King-583" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Dolphin Beach Club", ResortId = 18, ResortUrl = "/our-resorts/Dolphin-Beach-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Fantasy Island Resort II", ResortId = 21, ResortUrl = "/our-resorts/Fantasy-Island-Resort-II" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Foxrun Townhouses", ResortId = 65, ResortUrl = "/our-resorts/Foxrun-Townhouses" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Golf Club Villas at Big Canoe ", ResortId = 108, ResortUrl = "/our-resorts/Golf-Club-Villas-at-Big-Canoe" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Grande Villas at World Golf Village", ResortId = 9, ResortUrl = "/our-resorts/Grande-Villas-at-World-Golf-Village" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Gulfstream Manor", ResortId = 19, ResortUrl = "/our-resorts/Gulfstream-Manor" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Harbour Lights", ResortId = 8, ResortUrl = "/our-resorts/Harbour-Lights" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Horizons at 77th", ResortId = 129, ResortUrl = "/our-resorts/Horizons-at-77th" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Hotel Blake", ResortId = 131, ResortUrl = "/our-resorts/Hotel-Blake" });
            Resorts.Add(new SitecoreResorts() { ResortName = "La Cabana Beach Resort & Casino", ResortId = 46, ResortUrl = "/our-resorts/La-Cabana-Beach-Resort--Casino" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Lake Condominiums at Big Sky", ResortId = 42, ResortUrl = "/our-resorts/Lake-Condominiums-at-Big-Sky" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Landmark Holiday Beach Resort", ResortId = 31, ResortUrl = "/our-resorts/Landmark-Holiday-Beach-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Laurel Crest", ResortId = 5, ResortUrl = "/our-resorts/Laurel-Crest" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Mariner's Boathouse And Beach Resort", ResortId = 28, ResortUrl = "/our-resorts/Mariners-Boathouse-And-Beach-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Mountain Run at Boyne", ResortId = 13, ResortUrl = "/our-resorts/Mountain-Run-at-Boyne" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Mountain Loft", ResortId = 6, ResortUrl = "/our-resorts/MountainLoft" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Ocean Towers Beach Club", ResortId = 30, ResortUrl = "/our-resorts/Ocean-Towers-Beach-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Orlando's Sunshine Resort I & II", ResortId = 12, ResortUrl = "/our-resorts/Orlandos-Sunshine-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Outrigger Beach Club", ResortId = 24, ResortUrl = "/our-resorts/Outrigger-Beach-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Panama City Resort & Club", ResortId = 61, ResortUrl = "/our-resorts/Panama-City-Resort-And-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Paradise Isle Resort", ResortId = 44, ResortUrl = "/our-resorts/Paradise-Isle-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Paradise Point ", ResortId = 121, ResortUrl = "/our-resorts/Paradise-Point" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Parkside Williamsburg Resort", ResortId = 114, ResortUrl = "/our-resorts/Parkside-Williamsburg-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Patrick Henry Square", ResortId = 96, ResortUrl = "/our-resorts/Patrick-Henry-Square" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Petit Crest Villas at Big Canoe", ResortId = 33, ResortUrl = "/our-resorts/Petit-Crest-Villas-at-Big-Canoe" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Players Club", ResortId = 20, ResortUrl = "/our-resorts/Players-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Pono Kai Resort", ResortId = 16, ResortUrl = "/our-resorts/Pono-Kai-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Resort Sixty-Six ", ResortId = 25, ResortUrl = "/our-resorts/Resort-Sixty-Six" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Sandcastle Village II", ResortId = 69, ResortUrl = "/our-resorts/Sandcastle-Village-II" });
            Resorts.Add(new SitecoreResorts() { ResortName = "SeaGlass Tower", ResortId = 70, ResortUrl = "/our-resorts/SeaGlass-Tower" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Shenandoah Crossing", ResortId = 4, ResortUrl = "/our-resorts/Shenandoah-Crossing" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Shore Crest Vacation Villas I & II", ResortId = 7, ResortUrl = "/our-resorts/Shore-Crest-Vacation-Villas-I--II" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Shoreline Towers", ResortId = 43, ResortUrl = "/our-resorts/Shoreline-Towers" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Solara Surfside ", ResortId = 1, ResortUrl = "/our-resorts/Solara-Surfside" });
            Resorts.Add(new SitecoreResorts() { ResortName = "South Mountain Resort", ResortId = 116, ResortUrl = "/our-resorts/South-Mountain-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Surfrider Beach Club", ResortId = 68, ResortUrl = "/our-resorts/Surfrider-Beach-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Breakers Resort- Bluegreen Resorts", ResortId = 118, ResortUrl = "/our-resorts/The-Breakers-Resort--Bluegreen-Resorts" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Cliffs at Long Creek", ResortId = 101, ResortUrl = "/our-resorts/The-Cliffs-at-Long-Creek" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Club at Big Bear Village", ResortId = 125, ResortUrl = "/our-resorts/The-Club-at-Big-Bear-Village" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Falls Village ", ResortId = 10, ResortUrl = "/our-resorts/The-Falls-Village" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Fountains", ResortId = 14, ResortUrl = "/our-resorts/The-Fountains" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Hammocks at Marathon", ResortId = 15, ResortUrl = "/our-resorts/The-Hammocks-at-Marathon" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Innsbruck Aspen", ResortId = 128, ResortUrl = "/our-resorts/The-Innsbruck-Aspen" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Lodge Alley Inn", ResortId = 11, ResortUrl = "/our-resorts/The-Lodge-Alley-Inn" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Manhattan Club", ResortId = 120, ResortUrl = "/our-resorts/The-Manhattan-Club" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Soundings Seaside Resort ", ResortId = 109, ResortUrl = "/our-resorts/The-Soundings-Seaside-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Studio Homes at Ellis Square", ResortId = 124, ResortUrl = "/our-resorts/The-Studio-Homes-at-Ellis-Square" });
            Resorts.Add(new SitecoreResorts() { ResortName = "The Suites at Hershey ", ResortId = 17, ResortUrl = "/our-resorts/The-Suites-at-Hershey" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Tropical Sands Resort", ResortId = 27, ResortUrl = "/our-resorts/Tropical-Sands-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Via Roma Beach Resort ", ResortId = 22, ResortUrl = "/our-resorts/Via-Roma-Beach-Resort" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Waterwood Townhouses", ResortId = 66, ResortUrl = "/our-resorts/Waterwood-Townhouses" });
            Resorts.Add(new SitecoreResorts() { ResortName = "Windward Passage Resort ", ResortId = 41, ResortUrl = "/our-resorts/Windward-Passage-Resort" });

            // Write out the Resorts in the list. This will call the overridden ToString method
            // in the SitecoreResorts class.
            string BGOResort = Request.CurrentExecutionFilePath.ToLower();
            string splResorts;
            splResorts = BGOResort;
            bool? spl = false;

            string del = string.Empty;
            if (BGOResort.Contains("/"))
            {
                del = "/";
            }
            string[] splitUrl = BGOResort.Split(del.ToCharArray());
            string Regresorts = "/" + splitUrl[2].ToString() + "/" + splitUrl[3].ToString();

            if (BGOResort == "/BGModern/our-resorts")
            {
                Response.Redirect(ConfigurationManager.AppSettings["OurResortsToSiteCore"], true);
            }
            else
            {

                foreach (SitecoreResorts resort in Resorts)
                {
                    if ((splResorts.Contains("Eilan-Hotel-Spa")) || (splResorts.Contains("eilan-hotel-spa")))
                    {
                        splResorts = "/our-resorts/Eilan-Hotel---Spa";
                        spl = true;
                    }
                    if ((splResorts.Contains("La-Cabana-Beach-Resort")) || (splResorts.Contains("la-cabana-beach-resort")) )
                    {
                        splResorts = "/our-resorts/La-Cabana-Beach-Resort--Casino";
                        spl = true;
                    }
                    if ((splResorts.Contains("mountain-loft")) || (splResorts.Contains("Mountain-Loft")))
                    {
                        splResorts = "/our-resorts/MountainLoft";
                        spl = true;
                    }
                    if ((splResorts.Contains("panama-city-resort-club")) || (splResorts.Contains("Panama-City-Resort-Club")))
                    {
                        splResorts = "/our-resorts/Panama-City-Resort-And-Club";
                        spl = true;
                    }
                    if ((splResorts.Contains("orlandos-sunshine-resort")) || (splResorts.Contains("Orlandos-Sunshine-Resort")))
                    {
                        splResorts = "/our-resorts/Orlandos-Sunshine-Resort";
                        spl = true;
                    }
                    if ((splResorts.Contains("Shore-Crest-Vacation-Villas")) || (splResorts.Contains("shore-crest-vacation-villas")))
                    {
                        splResorts = "/our-resorts/Shore-Crest-Vacation-Villas-I--II";
                        spl = true;
                    }
                    if ((splResorts.Contains("The-Hotel-Blake")) || (splResorts.Contains("the-hotel-blake")))
                    {
                        splResorts = "/our-resorts/Hotel-Blake";
                        spl = true;
                    }
                    if ((splResorts.Contains("The-Breakers-Resort-Bluegreen-Resorts")) || (splResorts.Contains("the-breakers-resort-bluegreen-resorts")))
                    {
                        splResorts = "/our-resorts/The-Breakers-Resort--Bluegreen-Resorts";
                        spl = true;
                    }
                    splResorts = splResorts.ToLower();
                    if (spl == true)
                    {
                        if (splResorts == resort.ResortUrl.ToLower())
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["SitecoreResortUrl"] + resort.ResortUrl.ToLower(), true);
                            break;
                        }
                    }
                    else
                    {
                        if (Regresorts == resort.ResortUrl.ToLower())
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["SitecoreResortUrl"] + resort.ResortUrl.ToLower(), true);
                            break;
                        }
                    }


                }
            }

            return PartialView(model);
        }
    }
}