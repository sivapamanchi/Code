using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;
using static BGSitecore.Validator.TranslatedValidator;

namespace BGSitecore.Models
{
    public class JsonResponse 
    {
        public string RetCode { get; set; }

        public List<string> errors { get; set; }
    }
}