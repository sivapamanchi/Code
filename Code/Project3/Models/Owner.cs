using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSiteCore.Models
{

    public class Owner
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OwnerId { get; set; }
        public string Email { get; set; }
    }
}