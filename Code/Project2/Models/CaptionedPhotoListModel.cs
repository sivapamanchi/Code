using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Models;

namespace BGModern.Models
{
    public class CaptionedPhotoListModel 
    {
        public List<CaptionedPhotoModel> Photos { get; set; }

        public CaptionedPhotoListModel()
        {
            Photos = new List<CaptionedPhotoModel>();
        }
    }
}