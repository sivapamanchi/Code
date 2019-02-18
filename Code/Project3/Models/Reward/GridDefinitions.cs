using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models.Reward
{
   
    [SitecoreType]
    public class GridRowData
    {
        public const string ItemTitleId = "{56BDBE83-7D7E-43E0-902B-D7F184E6BBFE}";
        public const string ItemDescId = "{8A0F71E5-9ABE-47D8-A5EC-B18D1CDA3E04}";
        public const string ItemValueId = "{CF9B9DD9-C1F0-46CC-831E-F5C6B367BD8F}";
        public const string IsMorInfoId = "{CC20AA0D-D22E-4735-80A2-ADD968BD3AD0}";
        public const string MoreInfoTitleId = "{28EC8101-5745-4C0E-A77D-17710966E6F9}";
        public const string AlertInfoId = "{8AAB161A-879B-4F77-A9B0-BF66BCEF6864}";
        public const string UniqueRowId = "{832E7A4A-10E2-4ACD-8D4D-0739D2E784BE}";
        public const string QuantityColumnRequiredId = "{C3C36053-BBE5-49D0-9411-E324311356A1}";
        public const string TransactionTypeId = "{D5701547-7DDA-4C57-916C-AA898A04895C}";

        [SitecoreId]
        public virtual Guid GiftCardId { get; set; }

        [SitecoreField(FieldName = ItemTitleId)]
        public virtual string ItemTitle { get; set; }

        [SitecoreField(FieldName = ItemDescId)]
        public virtual string ItemDesc { get; set; }

        [SitecoreField(FieldName = ItemValueId)]
        public virtual string ItemValue { get; set; }

        [SitecoreField(FieldName = IsMorInfoId)]
        public virtual bool IsMoreInfo { get; set; }

        [SitecoreField(FieldName = MoreInfoTitleId)]
        public virtual string MoreInfoTitle { get; set; }

        [SitecoreField(FieldName = AlertInfoId)]
        public virtual string ItemAlertInfo { get; set; }

        [SitecoreField(FieldName = UniqueRowId)]
        public virtual string UniqueId { get; set; }

        [SitecoreField(FieldName = QuantityColumnRequiredId)]
        public virtual bool QuantityColumnRequired { get; set; }

        [SitecoreField(FieldName = TransactionTypeId)]
        public virtual string TransactionType { get; set; }
    }

    [SitecoreType]
    public class HeaderColumnList
    {
        public const string ColumnListId = "{ADDD3AA2-9B9E-46BE-A9D4-D606DF8DA210}";

        [SitecoreField(FieldName = ColumnListId)]
        public virtual string ColumnList { get; set; }

        public string[] GetColumnList()
        {
            string[] result = null;
            if (this.ColumnList != null)
            {
                result = ColumnList.Split('\n');
            }
            return result;
        }

    }
}