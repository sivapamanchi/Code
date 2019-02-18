using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGSitecore.Models
{
    public interface IWysiwyg
    {
        [SitecoreField(FieldName = "{0A83A6B3-E1DE-4949-82E2-B5105415C94F}")]
        string Title { get; set; }

        [SitecoreField(FieldName = "{7221BC8E-4F3A-4D03-B642-3BF814BC303B}")]
        string Content { get; set; }
    }
}
