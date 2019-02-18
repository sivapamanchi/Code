using BGSitecore.Models.Resort;
using BGSitecore.Utils;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BGSitecore.Models.Resort
{
    public class RoomSize
    {
        public const string DisplayNameId = "{7EFD9ADB-7C23-4C1C-ADC0-386104AF766D}";
        public const string RoomSizeId = "{FC09C634-7770-4348-B18B-56DBB8AF0300}";
  
        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        [SitecoreField(FieldName = RoomSizeId)]
        public virtual string RoomSizeCode { get; set; }

    }
}