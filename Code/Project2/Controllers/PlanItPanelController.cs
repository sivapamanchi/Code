using BGO;
using BGO.availabilityservice;
using BGO.bluegreenonline;
using BGO.Bluegreenonline;
using BGO.OwnerWS;
using BGO.ResortsService;
using BGModern.Classes;
using BGModern.Classes.Mappers;
using BGModern.Classes.Utilities;
using BGModern.Mappers;
using BGModern.Models;
using IBM.Data.DB2.iSeries;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Web.Script.Serialization;
using BGModern.HtmlExtensions;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ReservationLibrary;

namespace BGModern.Controllers
{
    // TODO: [Authorize]
    public class PlanItPanelController : Umbraco.Web.Mvc.SurfaceController
    {
        private readonly IPublishedContent ourResorts;

        private readonly Dictionary<string, string> stateNames = UmbracoDictionaryMapper.Map("StateAbbreviations");

        private readonly Dictionary<string, string> unitTypes = UmbracoDictionaryMapper.Map("UnitTypes", reverse: true);

        private readonly Dictionary<string, int> unitTypeSortOrder = UmbracoDictionaryMapper.Map<int>("UnitTypeSortOrder", x => Convert.ToInt32(x));

        private static readonly Dictionary<String, Season> seasonNames = new Dictionary<string, Season>()
        {
            {"WHITE", Season.White},
            {"BLUE", Season.Blue},
            {"RED", Season.Red},
            {"HIGH RED", Season.HighRed},
            {"ULTRA RED", Season.HighRed},
            {"LEAF", Season.HighRed},
            {"SPECIAL", Season.Special}
        };

        // TODO: Forgive me father for I have sinned
        private bool isFullWeekResort = false;
        private int minAdvSearchWindow = 0;
        private DateTime btMaxSearchDate;
        private DataTable unitAvailabilityTable;
        private DataTable calendarDataTable;
        private const string PrivateEncryptionKey = "Fs4HG6px";

        public PlanItPanelController()
        {
            ourResorts = null;

            int ourResortsContentId;
            string ourResortsContentIdString = ConfigurationManager.AppSettings["ourResortsContentId"];
            if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
            {
                ourResorts = Umbraco.TypedContent(ourResortsContentId);
            }
        }


        #region "Action methods"

        //
        // GET: /PlanItPanel/

        [HttpGet]
        public ActionResult PlanItPanel()
        {
            throw new NotImplementedException("Index is not implemented for PlanItPanelController");
        }

        [HttpGet]
        public ActionResult GetPartialView()
        {
            PlanItPanelModel model = new PlanItPanelModel();
            try
            {
                HydrateModel(model);
            }
            catch (Exception ex)
            {

            }

            return PartialView("PlanItPanel", model);
        }

        [HttpPost]
        public ContentResult Search(PlanItSearchModel model)
        {
            model.ValidationSummary = new ValidationSummary();
            model.TemporaryValidationSummary = new ValidationSummary();

            try
            {
                if (model.SearchType == SearchType.Destination)
                {
                    model = DestinationSearch(model);
                }
                else
                {
                    model = DateSearch(model);
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.ToString() + "<br/>\n" + ex.StackTrace.Replace("\n", "<br/>\n").Replace("\r", "");
                NotifyError(model, ex, exceptionMessage);
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return new ContentResult
            {
                Content = serializer.Serialize(model),
                ContentType = "application/json"
            };
        }

        public PlanItSearchModel DestinationSearch(PlanItSearchModel model)
        {
            VerifyResortIds(model);

            Session["SelectedDates"] = null;
            unitAvailabilityTable = LoadCalendarData(model);
            Session["inventory"] = unitAvailabilityTable;
            calendarDataTable = unitAvailabilityTable.Copy();
            FilterAllowedResorts(model, unitAvailabilityTable);

            model.CheckInDates = GetCheckInOutDates(unitAvailabilityTable).Select(x => x.Item1.ToShortDateString()).ToList();
            model.ResortUnitTypesByDate = GetResortUnitTypesByDate(unitAvailabilityTable).Select(x => new AvailableUnitTypesByDateAndResortModel { CheckInDate = x.Item1.ToShortDateString(), ResortID = x.Item2.ToString(), UnitCodeList = x.Item3 }).ToList();

            model.MaxSearchDate = btMaxSearchDate.ToShortDateString();

            model.FilterResorts = unitAvailabilityTable.AsEnumerable()
                .GroupBy(row => new
                {
                    ResortName = row["ResortName"].ToString(),
                    ProjectNumber = Convert.ToInt32(row["ResortID"])
                })
                .OrderBy(group => group.Key.ProjectNumber)
                .Select(group => new ResortFilterModel
                {
                    HiddenProjStatus = false,
                    Included = true,
                    Name = group.Key.ResortName,
                    ProjectNumber = group.Key.ProjectNumber,
                    UnitTypes = group.ToList()
                        .Select(unitRow => new ResortUnitModel
                        {
                            Included = true,
                            UnitTypeCode = unitRow["BluegreenUnitCode"].ToString(),
                            UnitTypeName = UnitTypeName(unitRow["BluegreenUnitCode"].ToString()),
                            SortOrder = UnitTypeSortOrder(unitRow["BluegreenUnitCode"].ToString()),
                        })
                        .Distinct(new ResortUnitModelComparer())
                        .OrderBy(y => y.SortOrder)
                        .ToList()
                })
                .ToList();

            return model;
        }

        public JsonResult SelectDate(PlanItSearchModel model)
        {
            try
            {
                // A ResortCriteria for each resort ID
                List<ResortCriteria> resortsInfo;

                if (model.DestinationResortIDs != null)
                {
                    resortsInfo = model.DestinationResortIDs
                        .Select(x => new ResortCriteria
                        {
                            ResortId = x.ToString(),
                            WebUnitTypes = "ANY"
                        })
                        .ToList();
                }
                else
                {
                    resortsInfo = new List<ResortCriteria>();
                    NotifyError(model, null, "Model.DestinationResortIDs was null");
                }

                LoadUnitAvailability(model, resortsInfo, DateTime.Parse(model.CheckInDate), DateTime.Parse(model.CheckInDate).AddDays(model.LengthOfStay));
                CheckPaymentStatus(model);
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.ToString() + "<br/>\n" + ex.StackTrace.Replace("\n", "<br/>\n").Replace("\r", "");
                NotifyError(model, ex, exceptionMessage);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SelectResort(PlanItSearchModel model)
        {
            try
            {
                var resortsInfo = new List<ResortCriteria>
                {
                    new ResortCriteria
                    {
                        ResortId = model.SelectedResortId.ToString(),
                        WebUnitTypes = "ANY"
                    }
                };

                LoadUnitAvailability(model, resortsInfo, DateTime.Parse(model.CheckInDate), DateTime.Parse(model.CheckOutDate));
                CheckPaymentStatus(model);
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.ToString() + "<br/>\n" + ex.StackTrace.Replace("\n", "<br/>\n").Replace("\r", "");
                NotifyError(model, ex, exceptionMessage);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private void CheckPaymentStatus(PlanItSearchModel model)
        {
            model.AllowBookIt = true;

            var bxgOwner = (BGO.OwnerWS.Owner)Session["BXGOwner"];
            if (bxgOwner.ReservationDuePaymentBalance > 0 && bxgOwner.InstallmentPlan[0].InstallmentStatus != "IP")
            {
                model.AllowBookIt = false;
                model.TemporaryValidationSummary.Add("Due to the current status of one or more of your accounts, we cannot allow online reservations at this time. Please call 800.456.CLUB(2582) for further assistance. <a onclick=\"newWin=window.open('" + CustomHtmlHelpers.GetParentSitePath(null) + "/owner/helpWinHours.aspx','newWin1','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=239,height=450,left=50,top=50'); return false;\" href=\"#\">Click here</a> for hours of operation.", fail: false);
            }
        }

        public PlanItSearchModel DateSearch(PlanItSearchModel model)
        {
            unitAvailabilityTable = GetCategories(model);
            Session["inventory"] = unitAvailabilityTable;
            FilterBTIneligibleUnits(model, unitAvailabilityTable);
            FilterAllowedResorts(model, unitAvailabilityTable);
            FilterUnitsOutsideSearchWindow(model, unitAvailabilityTable);

            // Build a ResortCriteria object for each distinct resort ID
            var resortsInfo = unitAvailabilityTable
                .AsEnumerable()
                .GroupBy(row => ((int)row["ResortId"]))
                .Select(group => new ResortCriteria()
                {
                    ResortId = group.Key.ToString(),
                    WebUnitTypes = "ANY"
                })
                .ToList();
            
            var checkInDate = DateTime.Parse(model.CheckInDate);
            var checkOutDate = DateTime.Parse(model.CheckOutDate);
            var stayDates = Tuple.Create(checkInDate, checkOutDate);
            model.LengthOfStay = (checkOutDate - checkInDate).Days;

            // Take the list of available units and
            //  * Group by state.
            //  * For each grouping, construct a ResortStateModel, with the name coming from the data row.
            //  * Then, for each ResortStateModel:
            //    * Order the list of units by city.
            //    * Create a ResortInfoModel from each unit, attaching properties from Umbraco.
            //    * Now we have a ResortInfoModel for each unit. A call to Distinct() returns a list of ResortInfoModel objects that are unique by resort ID.
            model.AvailableStates = unitAvailabilityTable.AsEnumerable()
                .GroupBy(row => row["ResortState"].ToString())
                .Select(row => new ResortStateModel()
                {
                    Name = row.Key,
                    AvailableResorts = row.ToList()
                        .OrderBy(resortRow => resortRow["ResortCity"].ToString())
                        .Select(resortRow => AttachUmbracoProperties(new ResortInfoModel
                        {
                            ID = resortRow["ResortID"].ToString(),
                        }, null, resortRow))
                        .Distinct(new ResortInfoModelComparer())
                        .ToList()
                })
                .ToList();

            AttachAvailableUnitTypes(model, unitAvailabilityTable);

            return model;
        }

        private void FilterAllowedResorts(PlanItSearchModel model, DataTable unitAvailabilityTable)
        {
            // Get resort IDs that allow bookings for our target reservation type
            HashSet<int> allowedDestinations = GetDestinationsSetByReservationType(model.ReservationType, x => x.GetPropertyValue<int>("DatabaseID"));;

            // Remove units at resorts that aren't allowed
            foreach (DataRow row in unitAvailabilityTable.Rows)
            {
                if (!allowedDestinations.Contains((int)row["ResortId"]))
                {
                    row.Delete();
                }
            }

            unitAvailabilityTable.AcceptChanges();
        }

        private void FilterUnitsOutsideSearchWindow(PlanItSearchModel model, DataTable unitAvailabilityTable)
        {
            // Get search window for each resort
            string searchWindowField = model.ReservationType == ReservationType.Points ? "SearchWindow" : "BtSearchWindow";
            Dictionary<int, int> resortSearchWindows = GetResortsByReservationType(model.ReservationType, x => x.GetPropertyValue<int>(searchWindowField));

            // Remove units where reservations are not allowed as early as the user wants
            var daysOut = (int)(DateTime.Parse(model.CheckInDate) - DateTime.Now.Date).TotalDays;
            System.Diagnostics.Debug.WriteLine(daysOut.ToString() + " days out");
            foreach (DataRow row in unitAvailabilityTable.Rows)
            {
                if (daysOut < resortSearchWindows[(int)row["ResortId"]]) {
                    System.Diagnostics.Debug.WriteLine(string.Format("Filtered out unit from resort {0}, search window = {1}", (int)row["ResortId"], resortSearchWindows[(int)row["ResortId"]]));
                    row.Delete();
                }
            }

            unitAvailabilityTable.AcceptChanges();
        }

        private void FilterBTIneligibleUnits(PlanItSearchModel model, DataTable unitAvailabilityTable)
        {
            var todayPlus48Days = DateTime.Now.AddDays(48);
            var checkInDate = DateTime.Parse(model.CheckInDate);

            foreach (DataRow row in unitAvailabilityTable.Rows)
            {
                if (model.ReservationType == ReservationType.Points && model.LengthOfStay == 1)
                {
                    if (row["ResortId"].ToString() != "11")
                    {
                        row.Delete();
                    }
                }
                else if (model.ReservationType == ReservationType.BonusTime)
                {
                    if (checkInDate >= todayPlus48Days && row["ResortId"].ToString() != "16" && row["ResortId"].ToString() != "46")
                    {
                        row.Delete();
                    }
                }
            }

            unitAvailabilityTable.AcceptChanges();
        }

        // For each resort, attaches a list of unit types available there. The same is done up to the state and the overall search results level.
        private void AttachAvailableUnitTypes(PlanItSearchModel model, DataTable unitAvailabilityTable)
        {
            var unitTypeComparer = new UnitTypeComparer();

            //model.AvailableStates = unitAvailabilityTable.AsEnumerable()
            //    .Select(row => row["ResortState"].ToString())
            //    .Distinct()
            //    .Select(x => new ResortStateModel
            //    {
            //        Name = x
            //    })
            //    .ToList();

            foreach (var state in model.AvailableStates)
            {
                //state.AvailableResorts = unitAvailabilityTable.AsEnumerable()
                //    .Where(row => row["ResortState"].ToString() == state.Name)
                //    .Select(row => new ResortInfoModel
            //    {
                //        ID = row["ResortId"].ToString()
                //    })
                //    .ToList();

                foreach (var resort in state.AvailableResorts)
                {
                    resort.AvailableUnitTypes = unitAvailabilityTable.AsEnumerable()
                        .Where(row => row["ResortId"].ToString() == resort.ID)
                        .Select(row => new Classes.UnitType
                        {
                            Code = row["BluegreenUnitCode"].ToString(),
                            Name = UnitTypeName(row["BluegreenUnitCode"].ToString()),
                            SortOrder = UnitTypeSortOrder(row["BluegreenUnitCode"].ToString())
                        })
                        .Distinct(unitTypeComparer)
                        .ToList();
                }

                state.AvailableUnitTypes = state.AvailableResorts
                    .SelectMany(x => x.AvailableUnitTypes)
                    .Distinct(unitTypeComparer)
                    .ToList();
            }

            model.AvailableUnitTypes = model.AvailableStates
                .SelectMany(x => x.AvailableUnitTypes)
                .Distinct(unitTypeComparer)
                .OrderBy(x => x.SortOrder)
                .ToList();
        }

        public JsonResult ReservationPointsValidation(string parameters)
        {
            Session["PalettReload"] = true;

            var resinfo = new ReservationValidations()
            {
                url = "",
                success = 0,
                @params = parameters,
                errorMessage = null
            };

            resinfo = GetValue(resinfo);

            return Json(resinfo, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region "Availability lookup methods"

        private void LoadUnitAvailability(PlanItSearchModel model, List<ResortCriteria> resortsInfo, DateTime checkIn, DateTime checkOut)
        {
            if (model.ValidationSummary.FailureCondition)
            {
                return;
            }

            // Build search criteria

            string siteId = string.Empty;
            string siteName = string.Empty;
            string membershipType = string.Empty;
            string reservationType = string.Empty;

            if (model.ReservationType == ReservationType.Points)
            {
                siteId = ConfigurationManager.AppSettings["PointsSiteId"];
                siteName = "Online Points";
                reservationType = "P";
            }
            else if (model.ReservationType == ReservationType.BonusTime)
            {
                siteId = ConfigurationManager.AppSettings["BonusTimeSiteId"];
                siteName = "Bonus Time";
                membershipType = ((BGO.OwnerWS.Owner)Session["BXGOwner"]).membershipLevel;
                reservationType = "B";
            }

            var availabilityService = new AvailabilityServiceSoapClient();
            var resultsCollector = new List<BGO.availabilityservice.AvailabilityMergedXMLs>();
            var searchCriteria = new AvailabilitySearchCriteria()
            {
                LOS = model.LengthOfStay,
                Accomodates = 2,
                SiteId = siteId,
                SiteName = siteName,
                ReservationType = reservationType,
                OwnerMemberShipType = membershipType,
                HandicapAccessible = model.IsWheelchairAccessible,
                SearchUnitsCount = ConfigurationManager.AppSettings["NumUnitsAvailable"] ?? "10",
                ExtendResortList = "",
                Segments = ""
            };

            searchCriteria.CheckinDate = checkIn.ToShortDateString();
            searchCriteria.CheckoutDate = checkOut.ToShortDateString();

            foreach (var resortInfo in resortsInfo)
            {
                searchCriteria.ResortInfo = new ResortCriteria[] { resortInfo };

                var requestStart = DateTime.Now;
                var results = availabilityService.GetBGOInventoryAvailability(searchCriteria).mylist;
                var requestEnd = DateTime.Now;

                if (results != null)
                {
                    resultsCollector.AddRange(results);
                    System.Diagnostics.Debug.WriteLine(string.Format("resort {0} {1}: got {2} results, {3} total ({4} seconds elapsed)",
                        resortInfo.ResortId, checkIn.ToShortDateString(), results.ToList().Count, resultsCollector.Count, (requestEnd - requestStart).TotalSeconds));
                }
            }

            // Not sure if we should be doing this each time when the individual queries are not user-initiated actions
            if (model.SearchType == SearchType.Destination)
            {
                // TODO: Verify that model.CheckInDate is indeed what goes here
                OwnerWebStats(model, resultsCollector.Count, checkIn);
            }

            // Turn our big long availability results list into a list of distinct resorts, each
            // with its own list of available units.
            model.AvailableResorts = resultsCollector
                .Distinct(new AvailabilityResultComparer())
                .Select(x => AttachUmbracoProperties(new ResortInfoModel
                {
                    ID = x.ResortInfo.ResortID.ToString(),
                    AvailableUnits = PlanItSearchResultsMapper.Map(x.ResortInfo.ResortID, resultsCollector)
                }, x.ResortInfo, null))
                .ToList();
        }

        private string UnitTypeName(string unitCode)
        {
            if (!unitTypes.ContainsKey(unitCode))
            {
                return unitCode;
            }
            else
            {
                return unitTypes[unitCode];
            }
        }

        private int UnitTypeSortOrder(string unitCode)
        {
            if (!unitTypeSortOrder.ContainsKey(unitCode))
            {
                return 0;
            }
            else
            {
                return unitTypeSortOrder[unitCode];
            }
        }

        private string GetStateName(string key)
        {
            if (stateNames.ContainsKey(key))
            {
                return stateNames[key];
            }
            else
            {
                return key;
            }
        }

        /// <summary>
        /// Given a ResortInfo object with only its ID property populated, tries to retrieve the remaining properties from Umbraco.
        /// If the resort is not in Umbraco, it tries one of serviceInfo, a Resort object from the availability service,
        /// or databaseInfo, a data row returned from a stored procedure call to the BluegreenOnline database.
        /// </summary>
        /// <param name="resortInfo">The model to populate</param>
        /// <param name="serviceInfo">Resort information returned as a BGO.availabilityservice.Resort object from the availability service</param>
        /// <param name="databaseInfo">Resort information returned as a DataRow from the database</param>
        /// <returns></returns>
        private ResortInfoModel AttachUmbracoProperties(ResortInfoModel resortInfo, BGO.availabilityservice.Resort serviceInfo, DataRow databaseInfo)
        {
            bool foundInUmbraco = false;

            if (ourResorts != null)
            {
                var resort = ourResorts.Children.ToList().Where(x => x.GetPropertyValue<string>("databaseid") == resortInfo.ID).FirstOrDefault();
                if (resort != null)
                {
                    resortInfo.Name = resort.GetPropertyValue<string>("resortname");
                    resortInfo.Address1 = resort.GetPropertyValue<string>("address1");
                    resortInfo.Address2 = resort.GetPropertyValue<string>("address2");
                    resortInfo.City = resort.GetPropertyValue<string>("city");
                    resortInfo.State = resort.GetPropertyValue<string>("state");
                    resortInfo.PostalCode = resort.GetPropertyValue<string>("zipcode");
                    resortInfo.Phone = resort.GetPropertyValue<string>("phonenumber");
                    resortInfo.Thumbnail = resort.GetPropertyValue<string>("resortimage");
                    resortInfo.Url = resort.Url;

                    foundInUmbraco = true;
                }
            }

            if (!foundInUmbraco)
            {
                if (serviceInfo != null)
                {
                    resortInfo.Name = serviceInfo.ResortName;
                    resortInfo.Address1 = serviceInfo.ResortAddr1;
                    resortInfo.Address2 = serviceInfo.ResortAddr2;
                    resortInfo.City = serviceInfo.ResortCity;
                    resortInfo.State = serviceInfo.ResortState;
                    resortInfo.PostalCode = serviceInfo.ResortPostalCode;
                    resortInfo.Phone = serviceInfo.ResortPhone;
                } else if (databaseInfo != null) {
                    resortInfo.Name = databaseInfo["ResortName"].ToString();
                    resortInfo.Address1 = databaseInfo["ResortAddress"].ToString();
                    resortInfo.City = databaseInfo["ResortCity"].ToString();
                    resortInfo.State = databaseInfo["ResortState"].ToString();
                    resortInfo.PostalCode = databaseInfo["ResortPostalCode"].ToString();
                    resortInfo.Phone = databaseInfo["ResortPhone"].ToString();
                }
            }

            return resortInfo;
        }

        private string GetResortUrl(int id)
        {
            foreach (IPublishedContent resort in ourResorts.Children)
            {
                if (resort.GetPropertyValue<int>("DatabaseID") == id)
                {
                    return resort.Url;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Collects check-in dates from available units into a list.
        /// </summary>
        /// <param name="availability">A DataTable containing destination search results</param>
        /// <returns></returns>
        private List<Tuple<DateTime, DateTime>> GetCheckInOutDates(DataTable availability)
        {
            var result = new List<Tuple<DateTime, DateTime>>();

            foreach (DataRow row in availability.Rows)
            {
                if (row["CheckInDate"] != null && row["CheckOutDate"] != null)
                {
                    result.Add(Tuple.Create((DateTime)row["CheckInDate"], (DateTime)row["CheckOutDate"]));
                }
                // TODO: Log null check-in/check-out date from availability service
            }

            return result.Distinct().ToList();
        }

        private List<Tuple<DateTime, string, string>> GetResortUnitTypesByDate(DataTable availability)
        {
            DateTime priorDateTime = DateTime.MinValue, currentDateTime;
            string priorResortID = "", currentResortID = "";
            string priorUnitCode="", currentUnitCode="", unitTypeList="";
            var result = new List<Tuple<DateTime, string, string>>();

            foreach (DataRow row in availability.Rows)
            {
                currentDateTime = Convert.ToDateTime(row["CheckInDate"]);
                currentResortID = Convert.ToString(row["ResortID"]);
                currentUnitCode = Convert.ToString(row["BluegreenUnitCode"]);

                if(priorDateTime == DateTime.MinValue)
                {
                    // this is the first pass, so set the "prior" values to the current values
                    priorDateTime = currentDateTime;
                    priorResortID = currentResortID;
                }

                if(currentDateTime.CompareTo(priorDateTime) == 0 && currentResortID == priorResortID)
                {
                    if (priorUnitCode != currentUnitCode)
                    {
                        if (unitTypeList.Length > 0)
                        {
                            unitTypeList += ",";
                        }

                        unitTypeList += currentUnitCode.ToString();

                        priorUnitCode = currentUnitCode;
                    }
                }
                else
                {
                    // add the prior data to a new tuple in the list
                    result.Add(Tuple.Create(priorDateTime, priorResortID.ToString(), unitTypeList));
                    
                    priorDateTime = currentDateTime;
                    priorResortID = currentResortID;
                    priorUnitCode = currentUnitCode;

                    unitTypeList = "";
                    unitTypeList += currentUnitCode.ToString();
                }
            }

            if(priorDateTime != DateTime.MinValue)
            {
                // final record
                // add the prior data to a new tuple in the list
                result.Add(Tuple.Create(priorDateTime, priorResortID, unitTypeList));
            }

            return result.Distinct().ToList();
        }

        private List<ResortFilterModel> PopulateResortGrid(PlanItSearchModel model)
        {
            var dt = unitAvailabilityTable.DefaultView.ToTable(true, "ResortId", "ResortName");
            var view = new DataView(unitAvailabilityTable);
            view.Sort = "ResortId, UnitCodeOrder";
            var distinctValues = view.ToTable(true, "ResortId", "ResortName", "BluegreenUnitCode", "BluegreenUnitTypeName");
            var distinctResorts = view.ToTable(true, "ResortId", "ResortName");

            var filterResorts = new List<ResortFilterModel>();
            
            foreach (DataRow row in distinctResorts.Rows)
            {
                var projectNumber = (int)row["ResortId"];

                var filterResort = new ResortFilterModel()
                {
                    Included = true,
                    Name = (string)row["ResortName"],
                    ProjectNumber = projectNumber,
                    HiddenProjStatus = false
                };

                filterResort.UnitTypes = PopulateUnitTypes(distinctValues, projectNumber);
                filterResorts.Add(filterResort);
            }

            return filterResorts;
        }

        private List<ResortUnitModel> PopulateUnitTypes(DataTable distinctValues, int projectNumber)
        {
            var units = distinctValues.Select("ResortId = " + projectNumber);

            return units
                .AsEnumerable()
                .Select(row => new ResortUnitModel
                {
                    Included = true,
                    UnitTypeCode = (string)row["BluegreenUnitCode"],
                    UnitTypeName = (string)row["BluegreenUnitTypeName"]
                })
                .ToList();
        }

        private DataTable LoadCalendarData(PlanItSearchModel model)
        {
            DataTable unitAvailabilityTable = new DataTable("UnitAvailability");

            if (model.ValidationSummary.FailureCondition)
            {
                return unitAvailabilityTable;
            }

            int SearchWindow = 30;
            DateTime todaysDate = new DateTime(model.ReservationYear, model.ReservationMonth, 1);
            DateTime CheckInDate = todaysDate;
            int LOS = model.LengthOfStay;
            string ResortID = "1";
            int Accomodates = 2;
            bool Handicapped = false;
            if (model.IsWheelchairAccessible == true)
            {
                Handicapped = true;
            }
            int noofdaysinMonth = 0;
            int noofdayspast = 0;
            string siteid = string.Empty;


            try
            {
                // Find the first day of the visible month
                DateTime d = new DateTime(todaysDate.Year, todaysDate.Month, 1);

                DateTime FirstDate = new DateTime();
                //DayofWeek.Sunday = 0, so we subtract 7 days because it starts on second week of calendar
                if (d.DayOfWeek == DayOfWeek.Sunday)
                {
                    FirstDate = d.AddDays(-7);
                }
                else
                {
                    //All other day are 1 - 6 respectively, so we negate them(d.DayofWeek * -1) to subtract
                    FirstDate = d.AddDays((int)d.DayOfWeek * -1);
                }

                DateTime today = DateTime.Today;
                DateTime endOfMonth = new DateTime(todaysDate.Year, todaysDate.Month, DateTime.DaysInMonth(todaysDate.Year, todaysDate.Month));

                //Calender always displays 6 weeks, so we add 6 weeks to the firstdate and subtract 1 day which is the last visible day
                DateTime LastDate = FirstDate.AddDays(6 * 7 - 1);

                if (todaysDate <= DateTime.Now.Date.AddDays(minAdvSearchWindow))
                {
                    FirstDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(minAdvSearchWindow));
                    CheckInDate = FirstDate;
                }
                else if (FirstDate < DateTime.Now.Date.AddDays(minAdvSearchWindow))
                {
                    FirstDate = Convert.ToDateTime(DateTime.Now.Date.AddDays(minAdvSearchWindow));
                }
                //SendError("start LoadCalendarData2", "after resorts", "after resorts")
                if (model.ReservationType == ReservationType.Points)
                {
                    if (todaysDate.Month == DateTime.Now.Date.AddMonths(11).Month)
                    {
                        LastDate = Convert.ToDateTime(DateTime.Now.Date.AddMonths(11).ToString("d"));
                    }
                    CheckInDate = FirstDate;
                    TimeSpan span = (LastDate - FirstDate);
                    SearchWindow = (int)span.TotalDays + 1;
                    siteid = ConfigurationManager.AppSettings["PointsSiteId"];
                    btMaxSearchDate = DateTime.Now.Date.AddMonths(11);

                }
                else if (model.ReservationType == ReservationType.BonusTime)
                {
                    DateTime maxdate = Convert.ToDateTime(DateTime.Now.Date.AddDays(48).ToShortDateString());
                    if (model.DestinationResortIDs.Count == 1 && (model.DestinationResortIDs.Contains(16) || model.DestinationResortIDs.Contains(46)))
                    {
                        maxdate = Convert.ToDateTime(DateTime.Now.Date.AddDays(93).ToShortDateString());

                    }
                    btMaxSearchDate = maxdate;

                    if (FirstDate > maxdate)
                    {
                        return unitAvailabilityTable;
                    }

                    CheckInDate = FirstDate;
                    TimeSpan span = default(TimeSpan);
                    if (LastDate < maxdate)
                    {
                        span = (LastDate - FirstDate);
                    }
                    else
                    {
                        span = (maxdate - FirstDate);

                    }

                    SearchWindow = (int)span.TotalDays + 1;
                    siteid = ConfigurationManager.AppSettings["BonusTimeSiteId"];

                }
                //SendError("start LoadCalendarData3", "after resorts", "after resorts")
                //split the code as diff function
                SqlConnection SqlConnection = new SqlConnection(ConfigurationManager.AppSettings["bxgwebDBConnectionPoints"]);
                SqlCommand SqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                CultureInfo provider = CultureInfo.InvariantCulture;

                try
                {
                    SqlCommand.CommandText = "uspBGOInvntoryCalendar";
                    SqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlCommand.Parameters.Clear();
                    SqlCommand.Connection = SqlConnection;

                    //if (!(ConfigurationManager.AppSettings["PCEnvironment"] == "Production"))
                    //{
                    //    SqlCommand.CommandTimeout = 1200;
                    //    //In seconds
                    //}

                    SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CheckInDate", System.Data.SqlDbType.SmallDateTime)).Value = CheckInDate.ToShortDateString();

                    SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResortID", System.Data.SqlDbType.VarChar, 30)).Value = string.Join(",", model.DestinationResortIDs);
                    SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Accomodates", System.Data.SqlDbType.SmallInt)).Value = Accomodates;

                    SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SearchWindow", System.Data.SqlDbType.Int)).Value = SearchWindow;
                    SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SiteId", System.Data.SqlDbType.SmallInt)).Value = siteid;

                    if (model.IsWheelchairAccessible)
                    {
                        SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Handicapped", System.Data.SqlDbType.Bit)).Value = 1;
                    }
                    else
                    {
                        SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Handicapped", System.Data.SqlDbType.Bit)).Value = 0;
                    }

                    if (isFullWeekResort == true)
                    {
                        SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FullWeekResort", System.Data.SqlDbType.Bit)).Value = 1;
                        SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LOS", System.Data.SqlDbType.SmallInt)).Value = 7;
                        model.LengthOfStay = 7;
                    }
                    else
                    {
                        SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FullWeekResort", System.Data.SqlDbType.Bit)).Value = 0;
                        SqlCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LOS", System.Data.SqlDbType.SmallInt)).Value = LOS;
                    }

                    da.SelectCommand = SqlCommand;

                    da.Fill(unitAvailabilityTable);

                    try
                    {
                        // TODO: Replace DateTime.Now.Date with what the currently selected date should be here
                        bool webstats = OwnerWebStats(model, unitAvailabilityTable.Rows.Count, CheckInDate);
                    }
                    catch (Exception ex)
                    {
                        NotifyError(model, ex, "OwnerWebStats call");
                    }

                }
                catch (Exception ex)
                {
                    NotifyError(model, ex, "uspBGOInvntoryCalendar call");
                }
                finally
                {
                    if (SqlConnection.State == ConnectionState.Open)
                    {
                        SqlConnection.Close();
                    }
                }

                if (model.IsSamplerOwner)
                {
                    SamplerOwnerCheckInValidation(model, unitAvailabilityTable);
                }
            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "LoadCalendarData call");
            }
            //start

            return unitAvailabilityTable;

        }

        protected void SamplerOwnerCheckInValidation(PlanItSearchModel model, DataTable unitAvailabilityTable)
        {
            SamplerOwner oSamplerOwner = new SamplerOwner();
            //create new Sampler owner object to access ResortNoBook function
            string strMessage = string.Empty;
            //will always be true for non-Sampler owner
            string resortstate = "";
            int statetotal = 0;
            try
            {
                foreach (DataRow row in unitAvailabilityTable.Rows)
                {
                    if (model.SearchType == SearchType.Date)
                    {
                        DateTime ckoutDate = Convert.ToDateTime(model.CheckOutDate);
                        strMessage = oSamplerOwner.ReservationEligibility(DateTime.Parse(model.CheckInDate), (int)row["ResortID"], string.Empty, ckoutDate);
                    }
                    else
                    {
                        strMessage = oSamplerOwner.ReservationEligibility((DateTime)row["CheckInDate"], (int)row["ResortID"], string.Empty, (DateTime)row["CheckOutDate"]);
                    }

                    if (!(strMessage == string.Empty))
                    {
                        row.Delete();
                    }
                }

                unitAvailabilityTable.AcceptChanges();

            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "Checking reservation eligibility in SamplerOwnerCheckInValidation");
            }

            try
            {
                if (model.SearchType == SearchType.Date)
                {
                    foreach (DataRow row in unitAvailabilityTable.Rows)
                    {
                        if (resortstate != (string)row["ResortState"])
                        {
                            resortstate = (string)row["ResortState"];
                        }
                        DataRow[] dtrow = unitAvailabilityTable.Select("ResortState = '" + resortstate + "'");
                        statetotal = dtrow.Length;
                        row["TotalCount"] = statetotal;

                    }
                }
            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "Calculating availability totals in SamplerOwnerCheckInValidation");
            }

        }

        /// <summary>
        /// Updates a selection of stats for the current owner.
        /// 
        /// Differences from original:
        /// Since Calendar1 doesn't exist and we can't check its SelectedDate, the searchType
        /// parameter has been replaced with the nullable DateTime parameter selectedDate.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="count"></param>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        private bool OwnerWebStats(PlanItSearchModel model, int count, DateTime? selectedDate = null)
        {
            try
            {
                string city1 = model.DestinationCity;
                var C = new BGO.clsDBConnectivity();
                int result;
                string experience = string.Empty;
                string SearchTabSubVal = "";
                string searchtabvalue = "";
                string SearchTab = "";
                string UnitTypes = "";
                DateTime checkindate = new DateTime();
                DateTime checkoutdate = new DateTime();
                // ElapsedTime never gets set in the original code, so this variable is always "00"!
                string _elapsedtime = "00"; //ElapsedTime.ToString.Substring(6, 2);
                SearchTabSubVal = city1;
                SearchTab = "Dest";

                if (model.SearchType == SearchType.Destination)
                {
                    SearchTab = "Dest";
                    checkindate = selectedDate.Value;
                    checkoutdate = checkindate.AddDays(model.LengthOfStay);
                }
                else if (model.SearchType == SearchType.Date)
                {
                    checkindate = new DateTime(model.ReservationYear, model.ReservationMonth, 1);
                    checkoutdate = checkindate.AddDays(model.LengthOfStay);
                    UnitTypes = "";
                }

                string cursession = string.Empty;
                int arvact = 0;
                if (Session != null)
                {
                    experience = (string)Session["experience"];
                    cursession = Session.SessionID;
                    arvact = Convert.ToInt32(((Owner)Session["BXGOwner"]).Arvact);
                }

                C.dbCmnd.CommandText = "uspOwnerWebStats";
                C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;
                C.dbCmnd.Parameters.Clear();
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WebSessionID", System.Data.SqlDbType.VarChar, 75)).Value = cursession;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SiteID", System.Data.SqlDbType.TinyInt)).Value = "7";
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OwnerID", System.Data.SqlDbType.Int)).Value = arvact;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SearchTab", System.Data.SqlDbType.VarChar, 5)).Value = SearchTab;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SearchTabValue", System.Data.SqlDbType.VarChar, 35)).Value = searchtabvalue;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SearchTabSubVal", System.Data.SqlDbType.VarChar, 35)).Value = SearchTabSubVal;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ResortID", System.Data.SqlDbType.VarChar, 15)).Value = string.Join(",", model.DestinationResortIDs);
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BGOUnitType", System.Data.SqlDbType.VarChar, 15)).Value = UnitTypes;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjectNumber", System.Data.SqlDbType.Int)).Value = 0;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnitType", System.Data.SqlDbType.VarChar, 2)).Value = UnitTypes;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CheckInDate", System.Data.SqlDbType.DateTime)).Value = checkindate;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CheckOutDate", System.Data.SqlDbType.DateTime)).Value = checkoutdate;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NumberOfGuests", System.Data.SqlDbType.TinyInt)).Value = 0;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SumBal", System.Data.SqlDbType.Float)).Value = 0.0;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReservationNumber", System.Data.SqlDbType.VarChar, 20)).Value = "";
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phase", System.Data.SqlDbType.TinyInt)).Value = 1;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecordReturnCode", System.Data.SqlDbType.Int)).Value = count;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExtendedResort", System.Data.SqlDbType.Bit)).Value = false;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExtendedStay", System.Data.SqlDbType.Bit)).Value = false;
                if (model.IsWheelchairAccessible)
                {
                    C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Handycap", System.Data.SqlDbType.Bit)).Value = true;
                }
                else
                {
                    C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Handycap", System.Data.SqlDbType.Bit)).Value = false;
                }

                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@QryTime", System.Data.SqlDbType.Int)).Value = _elapsedtime;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime)).Value = DateTime.Now.Date;
                C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreatedBy", System.Data.SqlDbType.Char, 20)).Value = "Web";
                try
                {
                    result = C.dbCmnd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    NotifyError(model, ex, "uspOwnerWebStats call", fail: false);

                }
                finally
                {
                    C.Close();

                }

            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "OwnerWebStats", fail: false);
            }

            return true;

        }

        private void VerifyResortIds(PlanItSearchModel model)
        {
            model.DestinationResortIDs = DestinationsFromAS400.GetResortsByCity(model.ReservationType == ReservationType.BonusTime, model.DestinationCity, model.DestinationState, model.HomeProject);

            if (model.DestinationResortIDs.Contains(30) && model.DestinationResortIDs.Contains(31) && model.DestinationResortIDs.Contains(61))
            {
                if (model.LengthOfStay != 7)
                {
                    model.ValidationSummary.Add(string.Format("To view availability at additional resorts in {0}, {1} that require a full week stay, please adjust your search to 7 nights.", model.DestinationCity, model.DestinationState), fail: false);
                    model.DestinationResortIDs.Remove(31);
                    model.DestinationResortIDs.Remove(61);
                    isFullWeekResort = IsFullWeekResort(model, model.DestinationResortIDs[0]);
                }
                else
                {
                    isFullWeekResort = IsFullWeekResort(model, model.DestinationResortIDs[1]);
                }
            } else
            {
                isFullWeekResort = IsFullWeekResort(model, model.DestinationResortIDs[0]);
            }

            if (model.LengthOfStay == 1 && model.ReservationType == ReservationType.BonusTime && model.DestinationResortIDs.Contains(24))
            {
                model.DestinationResortIDs.Remove(24);
            }
        }

        private bool IsFullWeekResort(PlanItSearchModel model, int id)
        {
            bool sCheckInDays = false;

            var database = new BGO.clsDBConnectivity();
            SqlDataReader reader = null;
            var command = new SqlCommand("uspGetResortParameters", database.dbConn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ProjectNumber", SqlDbType.VarChar)).Value = id.ToString();

            // TODO: Maybe add a "MinAdvSearchWindow" property to the Resort content type in Umbraco?
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["FullWeekResort"] == DBNull.Value)
                    {
                        sCheckInDays = false;
                    }
                    else if ((bool)reader["FullWeekResort"] == true)
                    {
                        sCheckInDays = true;
                    }

                    // Column actually contains a boxed byte, so .NET will complain if we try to cast directly to int.
                    // So we'll unbox it and let .NET do the cast implicitly.
                    if (model.ReservationType == ReservationType.Points)
                    {
                        minAdvSearchWindow = (byte)reader["MinAdvSearchWindow"];
                    }
                    else
                    {
                        minAdvSearchWindow = (byte)reader["BTAdvResWindow"];
                    }
                }
            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "IsFullWeekResort");
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                database.Close();
            }

            //int? ourResortsContentId = WebConfigContentId.GetValue("ourResortsContentId");
            //if (ourResortsContentId.HasValue)
            //{
            //    IPublishedContent ourResorts = Umbraco.Content(ourResortsContentId.Value);
            //    IPublishedContent resort = ourResorts.Children.Where(x => x.GetPropertyValue<int>("DatabaseID") == id).FirstOrDefault();
            //    if (resort != null)
            //    {
            //        sCheckInDays = resort.GetPropertyValue<bool>("fullWeekResort");
            //    }
            //}

            return sCheckInDays;
        }

        private DataTable GetAvailableUnits(PlanItSearchModel model)
        {
            var unitAvailability = new DataTable();

            using (var connection = new SqlConnection(ConfigurationManager.AppSettings["bxgwebDBConnectionPoints"]))
            {
                var command = new SqlCommand("uspBGOInvntoryCalendar", connection);
                command.CommandType = CommandType.StoredProcedure;

                if (ConfigurationManager.AppSettings["PCEnvironment"] == "Production")
                {
                    command.CommandTimeout = 1200;
                }

                command.Parameters.Add(new SqlParameter("@CheckInDate", SqlDbType.SmallDateTime)).Value = model.CheckInDate;
                command.Parameters.Add(new SqlParameter("@ResortID", SqlDbType.VarChar, 30));
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());
                command.Parameters.Add(new SqlParameter());

                connection.Close();
            }

            return unitAvailability;
        }

        private void HydrateModel(PlanItPanelModel model)
        {
            model.SiteRoot = Url.Content("~");

            // populate properties from server-side for client-side logic
            var owner = (BGO.OwnerWS.Owner)Session["BXGOWner"];
            model.IsSamplerOwner = owner.User[0].isSampler;
            model.HomeProject = owner.User[0].HomeProject;
            model.OnHomePage = CurrentPage.Id == Convert.ToInt32(ConfigurationManager.AppSettings["homeContentId"]);
            // Number of available units in search results is hidden from sales account travelerplus@bxgcorp.com, arvact 430142
            model.ShowAvailabilityCount = owner.Arvact != "430142";

            // Hide entirely from fixed, flex and traditional owners
            var isFixedFlexOrTraditionalOwner = Session["IsFixedFlexOrTraditionalOwner"];
            if (isFixedFlexOrTraditionalOwner is bool && (bool)isFixedFlexOrTraditionalOwner)
            {
                model.IsEnabled = false;
                return;
            }
            else
            {
                model.IsEnabled = true;
            }

            if (CurrentPage.DocumentTypeAlias == "Resort")
            {
                model.InitialDestination = CurrentPage.GetPropertyValue<string>("city").Trim() + ", " + CurrentPage.GetPropertyValue<string>("state").Trim();
            }

            if (Session["SearchCity"] != null && Session["SearchCity"] is string)
            {
                model.InitialDestination = (string)Session["SearchCity"];
                Session.Remove("SearchCity");
            }

            if (Session["SearchLOS"] != null && Session["SearchLOS"] is int)
            {
                model.InitialLOS = (int)Session["SearchLOS"];
                Session.Remove("SearchLOS");
            }

            // destination logic
            var allDestinations = DestinationsFromAS400.GetAllDestinations(model.IsSamplerOwner, model.HomeProject);

            var pointsDestinationsFromUmbraco = GetDestinationsSetByReservationType(ReservationType.Points, x => x.GetPropertyValue<string>("city").ToLower().Trim() + ", " + x.GetPropertyValue<string>("state").ToLower().Trim());
            model.Destinations = allDestinations
                .Where(x => pointsDestinationsFromUmbraco.Contains(x.Description.ToLower()))
                .ToList();
            bool secIdentity = false;

            if (Session["secIdentity"] == null)
            {
                try
                {
                    secIdentity = CheckOwnerSecondaryMktPoint();

                }
                catch (Exception)
                {
                }
            }
            else
            {
                secIdentity = (bool)Session["secIdentity"];
            }

            var bonusTimeDestinationsFromUmbraco = GetDestinationsSetByReservationType(ReservationType.BonusTime, x => x.GetPropertyValue<string>("city").ToLower().Trim() + ", " + x.GetPropertyValue<string>("state").ToLower().Trim());
            var bonusTimeDestinations = secIdentity ? GetSecondaryMarketDestinations(allDestinations) : allDestinations;
            bonusTimeDestinations = bonusTimeDestinations
                .Where(x => bonusTimeDestinationsFromUmbraco.Contains(x.Description.ToLower()))
                .ToList();

            model.FullWeekResorts = JsonConvert.SerializeObject(GetFullWeekResorts());
            model.PointsDestinations = JsonConvert.SerializeObject(model.Destinations.Select(x => new
            {
                name = x.Description,
                resortIDs = x.ResortIDs
            }));
            model.BonusTimeDestinations = JsonConvert.SerializeObject(bonusTimeDestinations.Select(x => new
            {
                name = x.Description,
                resortIDs = x.ResortIDs
            }));

            //var destinationSet = new Dictionary<string, Destination>();

            // loop through the children of Resorts content item and populate the destination member of the model
            // with resorts available to this user. Destinations with multiple resorts are collapsed to a sinle item.
            //if (ourResorts != null)
            //{
            //    foreach (IPublishedContent child in ourResorts.Children)
            //    {
            //        ResortModel resort = ResortMapper.Map(child);

            //        if (resort != null && availableDestinations.Contains(Tuple.Create(resort.City.ToLower(), resort.State.ToLower())))
            //        {
            //            var destLocation = resort.City + ", " + resort.State;
            //            var destLocationUpper = destLocation.ToUpper();
            //            if (!destinationSet.ContainsKey(destLocationUpper))
            //            {
            //                destinationSet[destLocationUpper] = new Destination(0, destLocation)
            //                {
            //                    ResortIDs = new List<int> { resort.LegacyID },
            //                    City = resort.City,
            //                    State = resort.State
            //                };
            //            }
            //            else
            //            {
            //                destinationSet[destLocationUpper].ResortIDs.Add(resort.LegacyID);
            //            }
            //        }
            //    } 
            //}

            //model.Destinations = destinationSet
            //    .Select(x => x.Value)
            //    .OrderBy(x => x.State)
            //    .ThenBy(x => x.City)
            //    .ToList();

            // number of nights logic
            model.NumberOfNights = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            // Month logic
            DateTime dtToday = DateTime.Now.Date, dtFirstOfTheMonth = DateTime.MinValue, dtTwelveMonthsOut = DateTime.MinValue;
            dtFirstOfTheMonth = new DateTime(dtToday.Year, dtToday.Month, 1);

            model.Months = new List<DateTime>();

            for(int nextMonth = 0; nextMonth < 12; nextMonth++)
            {
                model.Months.Add(dtFirstOfTheMonth.AddMonths(nextMonth));
            }
        }

        private List<int> GetFullWeekResorts()
        {
            IPublishedContent ourResorts = null;
            int? ourResortsContentId = WebConfigContentId.GetValue("ourResortsContentId");
            if (ourResortsContentId.HasValue)
            {
                ourResorts = Umbraco.TypedContent(ourResortsContentId.Value);
                return ourResorts.Children
                    .ToList()
                    .Where(x => x.GetPropertyValue<bool>("FullWeekResort"))
                    .Select(x => x.GetPropertyValue<int>("DatabaseID")).ToList();
            }
            else
            {
                return null;
            }
        }

        private List<Destination> GetSecondaryMarketDestinations(List<Destination> allDestinations)
        {
            var destinations = new List<Destination>();
            var C = new BGO.clsDBConnectivity();
            SqlDataReader dbDataReader = null;

            //load secondary market destinations

            string secResorts = Session["secondarymktresorts"].ToString();

            try
            {
                C.Open();

                foreach (string part in secResorts.Split(','))
                {
                    try
                    {
                        C.dbCmnd.CommandText = "uspSelectBTDestinationByAs400Project";
                        C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;
                        C.dbCmnd.Parameters.Clear();
                        C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@as400project", System.Data.SqlDbType.VarChar, 20)).Value = part;

                        dbDataReader = C.dbCmnd.ExecuteReader();

                        while (dbDataReader.Read())
                        {
                            var city = dbDataReader["City"].ToString();
                            var state = dbDataReader["StateCode"].ToString();
                            var destination = new Destination(0, city + ", " + state);
                            
                            // Pick up IDs from resort master list in Umbraco
                            var umbracoDestination = allDestinations.Where(x => x.Description == destination.Description).SingleOrDefault();
                            if (umbracoDestination != null)
                            {
                                destination.ResortIDs = umbracoDestination.ResortIDs;
                            }
                            else
                            {
                                destination.ResortIDs = new List<int>();
                            }

                            destinations.Add(destination);
                        }

                        dbDataReader.Close();
                    }
                    catch (Exception ex)
                    {
                        //SendError(ex.Message.ToString(), "populateBTDestinations", " populateBTDestinations");
                    }
                    finally
                    {
                        if (dbDataReader != null)
                        {
                            dbDataReader.Close();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //SendError(ex.Message.ToString(), "populateBTDestinations1", " populateBTDestinations");
            }
            finally
            {
                C.Close();
                C = null;
            }

            return destinations;
        }

        private int getDaysOutRange(string date)
        {
            return (int)DateTime.Now.Date.Subtract(DateTime.Parse(date)).TotalDays;
        }

        private int getNumDaysSpan(string checkIn, string checkOut)
        {
            return (int)DateTime.Parse(checkOut).Subtract(DateTime.Parse(checkIn)).TotalDays;
        }

        protected void BTAvailabilityValidation(PlanItSearchModel model, ref DataTable unitAvailabilityTable)
        {

            try
            {
                int iDaysOutRange = getDaysOutRange(model.CheckInDate);
                int noofdays = getNumDaysSpan(model.CheckInDate, model.CheckOutDate);

                if (model.ReservationType == ReservationType.Points & noofdays == 1)
                {
                    foreach (DataRow row in unitAvailabilityTable.Rows)
                    {
                        if ((int)row["ResortId"] != 11)
                        {
                            row.Delete();
                        }
                    }

                }
                else if (model.ReservationType == ReservationType.BonusTime)
                {
                    if (iDaysOutRange >= 48)
                    {
                        foreach (DataRow row in unitAvailabilityTable.Rows)
                        {
                            if ((int)row["ResortId"] != 16 && (int)row["ResortId"] != 46)
                            {
                                row.Delete();
                            }
                        }

                    }
                }

                unitAvailabilityTable.AcceptChanges();
            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "BTAvailabilityValidation");
            }

        }

        public DataTable GetCategories(PlanItSearchModel model)
        {
            unitAvailabilityTable = new DataTable("UnitAvailability");

            try
            {
                if (!string.IsNullOrWhiteSpace(model.CheckInDate) & !string.IsNullOrWhiteSpace(model.CheckOutDate))
                {
                    //Load unit availability
                    LoadResortData(model, ref unitAvailabilityTable);

                    BTAvailabilityValidation(model, ref unitAvailabilityTable);
                    Session["inventory"] = unitAvailabilityTable;
                    if (unitAvailabilityTable.Rows.Count == 0)
                    {
                        model.ValidationSummary.Add("We’re sorry, there is no current availability for the dates you have requested.  Please select alternate dates and try your search again.", fail: true);
                    }

                    if (!model.ValidationSummary.FailureCondition)
                    {
                        if (model.IsSamplerOwner)
                        {
                            SamplerOwnerCheckInValidation(model, unitAvailabilityTable);

                            DateTime ckout = Convert.ToDateTime(model.CheckOutDate);
                            SamplerOwner oSamplerOwner = new SamplerOwner();
                            //create new Sampler owner object to access ResortNoBook function
                            string holidaytable = string.Empty;

                            if (string.IsNullOrWhiteSpace(model.CheckInDate))
                            {
                                holidaytable = "";
                            }
                            else
                            {
                                holidaytable = oSamplerOwner.holidayTable(Convert.ToDateTime(model.CheckInDate), ckout);
                            }

                            ckout = ckout.AddDays(-1);

                            int samplerBlockoutMonth = Convert.ToDateTime(model.CheckInDate).Month;

                            if (!string.IsNullOrEmpty(holidaytable.Trim()))
                            {
                                model.ValidationSummary.Add("We’re sorry, Sampler members are not eligible for stays on, the day before or the day after a major holiday. Please select alternate dates and try again.", fail: true);
                            }
                        }

                        DataTable distinctDT = unitAvailabilityTable.DefaultView.ToTable(true, "ResortState", "TotalCount", "ResortText", "SNO");
                        DataView view = new DataView(distinctDT);

                        view.Sort = "ResortState";
                        //assigning sequence number to the state. This code for open multiple panes of the accordion control
                        int cnt = 0;

                        //foreach (DataRow drv in view)
                        //{
                        //    drv["SNO"] = cnt;
                        //    cnt = cnt + 1;

                        //}
                    }
                }
                else
                {
                    // display nothing
                }

            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "GetCategories");
            }

            return unitAvailabilityTable;
        }

        protected void LoadResortData(PlanItSearchModel model, ref DataTable unitAvailabilityTable)
        {
            //Load date results
            int siteid = 0;
            DateTime ckinDate = Convert.ToDateTime(model.CheckInDate);
            int numDays = DateTime.Parse(model.CheckOutDate).Subtract(DateTime.Parse(model.CheckInDate)).Days;
            bool _fullweek = false;
            string _restype = "";
            bool _handycap = false;

            if (model.IsWheelchairAccessible)
            {
                _handycap = true;
            }

            if (numDays == 7)
            {
                _fullweek = true;
            }

            if (model.ReservationType == ReservationType.Points)
            {
                siteid = int.Parse(ConfigurationManager.AppSettings["PointsSiteId"]);
                //_restype = "P";
            }
            else if (model.ReservationType == ReservationType.BonusTime)
            {
                siteid = int.Parse(ConfigurationManager.AppSettings["BonusTimeSiteId"]);
                //_restype = "B";
            }
            _restype = " ";

            unitAvailabilityTable = new DataTable("UnitAvailability");
            SqlConnection SqlConnection = new SqlConnection((string)ConfigurationManager.AppSettings["bxgwebDBConnectionPoints"]);
            SqlCommand SqlCommand = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();

            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                SqlCommand.CommandText = "uspBGOResortInvntoryAvailability";
                SqlCommand.CommandType = CommandType.StoredProcedure;
                SqlCommand.Parameters.Clear();
                SqlCommand.Connection = SqlConnection;
                //SqlCommand.Connection.Open()

                SqlCommand.Parameters.Add(new SqlParameter("@CheckInDate", SqlDbType.SmallDateTime)).Value = ckinDate.ToShortDateString();
                SqlCommand.Parameters.Add(new SqlParameter("@SiteId", SqlDbType.SmallInt)).Value = siteid;
                SqlCommand.Parameters.Add(new SqlParameter("@LOS", SqlDbType.SmallInt)).Value = numDays;
                SqlCommand.Parameters.Add(new SqlParameter("@FullWeekResort", SqlDbType.Bit)).Value = _fullweek;
                SqlCommand.Parameters.Add(new SqlParameter("@Handicapped", SqlDbType.Bit)).Value = _handycap;
                SqlCommand.Parameters.Add(new SqlParameter("@ResType", SqlDbType.Char)).Value = _restype;


                if (model.HomeProject == "51" && model.IsSamplerOwner)
                {
                    SqlCommand.Parameters.Add(new SqlParameter("@OwnerType", SqlDbType.VarChar)).Value = "VCS";
                }
                else if (model.HomeProject == "52" && model.IsSamplerOwner)
                {
                    SqlCommand.Parameters.Add(new SqlParameter("@OwnerType", SqlDbType.VarChar)).Value = "S24";
                }
                else
                {
                    SqlCommand.Parameters.Add(new SqlParameter("@OwnerType", SqlDbType.VarChar)).Value = "VC";
                }

                da.SelectCommand = SqlCommand;
                da.Fill(unitAvailabilityTable);
            }
            catch (Exception ex)
            {
                NotifyError(model, ex, "LoadResortData");
            }
            finally
            {
                if (SqlConnection.State == ConnectionState.Open)
                {
                    SqlConnection.Close();
                }
            }

            //secondary market verification
            if (model.ReservationType == ReservationType.BonusTime)
            {
                bool secIdentity = false;

                if (Session["secIdentity"] == null)
                {
                    try
                    {
                        secIdentity = CheckOwnerSecondaryMktPoint();

                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    secIdentity = (bool)Session["secIdentity"];
                }

                if (secIdentity)
                {
                    secMarketResorts(ref unitAvailabilityTable);
                }
            }
        }

        private bool CheckOwnerSecondaryMktPoint()
        {
            //*******
            //Ancillary benefits - Owner Bonus Time Reservation modifications
            //Declare SQL,As400 connection

            clsDBConnectivityAS400DB2 CAS400DB2;
            iDB2DataAdapter objDataAdapter = default(iDB2DataAdapter);
            DataSet objDataSet = default(DataSet);

            try
            {
                CAS400DB2 = new clsDBConnectivityAS400DB2();
            }
            catch (Exception e)
            {
                return false;
            }

            try
            {
                CAS400DB2.dbCmnd.CommandText = "FRIANO.SCNDMKTPTS";
                CAS400DB2.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;

                CAS400DB2.dbCmnd.Parameters.Add("ARVACT", IBM.Data.DB2.iSeries.iDB2DbType.iDB2Numeric);
                CAS400DB2.dbCmnd.Parameters["ARVACT"].Value = Session["OwnerNumber"];
                CAS400DB2.dbCmnd.Parameters["ARVACT"].Direction = ParameterDirection.Input;
                CAS400DB2.dbCmnd.Parameters["ARVACT"].Precision = 6;
                CAS400DB2.dbCmnd.Parameters["ARVACT"].Scale = 0;

                objDataAdapter = new iDB2DataAdapter(CAS400DB2.dbCmnd);
                objDataSet = new DataSet();
                objDataAdapter.Fill(objDataSet);

            }
            catch (Exception ex)
            {
                // TODO: Send this error
                //NotifyError(model, ex, "CheckOwnerSecondaryMktPoint, FRIANO.SCNDMKTPTS");
                return true;
            }
            finally
            {
                CAS400DB2.Close();
                CAS400DB2 = null;
            }

            string isSecondary = "N";
            string secResorts = "";

            try
            {
                DataTable dt = objDataSet.Tables[0];
                if (objDataSet.Tables[1].Rows.Count == 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((string)dr[2] != "Y")
                        {
                            isSecondary = "N";
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            isSecondary = "Y";
                        }
                        secResorts += dr[0] + ",";
                    }

                    //If owner belongs to only secondary market points
                    //var C = new BGO.clsDBConnectivity();


                    if (isSecondary == "Y")
                    {
                        try
                        {
                            DataColumn colPk = new DataColumn();
                            colPk = objDataSet.Tables[0].Columns["ARHOME"];
                            objDataSet.Tables[0].PrimaryKey = new DataColumn[] { objDataSet.Tables[0].Columns[0] };

                            //Add two columns MSSQL projectID and Projectname
                            DataColumn newColumn = new DataColumn();
                            newColumn = new DataColumn();
                            newColumn.DataType = System.Type.GetType("System.String");
                            newColumn.ColumnName = "msProjectID";

                            objDataSet.Tables[0].Columns.Add(newColumn);

                            newColumn = new DataColumn();
                            newColumn.DataType = System.Type.GetType("System.String");
                            newColumn.ColumnName = "resortname";

                            objDataSet.Tables[0].Columns.Add(newColumn);
                        }
                        catch (Exception ex)
                        {
                            // TODO: Send this error
                            //SendError(ex.Message.ToString(), "CheckOwnerSecondaryMktPoint", "colPk");
                        }

                        secResorts = secResorts.Substring(0, secResorts.Length - 1);

                    }

                }

            }
            catch (Exception ex)
            {
                // TODO: Send this error
                //SendError(ex.Message.ToString(), "CheckOwnerSecondaryMktPoint", "uspSelectBTDestinationByAs400Project");
                return true;
            }
            finally
            {
                //close the connection
            }

            Session["secondarymktresorts"] = secResorts;
            Session["secondaryMkt"] = isSecondary;
            Session["secOwnerDataSet"] = objDataSet;


            if (isSecondary == "Y")
            {
                Session["secIdentity"] = true;
                return true;
            }
            else
            {
                Session["secIdentity"] = false;
                return false;

            }
        }

        public void secMarketResorts(ref DataTable unitAvailabilityTable)
        {
            var C = new BGO.clsDBConnectivity();
            System.Data.SqlClient.SqlDataReader dbDataReader = default(System.Data.SqlClient.SqlDataReader);
            string secResorts = (string)Session["secondarymktresorts"];
            int resortId = -1;
            string[] parts = secResorts.Split(',');
            string part = null;

            try
            {
                DataTable unitFilterTable_backup = new DataTable();
                unitFilterTable_backup.Columns.Add("ResortID", typeof(int));
                unitFilterTable_backup.Columns.Add("ResortName", typeof(string));
                unitFilterTable_backup.Columns.Add("ResortAddress", typeof(string));
                unitFilterTable_backup.Columns.Add("ResortCity", typeof(string));
                unitFilterTable_backup.Columns.Add("ResortState", typeof(string));
                unitFilterTable_backup.Columns.Add("ResortPostalCode", typeof(string));
                unitFilterTable_backup.Columns.Add("ResortPhone", typeof(string));
                unitFilterTable_backup.Columns.Add("BluegreenUnitCode", typeof(string));
                unitFilterTable_backup.Columns.Add("TotalCount", typeof(string));
                unitFilterTable_backup.Columns.Add("ResortText", typeof(string));
                unitFilterTable_backup.Columns.Add("SNO", typeof(string));


                C.Open();
                List<DataRow> drlist = new List<DataRow>();
                //select only secondary market resorts
                foreach (string part_loopVariable in parts)
                {
                    part = part_loopVariable;
                    C.dbCmnd.CommandText = "uspSelectResortID";
                    C.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;
                    C.dbCmnd.Parameters.Clear();
                    C.dbCmnd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProjNum", System.Data.SqlDbType.VarChar, 20)).Value = part;

                    dbDataReader = C.dbCmnd.ExecuteReader();

                    while (dbDataReader.Read())
                    {
                        resortId = (int)dbDataReader["Resortid"];
                    }

                    dbDataReader.Close();

                    foreach (DataRow row in unitAvailabilityTable.Rows)
                    {
                        if ((int)row["ResortId"] == resortId)
                        {
                            unitFilterTable_backup.ImportRow(row);
                        }
                    }

                }

                unitAvailabilityTable.Rows.Clear();
                unitAvailabilityTable.AcceptChanges();

                unitAvailabilityTable = unitFilterTable_backup.Copy();
            }
            catch (Exception ex)
            {
                // TODO: send this error
                //SendError(ex.Message.ToString(), "populateBTDestinations", " populateBTDestinations");
            }
            finally
            {
                C.Close();
                C = null;
            }

            dynamic SecondaryRess = secResorts.Split(',');
            string SecondaryRes = null;

            foreach (string SecondaryRes_loopVariable in SecondaryRess)
            {
                SecondaryRes = SecondaryRes_loopVariable;
            }
        }

        #endregion

        #region "Booking validation"

        private ReservationValidations GetValue(ReservationValidations resinfo)
        {
            string message = "";
            Owner BXGOwner = (Owner)Session["BXGOwner"];
            string lblMEssage = "";
            bool savePointsFlag = false;
            int reservationsTotal = 0;
            bool fspe = false;
            var resService = new BGO.ResortsService.ResortsServiceClient();
            var unitRequest = new BGO.ResortsService.EvaluateBookingRequest();
            var unitResponse = new BGO.ResortsService.BookingScreeningResponse();

            try
            {
                byte[] decodedBytes = null;
                decodedBytes = Convert.FromBase64String(resinfo.@params);
                string decodedText = null;
                decodedText = Encoding.UTF8.GetString(decodedBytes);
                string[] _params = decodedText.Split('|');
                string restype = substrfunction(_params[10], "");

                if (restype == "PT")
                {
                    resinfo.url = HtmlExtensions.CustomHtmlHelpers.GetParentSitePath(null).ToString() + "/Owner/ptsReserv.aspx?params=" + resinfo.@params;
                    resinfo.resType = "PT";
                }
                else if (restype == "BT")
                {
                    resinfo.url = HtmlExtensions.CustomHtmlHelpers.GetParentSitePath(null).ToString() + "/BonusTime/reservConfirm.aspx?params=" + resinfo.@params;
                    resinfo.resType = "BT";
                }

                // CreditCardInfoController redirects to this URL after completing payment
                Session["CcformRedirectUrl"] = resinfo.url;

                //assigh the parameters
                DateTime checkin = DateTime.Parse(substrfunction(_params[0], "Date"));
                string noofdays = substrfunction(_params[1], "");
                DateTime ckout = checkin.AddDays(Convert.ToInt32(noofdays));

                int ProjectNumber = Convert.ToInt32(substrfunction(_params[2], ""));
                string resortid = substrfunction(_params[3], "");
                Session["ResortId"] = resortid;
                string UnitType = substrfunction(_params[4], "");
                string UnitNumber = substrfunction(_params[5], "");
                string handycap = substrfunction(_params[6], "");
                string Points = substrfunction(_params[7], "");
                string Season = substrfunction(_params[8], "");
                dynamic ourResorts = null;
                int ourResortsContentId;
                string ourResortsContentIdString = ConfigurationManager.AppSettings["ourResortsContentId"];
                if (!string.IsNullOrWhiteSpace(ourResortsContentIdString) && Int32.TryParse(ourResortsContentIdString, out ourResortsContentId))
                {
                    ourResorts = Umbraco.TypedContent(ourResortsContentId);
                }

                //Populate the image based on the resorts.
                if (ourResorts != null)
                {
                    foreach (IPublishedContent content in ourResorts.Children)
                    {
                        if (content.DocumentTypeAlias.Equals("Resort"))
                        {
                            if (resortid == content.GetPropertyValue<string>("DatabaseId"))
                            {
                                Session["GeneralReservResortImage"] = content.GetPropertyValue<string>("ResortImage");
                                Session["GeneralReservResortUrl"] = content.Url;

                            }
                        }
                    }
                }

                try
                {
                    

                    unitRequest.CheckInDate = checkin;
                    unitRequest.LengthOfStay = Convert.ToInt32(noofdays);

                    if (BXGOwner.User[0].project == "50")
                    {
                        unitRequest.OwnerAccountProject = BXGOwner.User[0].project;
                    }
                    else
                    {
                        unitRequest.OwnerAccountProject = BXGOwner.User[0].HomeProject;
                    }

                    unitRequest.OwnerID = new BGO.ResortsService.OwnerID();
                    unitRequest.OwnerID.OwnerVacationNumber = BXGOwner.Arvact;
                    unitRequest.ProjectStay = ProjectNumber.ToString();

                    if (restype == "PT")
                    {
                        unitRequest.SiteName = BGO.ResortsService.Sites.OnlinePoints;
                        unitRequest.ReservationTypeName = "P";
                    }
                    else
                    {
                        unitRequest.SiteName = BGO.ResortsService.Sites.BonusTime;
                        unitRequest.ReservationTypeName = "B";
                    }

                    unitRequest.Points = SafeParser.ParseInt(Points);

                    unitRequest.UnitType = UnitType;
                    if (seasonNames.Keys.Contains(Season.Trim().ToUpper()))
                    {
                        unitRequest.Season = seasonNames[Season.Trim().ToUpper()];
                    }

                    unitResponse = resService.ScreeningBookReservation(unitRequest);

                    if (resService.State != System.ServiceModel.CommunicationState.Faulted)
                    {
                        resService.Close();
                    }

                    //initialize the session
                    Session["BookingScreeningResponse"] = null;

                    if (unitResponse != null && unitResponse.Success)
                    {
                        //BookStatus = 0 - means reservation cannot be book
                        //BookStatus = 1 - means reservation can be booked
                        //BookStatus = 2 - means reservation can be booked with SPE

                        try
                        {
                            reservationsTotal = GetReservationsCount(ProjectNumber.ToString());


                            Session["BookingScreeningResponse"] = unitResponse;
                            resinfo.SPECost = string.Format("{0:0.00}", unitResponse.SPECost);

                            if (unitResponse.BookStatus == BGO.ResortsService.BookStatusType.CanBeBookedwithSPE)
                            {
                                savePointsFlag = true;
                            }
                            else if (unitResponse.BookStatus == BGO.ResortsService.BookStatusType.CanBeBookedwithFuture)
                            {
                                //to be handled future reservation'
                                string successurl = resinfo.url;
                                lblMEssage = "<div align='left' class='roundedPanel' style='margin-bottom: 5px; border-radius: 10px; border: 0px none;'><div style='color:black;padding:10px;'>This reservation will use Points from your next use year and will be reflected in your Future Points balance.  Click below to agree and continue.</div><center><a href=\"" + successurl + "\"><div class='btn_continue'></div></a></center></div>";


                            }
                            else if (unitResponse.BookStatus == BGO.ResortsService.BookStatusType.CanBeBookedwithFutureSPE)
                            {
                                fspe = true;
                                savePointsFlag = true;
                            }

                        }
                        catch (Exception ex)
                        {
                            //reservationsTotal = 0;
                            //StringBuilder errMsg = new StringBuilder();
                            //errMsg.Append("Error happened fetching Reservations Count");
                            //errMsg.Append(ex.Message);
                            //sendMessage("OLPSupport@bxgcorp.com", "", "Exception Error - Getting Reservation Count", errMsg);
                        }

                        //The logical provided by AS400 it's a little bit fuzzy. The reason to state it is due to the fact AS400 cannot provide 
                        //how many points is available for owner on the check IN/OUT - this the PRE1. So, on the scenario where
                        //Owner does not have enough points, the return value on "PointsAvailable" WILL be ALWAYS ZERO. 
                        //Also, there is a slight percentage if a record become lock can interfere on the result and the total points "PointsAvailable"
                        //will be zero-out, OR better any odd scenario no handle by AS400 will be zero-out.


                        if (unitResponse.BookStatus == BGO.ResortsService.BookStatusType.CannotBeDone)
                        {

                            if (unitResponse.OwnerGoodStand == true & unitResponse.OwnerHasPastDueAccount == false & unitResponse.OwnerEligible == true)
                            {
                                List<OwnerPointsStatus> Accounts = new List<OwnerPointsStatus>();
                                List<ReservationDailyRates> dailyRates = default(List<ReservationDailyRates>);
                                string totalPointsReq = string.Format("{0:#,###}", SafeParser.ParseInt(Points));
                                try
                                {
                                    dailyRates = GetReservationDailyRates(unitRequest.CheckInDate, ckout, unitRequest.ProjectStay, unitRequest.UnitType);
                                    resinfo.resType = restype;
                                    resinfo.CheckOutDate = ckout.ToShortDateString();
                                    resinfo.CheckInDate = checkin.ToShortDateString();
                                    resinfo.TotalPointsRequired = totalPointsReq;
                                    resinfo.Rates = dailyRates;
                                    int elgPoints = 0;
                                    Accounts = GetOwnerPoints(unitResponse.PointsEligibility, unitRequest.CheckInDate, ref elgPoints);
                                    resinfo.Accounts = Accounts;
                                    if (elgPoints > 0)
                                    {
                                        resinfo.TotalEligiblePoints = string.Format("{0:#,###}", elgPoints);
                                    }
                                    else
                                    {
                                        resinfo.TotalEligiblePoints = elgPoints.ToString();
                                    }

                                    lblMEssage = "You are not eligible";


                                }
                                catch (Exception ex)
                                {
                                    if (restype == "PT")
                                    {
                                        string theEncryptedOwnerID = StringEncode(BXGOwner.Arvact, PrivateEncryptionKey);

                                        lblMEssage = "You do not have enough Points on your account to complete this reservation." + " Please modify your search or select a different villa." + " <a href=\"#\" onclick=\"javascript:newWin=window.open('" + ConfigurationManager.AppSettings["bxgwebSecureURL"] + "owner/ownerpointsdetail.aspx?id=" + theEncryptedOwnerID + "','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=590,height=450,left=50,top=20');newWin.focus(); return false;\">Click here</a>" + " to see a breakdown of your Points. If you are a Traveler Plus member, you may rent an allotment of additional Points" + " that may be used in any season.  Click Traveler Plus in the top menu to learn more.  To borrow Points from " + " your next use year, please call 800.456.CLUB(2582) for further assistance." + " <a href=\"#\" onclick=\"javascript:newWin=window.open('" + CustomHtmlHelpers.GetParentSitePath(null) + "/owner/helpWinHours.aspx','newWin','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=590,height=450,left=50,top=20');newWin.focus(); return false;\">Click here</a> for hours of operation.";
                                    }

                                }

                            }
                            else
                            {
                                lblMEssage = "Due to the current status of one or more of your accounts, we cannot allow online reservations at this time. Please call 800.456.CLUB(2582) for further assistance. <a onclick=\"newWin=window.open('" + CustomHtmlHelpers.GetParentSitePath(null) + "/owner/helpWinHours.aspx','newWin1','toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=239,height=450,left=50,top=50'); return false;\" href=\"#\">Click here</a> for hours of operation.";
                            }

                        }
                    }
                    else
                    {
                        //Implemented as part of TFS# 1241:BGO- Need error message change  : Actual change of Error Should be made in webService
                        var serverResponseError = unitResponse == null ? "The resorts service returned a null response" : unitResponse.Messages[0].Description;
                        var errorSpecificMessage = "An error happened, please try it again or contact the IT Dept.".Replace(" ", "");

                        if (unitResponse == null || (serverResponseError.Replace(" ", "").Contains(errorSpecificMessage)))
                        {
                            lblMEssage = "We’re sorry, an error has occurred.  Please wait a few minutes and try your request again.";
                        }
                        else
                        {
                            lblMEssage = unitResponse.Messages[0].Description + " (W)";
                        }

                    }

                }
                catch (Exception ex)
                {
                    lblMEssage = "Sorry, We are experiencing technicals issues. Please, try it again in a few minutes.";
                    Session["BookingScreeningResponse"] = null;
                }

            }
            catch (Exception ex)
            {
                lblMEssage = "Sorry, We are experiencing technicals issues. Please, try it again in a few minutes.(w)";
            }

            //If savePointsFlag Then
            //    lblMEssage = "SPERROR-" & speCost.ToString()
            //End If

            if (BXGOwner.User[0].HomeProject == "52")
            {
                if (unitResponse.BookStatus == BGO.ResortsService.BookStatusType.CannotBeDone)
                {
                    savePointsFlag = false;
                    lblMEssage = "You are not eligible";
                }
                else
                {
                    savePointsFlag = false;
                    lblMEssage = "";

                }
            }
            else
            {
                if (savePointsFlag & reservationsTotal >= 10)
                {
                    savePointsFlag = false;
                    lblMEssage = "The reservation you are attempting to book exceeds the limit of 10 pending reservations in the same destination at any given time per owner.  Please select a different destination or cancel other reservations pending for this destination to proceed.";

                }
                else if (savePointsFlag & fspe)
                {
                    string errorMessage = "<br/><div align='left' class='roundedPanel' style='margin-bottom: 5px; border-radius: 10px; border: 0px none;margin-bottom: 20px;padding-bottom: 10px;'><font style='color:black;left: 10px;color: black; position: relative;'>This reservation will use Points from your next use year and will be reflected in your Future Points balance.</font><br/></div><br/>";
                    lblMEssage = "SPERROR-fspe" + errorMessage;
                }
                else if (savePointsFlag & reservationsTotal < 10)
                {
                    lblMEssage = "SPERROR-" + resinfo.SPECost;
                }
                else if (savePointsFlag == false & reservationsTotal >= 10)
                {
                    lblMEssage = "The reservation you are attempting to book exceeds the limit of 10 pending reservations in the same destination at any given time per owner.  Please select a different destination or cancel other reservations pending for this destination to proceed.";
                }
            }


            if (lblMEssage.Trim().Length > 2)
            {
                resinfo.success = 0;
                resinfo.errorMessage = lblMEssage;
            }
            else
            {
                resinfo.success = 1;
                resinfo.errorMessage = "";
            }

            return resinfo;

        }

        protected static string StringEncode(string value, string key)
        {
            System.Security.Cryptography.MACTripleDES mac3des = new System.Security.Cryptography.MACTripleDES();
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));

            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value)) + '-' + Convert.ToBase64String(mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));

        }

        private static List<ReservationDailyRates> GetReservationDailyRates(DateTime CheckInDate, DateTime CheckOutDate, string ProjectNumber, string UnitType)
        {
	        List<ReservationDailyRates> dailyRates = new List<ReservationDailyRates>();


	        BGO.RatesService.RatesServiceSoapClient client = new BGO.RatesService.RatesServiceSoapClient();
	        BGO.RatesService.DailyRatesTwoWeeks[] ratesDailyResult = null;
	        BGO.RatesService.RatesDailySearchCriteria ratesDailySearchCriteria = new BGO.RatesService.RatesDailySearchCriteria();
	        int rowCount = 0;
	        ratesDailySearchCriteria.CheckinDate = CheckInDate.ToShortDateString();
	        ratesDailySearchCriteria.ResortID = ProjectNumber;
	        ratesDailySearchCriteria.UnitType = UnitType;
	        ratesDailySearchCriteria.WeekNumber = 0;

	        ratesDailyResult = client.GetDailyRates(ratesDailySearchCriteria);


	        foreach ( var ratesDailyItem in ratesDailyResult) {
		        if (rowCount == 14 & DateTime.Parse(ratesDailyItem.CalendarDate) > CheckOutDate.AddDays(-1)) {
			        break; // TODO: might not be correct. Was : Exit For
		        }

		        ReservationDailyRates rate = new ReservationDailyRates();

		        rate.DailyRate = ratesDailyItem.UnitRate;
		        rate.DayName = ratesDailyItem.DayName + "  " + ratesDailyItem.CalendarDate.Trim().Substring(0, ratesDailyItem.CalendarDate.Trim().Length - 5);
		        rate.SeasonName = ratesDailyItem.SeasonName;

		        if (Convert.ToDateTime(ratesDailyItem.CalendarDate) >= CheckInDate & Convert.ToDateTime(ratesDailyItem.CalendarDate) <= CheckOutDate.AddDays(-1)) {
			        rate.DaySelected = true;
		        }

		        dailyRates.Add(rate);
		        rowCount = rowCount + 1;
	        }

	        return dailyRates;


        }

        public static List<OwnerPointsStatus> GetOwnerPoints(PointsEligibilityDesc[] PointsEligibility, System.DateTime checkinDate, ref int elgPoints)
        {

            List<OwnerPointsStatus> Accounts = new List<OwnerPointsStatus>();

            //PointsEligibility = PointsEligibility.Where(Function(x) CType(x.ExpirationDate, Date) > CType(checkinDate.ToString("MM/dd/yy"), Date)).ToArray()

            PointsEligibility = PointsEligibility.ToArray();


            if (PointsEligibility.Length > 0)
            {
                for (int count = 0; count <= PointsEligibility.Length - 1; count++)
                {
                    OwnerPointsStatus account = new OwnerPointsStatus();
                    DateTime strExpireDate = default(DateTime);
                    strExpireDate = DateTime.ParseExact(PointsEligibility[count].ExpirationDate.Trim(), "MM/dd/yy", CultureInfo.InvariantCulture);
                    account.PointsType = PointsTypeDesc(PointsEligibility[count].PointsType.Trim());
                    account.Points = string.Format("{0:#,#}", Convert.ToInt32(PointsEligibility[count].PointsAvailable));
                    account.ExpDate = string.Format("{0:MM/dd/yyyy}", strExpireDate);
                    account.EligibilityMsg = PointsEligibility[count].EligibleMessage.Trim();
                    account.IsEligible = PointsEligibility[count].IsEligible.ToString();
                    if ((account.EligibilityMsg.IndexOf("Eligible") != -1))
                    {
                        elgPoints = elgPoints + SafeParser.ParseInt(account.Points);
                        account.IsEligible = "True";
                    }

                    Accounts.Add(account);

                }
            }
            return Accounts;

        }

        private static string PointsTypeDesc(string pointsType)
        {
            string pointsDesc = "";

            switch (pointsType)
            {
                case "A":
                    pointsDesc = "Annual";
                    break;
                case "B":
                    pointsDesc = "Borrowed";
                    break;
                case "X":
                    pointsDesc = "Additional Points";
                    break;
                case "F":
                    pointsDesc = "AIM Points";
                    break;
                case "H":
                    pointsDesc = "Anniversary";
                    break;
                case "J":
                    pointsDesc = "New Sale Incentive";
                    break;
                case "K":
                    pointsDesc = "CustomerSatisfactionCert";
                    break;
                case "S":
                    pointsDesc = "Saved";
                    break;
                case "M":
                    pointsDesc = "Conversion Certificate";
                    break;
                case "T":
                    pointsDesc = "Biennial Points";
                    break;
                case "U":
                    pointsDesc = "Biennial Borrowed";
                    break;
                case "R":
                    pointsDesc = "Rented";
                    break;
                default:

                    break;
            }

            return pointsDesc;

        }

        private int GetReservationsCount(string proj)
        {
            int reservationsCount = 0;
            try
            {
                // get reservations in datatable
                DataTable dtReservations = new DataTable();
                DataTable dtRegions = new DataTable();
                // dtReservations = GetDataTableFromObjects(histories)
                dtReservations = GetFutureReservations();
                dtRegions = GetGeoRegion();

                // getGeoRegion for project
                var regions = (from regionRow in dtRegions.AsEnumerable()
                               where ((string)regionRow["AS400ProjNo"]) == proj
                               select new { Region = regionRow["GeographicRegion"] }).ToList();

                var query = (from t1 in dtReservations.AsEnumerable()
                             join t2 in dtRegions.AsEnumerable()
                             on t1["_ProjectStay"].ToString() equals t2["AS400ProjNo"].ToString()
                             where t2["GeographicRegion"].ToString() == regions[0].Region.ToString()
                             select new
                             {
                                 Proj = t1["_ProjectStay"],
                                 ResNumber = t1["_ReservationNumber"],
                                 Region = t2["GeographicRegion"]
                             }).ToList();
                reservationsCount = query.Count;
            }
            catch (Exception ex)
            {
                //reservationsCount = 0;
                //StringBuilder errMsg = new StringBuilder();
                //errMsg.Append("Error happened fetching Reservations List");
                //errMsg.Append(ex.Message);
                //sendMessage("OLPSupport@bxgcorp.com", "", "Exception Error - Getting Reservation List", errMsg);
            }

            return reservationsCount;
        }

        public static DataTable GetGeoRegion()
        {
            DataTable dtGeo = new DataTable();

            BGO.clsDBConnectivity conn = new BGO.clsDBConnectivity();
            SqlDataAdapter objDataAdapter = default(SqlDataAdapter);
            DataSet objDataSet = default(DataSet);


            try
            {
                conn.dbCmnd.CommandText = "uspSelectResortGeoRegion";
                conn.dbCmnd.CommandType = System.Data.CommandType.StoredProcedure;

                objDataAdapter = new SqlDataAdapter(conn.dbCmnd);
                objDataSet = new DataSet();
                objDataAdapter.Fill(objDataSet);

                dtGeo = objDataSet.Tables[0];

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn = null;
            }

            return dtGeo;
        }


        private DataTable GetFutureReservations()
        {
            DataTable dtReservations = new DataTable();
            Owner BXGOwner = (Owner)Session["BXGOwner"];
            BGO.ResortsService.ReservationHistory history = new BGO.ResortsService.ReservationHistory();
            BGO.ResortsService.ReservationHistoryList historyResult = new BGO.ResortsService.ReservationHistoryList();
            BGO.ResortsService.ResortsServiceClient reservationAS400 = new BGO.ResortsService.ResortsServiceClient();
            BGO.ResortsService.ReservationHistoryItem[] histories = null;
            BGO.ResortsService.OwnerID owner = new BGO.ResortsService.OwnerID();

            history.OwnerID = null;
            owner.OwnerVacationNumber = BXGOwner.Arvact;
            history.OwnerID = owner;
            history.SiteName = Sites.OnlinePoints;
            history.EffectiveDate = System.DateTime.Today;
            history.SearchHistoryBy = ReservationHistoryType.Future;


            try
            {
                historyResult = reservationAS400.GetReservationsHistory(history);

                if (historyResult.Success)
                {
                    histories = historyResult.ReservationHistoryItem;
                }

                if (reservationAS400.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    reservationAS400.Close();
                }

                dtReservations = GetDataTableFromObjects(histories);


            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (reservationAS400.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    reservationAS400.Close();
                }

            }


            return dtReservations;
        }

        public static DataTable GetDataTableFromObjects(object[] objects)
        {
            if (objects != null && objects.Length > 0)
            {
                Type t = objects[0].GetType();
                DataTable dt = new DataTable(t.Name);
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }


                foreach (BGO.ResortsService.ReservationHistoryItem o in objects)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }
            return null;
        }

        private string substrfunction(string str, string type)
        {
            return str.Substring(str.IndexOf('=') + 1);
        }

        #endregion

        /// <summary>
        /// Returns a hash set of destinations in which at least one resort allows online bookings of the specified reservation type
        /// </summary>
        /// <typeparam name="T">The type returned by getKey</typeparam>
        /// <param name="resType">The reservation type to filter on</param>
        /// <param name="getKey">A function that maps the individual resort to the key to insert into the resulting HashSet</param>
        /// <returns></returns>
        private HashSet<T> GetDestinationsSetByReservationType<T>(ReservationType resType, Func<IPublishedContent, T> getKey)
        {
            var checkProperty = resType == ReservationType.BonusTime ? "allowbtbooking" : "allowptsbooking";
            var destinations = new HashSet<T>();
            int? ourResortsContentId = WebConfigContentId.GetValue("ourResortsContentId");
            if (ourResortsContentId.HasValue)
            {
                var ourResorts = Umbraco.TypedContent(ourResortsContentId.Value).Children.ToList();
                ourResorts
                    .Where(x => x.GetPropertyValue<bool>(checkProperty))
                    .Select(x => getKey(x))
                    .Distinct()
                    .ToList()
                    .ForEach(x => { destinations.Add(x); });
            }

            return destinations;
        }

        /// <summary>
        /// Returns a hash set of destinations in which at least one resort allows online bookings of the specified reservation type
        /// </summary>
        /// <typeparam name="T">The type returned by getKey</typeparam>
        /// <param name="resType">The reservation type to filter on</param>
        /// <param name="getKey">A function that maps the individual resort to the key to insert into the resulting HashSet</param>
        /// <returns></returns>
        private Dictionary<int, T> GetResortsByReservationType<T>(ReservationType resType, Func<IPublishedContent, T> getValue)
        {
            var checkProperty = resType == ReservationType.BonusTime ? "allowbtbooking" : "allowptsbooking";
            var destinations = new Dictionary<int, T>();
            int? ourResortsContentId = WebConfigContentId.GetValue("ourResortsContentId");
            if (ourResortsContentId.HasValue)
            {
                var ourResorts = Umbraco.TypedContent(ourResortsContentId.Value).Children.ToList();
                ourResorts
                    .Where(x => x.GetPropertyValue<bool>(checkProperty))
                    .Select(x => new {
                        ID = x.GetPropertyValue<int>("DatabaseID"),
                        Value = getValue(x)
                    })
                    .Distinct()
                    .ToList()
                    .ForEach(x => { destinations[x.ID] = x.Value; });
            }

            return destinations;
        }

        public void NotifyError(PlanItSearchModel model, Exception ex, string detailMessage = "", bool fail = true)
        {
            var exceptionDetail = string.Empty;
            if (ex != null)
            {
                exceptionDetail += ex.ToString() + "\n" + ex.StackTrace;
            }
            model.ValidationSummary.ExceptionDetail.Add(exceptionDetail);

#if DEBUG
            var message = new StringBuilder();
            message.Append(detailMessage);
            message.Append("<br/>\n");
            message.Append(exceptionDetail.Replace("\n", "<br/>\n").Replace("\r", ""));
            model.ValidationSummary.Add(message.ToString(), fail);
#else
            model.ValidationSummary.Add("We’re sorry, an error has occurred. Please wait a few minutes and try your request again.", fail);
#endif

            // TODO: Log detailMessage somewhere
        }
    }
}
