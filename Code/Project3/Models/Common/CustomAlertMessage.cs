using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Common
{
    [Serializable]
    public class CustomAlertMessage
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public AlertMessageTypes Type { get; set; }
    }
}