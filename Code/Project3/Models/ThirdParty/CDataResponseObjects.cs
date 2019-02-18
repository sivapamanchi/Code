using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ThirdParty
{
    public class CDataResponseObjects
    {
        public class TaxRecordSearchCDataResponse
        {
            public string DataRecordID { get; set; } = string.Empty;
            public string Package_x0020_Code { get; set; } = string.Empty;
            public string RecipientFullName { get; set; } = string.Empty;
            public string AccountNumber { get; set; } = string.Empty;
            public string TaxYear { get; set; } = string.Empty;
            public string Filepath { get; set; } = string.Empty;
        }

        public class TaxRecordPreviewCDataResponse
        {
            public string DataRecordID { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string FilePath { get; set; } = string.Empty;
        }
    }
}