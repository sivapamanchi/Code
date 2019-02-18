using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data;

namespace BGSitecore.Models.Reward
{
    //[SitecoreType]
    //public class GridLabels
    //{
    //    [SitecoreQuery("./*[@@templateid='{A04A513C-E74C-4FAC-A1E3-366B06FFE1BD}']", IsRelative = true, IsLazy = false)]
    //    public virtual IEnumerable<GridLabelText> GridLabelTitles { get; set; }


    //}

    [SitecoreType]
    public class Grid
    {
        [SitecoreId]
        public virtual ID ItemId { get; set; }

        [SitecoreQuery("./*[@@templateid='{DD218605-4205-405C-A514-0D2A188735CD}']", IsRelative = true, IsLazy = false)]
        public virtual HeaderColumnList GridHeaders { get; set; }

        [SitecoreQuery("./*[@@templateid='{57A1379F-1EFF-4F96-806C-4684A5E56056}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<GridRowData> GridRowsData { get; set; }


    }

    
}