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
    public class Lookup : BaseComponent
    {
        private const string RELATIONSHIP_GUID = "{2A0864ED-4476-4CA5-B723-A1DE181C49C6}";


        public const string CodeId = "{4FF8ED96-D615-4C4C-A27F-18C95EFBFC3B}";
        [SitecoreField(FieldName = Lookup.CodeId)]
        public virtual string Code { get; set; }

        public const string DisplayNameId = "{38205BB2-CB2E-4062-88D3-C3390CC4943D}";
        [SitecoreField(FieldName = Lookup.DisplayNameId)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Builds the relationship dropdown
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> BuildRelationshipDropdown()
        {
            return Lookup.BuildDropdown(SitecoreUtils.GetItem(RELATIONSHIP_GUID));
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