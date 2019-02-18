using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ThirdParty
{
    public class SendEmail
    {
    }
    public class SendEmailResponse
    {
        public string Success { get; set; }

        public List<Error> Errors { get; set; }

        public string Status { get; set; }

        public string TransactionID { get; set; }
    }
    public class SendEmailRequest
    {
        public string FolderName { get; set; }

        public string SupplymentTableName { get; set; }

        public string EventID { get; set; }

        public string EmailAddress { get; set; }

        public List<Keyvalue> KeyValues = new List<Keyvalue>();

        public List<PrimaryKeyModel> PrimaryKeys = new List<PrimaryKeyModel>();

        public class PrimaryKeyModel
        {
            public string PrimaryKey { get; set; }
        }
        public class Keyvalue
        {
            public string Key { get; set; }

            public string Value { get; set; }

        }
    }
    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }

}