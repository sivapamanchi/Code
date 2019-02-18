using BGSitecore.Controllers;
using BGSitecore.Utils;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace BGSitecore.Models
{
    public class SavedSearch : SearchParameters
    {
        public string Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// SavedSearch constructor
        /// </summary>
        /// <param name="name"></param>
        public SavedSearch(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// SavedSearch constructor
        /// </summary>
        /// <param name="name"></param>
        public SavedSearch(NameValueCollection nvc)
        {
            
            this.Name = HttpContext.Current.Server.HtmlEncode(nvc["nm"]);
            this.Destination = HttpContext.Current.Server.HtmlEncode(nvc["ds"]);
            this.DestinationDisplayName = HttpContext.Current.Server.HtmlEncode(nvc["dsn"]);
            this.MonthSearch = SearchResultController.FormatMonthSearch(HttpContext.Current.Server.HtmlEncode(nvc["ms"]));
            this.monthsearchduration = HttpContext.Current.Server.HtmlEncode(nvc["du"]);
            
            this.ReservationType = HttpContext.Current.Server.HtmlEncode(nvc["rt"]);

            if (string.IsNullOrEmpty(this.Name)) throw new Exception("SaveSearch must have a name");
        }


        /// <summary>
        /// Retrives the name of the point type.
        /// </summary>
        /// <returns></returns>
        public string GetReservationTypeName()
        {
            string result = "Points Reservation";
            switch (this.ReservationType)
            {
                case "1":
                    result = "Points Reservation";
                    break;
                case "2":
                    result = "Bonus Time Reservation";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Builds the HTML link of the saved search 
        /// </summary>
        /// <returns></returns>
        public string ToHtmlLink()
        {
            return ToHtmlLink(false);
        }

        /// <summary>
        /// Builds the HTML link of the saved search 
        /// </summary>
        /// <returns></returns>
        public string ToHtmlLink(bool home)
        {
            StringBuilder result = new StringBuilder();

            bool include = (home) ? (this.CheckInDate >= DateTime.Now) : true; // home pages exclude expired searches
            if (include)
            {
                if (home) result.Append("<li>");
                result.Append("<a class='js-savedsearch' href='/owner/search-results' ");
                result.AppendFormat("data-id='{0}' ", this.Id);
                result.AppendFormat("data-rt='{0}' ", this.ReservationType);
                // result.AppendFormat("data-wc='{0}' ", this.WheelchairAccessible);
                //result.AppendFormat("data-ci='{0}' ", this.CheckInDate.ToShortDateString());
                //result.AppendFormat("data-co='{0}' ", this.CheckOutDate.ToShortDateString());
                result.AppendFormat("data-ms='{0}' ", this.MonthSearch);
                result.AppendFormat("data-du='{0}' ", this.monthsearchduration);
                result.AppendFormat("data-ds='{0}' ", this.Destination);
                result.AppendFormat(">{0}</a>", this.Name);
                if (home) result.Append("</li>");
            }
            return result.ToString();
        }

        /// <summary>
        /// Builds the HTML link of the saved search 
        /// </summary>
        /// <returns></returns>
        public string ToDeleteLink()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("<a href='javascript: void(0)' class='js-savedsearch-delete text-danger' data-name='{0}' title='Delete this saved search'><i class='fa fa-fw fa-lg fa-trash' aria-hidden='true'></i><span class='sr-only'>Delete this saved search</span></a>", this.Name);
            return result.ToString();
        }
    }
}