using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class Section : LinkCollection
    {
        public const string NoDataMessageId = "{CB9A1C88-5868-4B7B-BB84-9164E0D6713F}";
        public const string NoDataHideFaceIconId = "{33DA0E6F-B223-4D9A-88FF-FAF1CB92D82A}";
        public const string NoDataPrimaryId = "{36C6A741-31A7-46F1-8C5D-57BA96CEF628}";
        public const string NoDataPrimaryUrlId = "{D6C6D35A-020E-4AC7-A41D-169CBA2D7F49}";
        public const string NoDataSecondaryId = "{0DC51DB5-21FE-4497-B95D-91B40B4490E7}";
        public const string NoDataSecondaryUrlId = "{29D0D60C-85B8-4EC1-80EF-B4FBC0351E04}";
        public const string ColumnListId = "{BFB57270-D4F5-43F9-86DD-54831230FDEF}";
        public const string InitialSortColumnId = "{F97D2742-BCB9-4E83-813C-3620FABCC58F}";
        public const string InitialSortDescendingId = "{5548E024-1064-404F-B2D2-09AA1D523257}";
        public const string RestrictColumnFromSortId = "{AE13707F-E04F-4BC7-AC2E-A4C507DDEF2A}";
        public const string HiddenColumnId = "{E8618B12-5BFC-4E1B-B136-D7656BD481B7}";
        public const string MaxItemsInTableId = "{6F9A20FB-CF7E-43C1-A31E-FE0A97265DA6}";
        public const string MaxItemsPerPageId = "{FDE37372-F1D8-4F18-84C7-F867F41496D3}";
        public const string PaginationId = "{DFB6C9E8-BA6D-45C0-A94C-886E71020773}";
        public const string LazyLoadContentId = "{20A947BA-F18A-49CC-ACE2-EFA88421E0CA}";

        [SitecoreField(FieldName = Section.NoDataMessageId)]
        public virtual string NoDataMessage { get; set; }

        [SitecoreField(FieldName = Section.NoDataHideFaceIconId)]
        public virtual bool NoDataHideFaceIcon { get; set; }

        [SitecoreField(FieldName = Section.NoDataPrimaryId)]
        public virtual string NoDataPrimary { get; set; }

        [SitecoreField(FieldName = Section.NoDataPrimaryUrlId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link NoDataPrimaryUrl { get; set; }

        [SitecoreField(FieldName = Section.NoDataSecondaryId)]
        public virtual string NoDataSecondary { get; set; }

        [SitecoreField(FieldName = Section.NoDataSecondaryUrlId, FieldType = SitecoreFieldType.GeneralLink)]
        public virtual Link NoDataSecondaryUrl { get; set; }

        [SitecoreField(FieldName = Section.ColumnListId)]
        public virtual string ColumnList { get; set; }

        [SitecoreField(FieldName = InitialSortColumnId)]
        public virtual int InitialSortColumn { get; set; }

        [SitecoreField(FieldName = InitialSortDescendingId)]
        public virtual bool InitialSortDescending { get; set; }

        [SitecoreField(FieldName = RestrictColumnFromSortId)]
        public virtual string RestrictColumnFromSort { get; set; }

        [SitecoreField(FieldName = HiddenColumnId)]
        public virtual int HiddenColumn { get; set; }

        [SitecoreField(FieldName = MaxItemsInTableId)]
        public virtual int MaxItemsInTable { get; set; }

        [SitecoreField(FieldName = MaxItemsPerPageId)]
        public virtual int MaxItemsPerPage { get; set; }

        [SitecoreField(FieldName = PaginationId)]
        public virtual bool Pagination { get; set; }

        [SitecoreField(FieldName = LazyLoadContentId)]
        public virtual bool LazyLoadContent { get; set; }

        public bool showSectionTitle { get; set; }
        public bool HideSection { get; set; }

        public virtual DataTable TableData { get; set; }

        public virtual DataTable TableDataSummary { get; set; }

        public string[] GetColumnList()
        {
            string[] result = null;
            if (this.ColumnList != null)
            {
                result = ColumnList.Split('\n');
            }
            return result;
        }

        public const string RichTextContentId = "{356158F4-D88D-4655-A500-72D3002B9D80}";
        [SitecoreField(FieldName = RichTextContentId)]
        public virtual string SuccessMessage { get; set; }
    }
}