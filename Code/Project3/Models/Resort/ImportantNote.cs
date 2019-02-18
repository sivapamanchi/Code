using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

using Sitecore.Data.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Resort
{

    [SitecoreType(AutoMap = true)]
    public class ImportantNote
    {

        public const string SubHeaderId = "{A7E5A468-1DE8-4C28-8653-E72683E53840}";
        public const string ShowInSummaryId = "{E7F6643D-C133-4FFB-B8FC-22446E4B295B}";
        public const string ContentId = "{018F3ED8-D60C-4696-93C3-809621255C7E}";
        public const string ShowOnFirstColumnId = "{7094CC2E-FEEB-4256-A06E-041228DB98EA}";

        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreItem]
        public virtual Item InnerItem { get; set; }

        [SitecoreField(FieldName = "__Sortorder")]
        public virtual string SortOrder { get; set; }

        [SitecoreField(FieldName = SubHeaderId)]
        public virtual string SubHeader { get; set; }

        [SitecoreField(FieldName = ShowInSummaryId)]
        public virtual bool ShowInSummary { get; set; }

        [SitecoreField(FieldName = ShowOnFirstColumnId)]
        public virtual bool ShowOnFirstColumn { get; set; }

        [SitecoreField(FieldName = ContentId, Setting = SitecoreFieldSettings.RichTextRaw)]
        public virtual string NotesContent { get; set; }

    }
}