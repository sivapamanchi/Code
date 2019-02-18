using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Collections;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BGSitecore.Models
{
    public class State
    {
        private const string STATE_GUID = "{5D0A8047-2431-48F8-AA75-060BD75DAB72}";
        private const string STATE_POSSESSIONS_GUID = "{A6BBDBDC-B900-4077-9865-40CA5B4FBDDB}";

        public const string CodeId = "{DB3425E8-745D-4BF4-A184-FB9DED7B2703}";
        [SitecoreField(FieldName = State.CodeId)]
        public virtual string Code { get; set; }

        public const string DisplayNameId = "{B57A28C2-1E21-46F3-901E-A0192344659D}";
        [SitecoreField(FieldName = State.DisplayNameId)]
        public virtual string DisplayName { get; set; }

        public const string CountryId = "{6FEB222E-2207-459E-9EEA-9444BBA37F51}";
        [SitecoreField(FieldName = State.CountryId)]
        public virtual string Country { get; set; }

        /// <summary>
        /// Builds the relationship dropdown
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> BuildStateDropdown()
        {
            return State.BuildDropdown(SitecoreUtils.GetItem(STATE_GUID));
        }

        /// <summary>
        /// Builds the Profile dropdown which includes USA and Province states/Military
        /// </summary>
        /// <returns>Selceted List items</returns>
        public static List<SelectListItem> BuildProfileStateDropdown()
        {
            var usaStates = BuildStateDropdown();
            var usaPossessions = BuildUsaPossessionsDropdown();
            var allStates = new List<SelectListItem>(usaStates.Count +
                                    usaPossessions.Count);
            allStates.AddRange(usaStates);
            allStates.AddRange(usaPossessions);

            return allStates.OrderBy(x=>x.Value).Distinct().ToList();
        }

        /// <summary>
        /// Builds the dropwodn for USA Province states and Military
        /// </summary>
        /// <returns>Selceted List items</returns>
        public static List<SelectListItem> BuildUsaPossessionsDropdown()
        {
            return BuildDropdown(SitecoreUtils.GetItem(STATE_POSSESSIONS_GUID));
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
                    string code = (string)item[CodeId];
                    string displayName = (string)item[DisplayNameId];
                    result.Add(new SelectListItem() { Value = code, Text = displayName });
                }
            }

            return result;
        }

    }
}