using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class PointsWeekDetails
    {
        public const string TitleId = "{D37F1824-6961-4172-81A2-CE88129DBC37}";
        public const string CssClassId = "{DE451E79-D898-45F8-ACE7-0B450E369316}";
        public const string MondayId = "{DD955A2B-6330-4592-B36A-7CBDFE3D3C37}";
        public const string TuesdayId = "{CC6E30B6-1300-4A2D-BB4D-6049D62DB5E5}";
        public const string WednesdayId = "{D91452EF-118F-4EA5-B5BA-763149728DBD}";
        public const string ThursdayId = "{D980CC5C-2312-4491-8EA3-4B2782264131}";
        public const string FridayId = "{1E47679C-3AEE-4E15-8281-CF32CDDC9684}";
        public const string SaturdayId = "{04FD96CD-8E23-4F82-8C0E-7BC7535A51D8}";
        public const string SundayId = "{58F522F4-97A9-4AEF-A9E2-F06E648098F4}";
        public const string DisplayNameId = "{C643CCD2-31CF-492D-B23F-CAFFB355FB54}";

        [SitecoreId]
        public virtual Guid ID { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreInfo(SitecoreInfoType.FullPath)]
        public virtual string FullPath { get; set; }


        [SitecoreField(FieldName = TitleId)]
        public virtual string Title { get; set; }



        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        [SitecoreField(FieldName = CssClassId)]
        public virtual string CssClass { get; set; }

        [SitecoreField(FieldName = MondayId)]
        public virtual string Monday { get; set; }

        [SitecoreField(FieldName = TuesdayId)]
        public virtual string Tuesday { get; set; }

        [SitecoreField(FieldName = WednesdayId)]
        public virtual string Wednesday { get; set; }

        [SitecoreField(FieldName = ThursdayId)]
        public virtual string Thursday { get; set; }

        [SitecoreField(FieldName = FridayId)]
        public virtual string Friday { get; set; }

        [SitecoreField(FieldName = SaturdayId)]
        public virtual string Saturday { get; set; }

        [SitecoreField(FieldName = SundayId)]
        public virtual string Sunday { get; set; }


    }
}