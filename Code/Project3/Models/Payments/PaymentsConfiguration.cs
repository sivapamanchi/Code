using BGSitecore.Models.Common;
using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models.Payments
{
    public class PaymentsConfiguration
    {
        public const string PaymentsConfigurationItem = "{34FFD2DD-7EA1-4F7C-AAA6-C926D24E97E8}";
        public const string PaymentDueContentItem = "{BE5EFF2D-3951-4DBF-A048-9E546EB7F055}";
        public const string PrePaymentContentItem = "{1CB2528F-1D0E-4E77-A32E-455310AC3C25}";
        public const string AccountsHelpContentFolderItem = "{2ED0A486-DDCB-4657-B8C3-25C8408D1A66}";
        public const string PayInstallmentsPageId = "{E7FB90F6-01EA-4A29-B3A2-62CA1ACEAC6C}";
        public const string AccountStatusPageId = "{17A91EDA-6977-4214-83A2-21C8AF335581}";
        public const string PaymentReminderPageId = "{36D56031-D5F1-4B84-AE53-007EC855B518}";
        public const string MortgageSummaryPageId = "{9D9031B1-2080-4C89-A413-F8B9B917FB0E}";
        public const string PaymentsOptionPageId = "{C5A9978E-AAB0-4B44-89B1-3D0723F42994}";
        public const string InstallmentPlanPageId = "{D9FA7A85-9C10-4CC9-BCEA-0E78E3D1D5BF}";
        


    [SitecoreField(FieldName = PaymentDueContentItem)]
        public virtual Wysiwyg PaymentDueContent { get; set; }

        [SitecoreField(FieldName = PrePaymentContentItem)]
        public virtual Wysiwyg PrePaymentContent { get; set; }

        [SitecoreField(FieldName = AccountsHelpContentFolderItem)]
        public virtual FolderItems<AccountHelpContent> AccountsHelpContentFolderPath { get; set; }

        [SitecoreField(FieldName = PayInstallmentsPageId)]
        public virtual Link PayInstallmentsPage { get; set; }

        [SitecoreField(FieldName = AccountStatusPageId)]
        public virtual Link AccountStatusPage { get; set; }

        [SitecoreField(FieldName = PaymentReminderPageId)]
        public virtual Link PaymentReminderPage { get; set; }

        [SitecoreField(FieldName = MortgageSummaryPageId)]
        public virtual Link MortgageSummaryPage { get; set; }
        [SitecoreField(FieldName = PaymentsOptionPageId)]
        public virtual Link PaymentsOptionPage { get; set; }

        [SitecoreField(FieldName = InstallmentPlanPageId)]
        public virtual Link InstallmentPlanPage { get; set; }
    }
}