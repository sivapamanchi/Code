using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Common
{
    [Serializable]
    public class Error
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }
}