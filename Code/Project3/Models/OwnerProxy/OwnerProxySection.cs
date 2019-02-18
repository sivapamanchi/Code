using BGSitecore.Models;
using BGSitecore.Models.Resort;
using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;

namespace BGSitecore.Models.OwnerProxy
{
    [SitecoreType(AutoMap = true)]
    public class OwnerProxySection : BasePage
    {
        public const string DisplaynameId = "{C6F0E993-487E-4E62-9EB4-B8CBD6960765}";
        public const string classnameId = "{484F459A-606B-4940-B03E-24F829DC970E}";
        public const string HideNameId = "{DDEC4E3E-2114-4BDE-BF19-1045D5CF8356}";
        public const string ShowJumpToSectionId = "{27A8E3DB-364C-476B-99A9-F7F9009FF0AB}";
        public const string HideSectionWhenPrintingId = "{65B55698-79D4-42DA-9D8F-96FC2A8143D0}";
        public const string ShowLineAfterSectionId = "{DCB072ED-7BB1-47DD-9C72-B1BDEF151B5F}";

        public const string JumpDisplayNameId = "{E99C9A16-6CBF-4AA1-A8E3-5C8199793772}";
        public const string JumpUniqueIdId = "{21C11358-3CF4-4AD4-B8B6-7D800D9DC905}";
        public const string JumpCaptionId = "{3D919BCB-1F4A-412C-B3B1-3723BABAFA0F}";

        public const string SectionDescriptionId = "{DDDB36F2-741F-4041-9A69-9F2CA1B2EC48}";

        public const string RestrictionRuleId = "{85B5A4F7-0974-4455-85FA-B33FFAF397E0}";

        [SitecoreField(FieldName = RestrictionRuleId)]
        public virtual string RestrictionRule { get; set; }

        [SitecoreField(FieldName = DisplaynameId)]
        public virtual string Displayname { get; set; }

        [SitecoreField(FieldName = classnameId)]
        public virtual string classname { get; set; }

        [SitecoreField(FieldName = HideNameId)]
        public virtual bool HideName { get; set; }

        [SitecoreField(FieldName = HideSectionWhenPrintingId)]
        public virtual bool HideSectionWhenPrinting { get; set; }

        [SitecoreField(FieldName = ShowJumpToSectionId)]
        public virtual bool ShowJumpToSection { get; set; }

        [SitecoreField(FieldName = ShowLineAfterSectionId)]
        public virtual bool ShowLineAfterSection { get; set; }

        [SitecoreField(FieldName = JumpDisplayNameId)]
        public virtual string JumpDisplayName { get; set; }

        [SitecoreField(FieldName = JumpCaptionId)]
        public virtual string JumpCaption { get; set; }

        [SitecoreField(FieldName = JumpUniqueIdId)]
        public virtual string JumpUniqueId { get; set; }

        [SitecoreField(FieldName = SectionDescriptionId)]
        public virtual string SliderGalleryDescription { get; set; }


        [SitecoreQuery("./*[@@templateid='{54DDFD1F-71AA-4AA7-91D6-80EA91DB1B79}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<RichText> AllRichText { get; set; }


        [SitecoreQuery("./*[@@templateid='{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}']", IsRelative = true, IsLazy = false)]
        public virtual Candidates Candidates { get; set; }

        public bool HideSection { get; set; }


        public string BuildJumpToLink()
        {
            return "jumpto-" + Displayname.ToLower().Replace(' ', '-');
        }
    }
    [SitecoreType]
    public class Candidates
    {

        [SitecoreQuery("./*[@@templateid='{1888FF19-C3A1-4BD9-8571-B16B61293DE2}']", IsRelative = true, IsLazy = false)]
        public virtual IEnumerable<Candidate> AllCandidates { get; set; }

       

    }

    [SitecoreType]
    public class Candidate
    {

        public const string FullNameId = "{05F27AFD-D862-4207-A94F-C2DABA098BA3}";
        public const string DisplayNameId = "{DD8A1DC2-E522-4E11-8506-CA9CBCF0514C}";
        public const string DescriptionId = "{A96A871A-C618-48D2-B0FE-F30173513FFA}";
        public const string VoteSubmissionId = "{8A91C1C6-BE76-4B51-ABDE-52053D9D8953}";
        public const string isCandidateId = "{0583A0EB-A093-4FD4-AF27-B0528AFD9AAF}";

        
        [SitecoreField(FieldName = FullNameId)]
        public virtual string FullName { get; set; }

        [SitecoreField(FieldName = DisplayNameId)]
        public virtual string DisplayName { get; set; }

        [SitecoreField(FieldName = VoteSubmissionId)]
        public virtual string VoteSubmissionValue { get; set; }

        [SitecoreField(FieldName = isCandidateId)]
        public virtual bool isCandidate { get; set; }

        [SitecoreField(FieldName = DescriptionId)]
        public virtual string Description { get; set; }

        [SitecoreIgnore]
        public bool isValidVotingPeriod { get; set; }
    }
    
  
}