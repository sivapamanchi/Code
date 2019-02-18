using BGSitecore.Models.Common;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Payments
{
    [Serializable]
    public class AccountsListViewModel
    {
        [SitecoreIgnore]
        public IEnumerable<AccountViewModel> AccountList { get; set; }

        public Section SectionData { get; set; }
        public PaymentSection PaymentSectionData { get; set; }
        public bool IsARDATextAllowed { get; set; }
        public bool IsFooterNoteAllowed { get; set; }
        public CustomAlertMessage Alert { get; set; }

    }
}