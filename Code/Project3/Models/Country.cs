using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BGSitecore.Models
{
    public class Country
    {
        private const string COUNTRY_GUID = "{DBE138C0-160F-4540-9868-0098E2CE8174}";

        public const string CodeId = "{4B6F46F0-ED6D-4920-A447-1DEE7586044D}";
        [SitecoreField(FieldName = CodeId)]
        public virtual string Code { get; set; }


        /// <summary>
        /// Builds the relationship dropdown
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> BuildCountryDropdown()
        {
            return BuildDropdown(SitecoreUtils.GetItem(COUNTRY_GUID));
        }

        /// <summary>
        /// Builds up a dropdown based on the lookup node parent
        /// </summary>
        /// <returns></returns>
        private static List<SelectListItem> BuildDropdown(Item root)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            if (root != null)
            {
                foreach (Item item in root.Children)
                {
                    string code = item[CodeId];
                    string displayName = item.Name;
                    result.Add(new SelectListItem() { Value = code, Text = displayName });
                }
            }
            return result;
        }

    }
}