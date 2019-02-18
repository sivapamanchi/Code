using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models
{
    public class SignInResponse
    {
        public enum errors
        {
            LockedAccount,
            SignInFail,
            InvalidPassword

        }

        public bool IsSuccessfull { get; set; }

        public errors errorCode { get; set; }
    }
}