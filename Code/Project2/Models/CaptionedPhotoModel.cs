using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGModern.Models
{
    public class CaptionedPhotoModel 
    {
        public String Caption { get; set; }
        public int PhotoID { get; set; }
        public String PhotoURL { get; set; }
    }
}