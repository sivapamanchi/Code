using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using BGSitecore.Models.Common;
using Glass.Mapper.Sc.Fields;

namespace BGSitecore.Models
{
    [SitecoreType(AutoMap = true)]
    
    public class ContactUs
    {
        public const string ContactUsTextId = "{7B869EEA-3287-410D-A1F4-88F23EFD886E}";
        public const string ContactUsLinkId = "{5BE66728-9BA9-4748-BA3E-E95E92EFA85E}";
        public const string ContactUsTitleId = "{6B410EB9-A999-4433-864E-1F2D3161EC7C}";

        [SitecoreField(FieldName = ContactUsTextId)]
        public virtual string ContactUsText { get; set; }

        [SitecoreField(FieldName = ContactUsLinkId)]
        public virtual string ContactUsLink { get; set; }

        [SitecoreField(FieldName = ContactUsTitleId)]
        public virtual string Title { get; set; }
    }

    [SitecoreType(AutoMap = true)]
    public class ContactUsViewModel :BasePage
    {
        public const string FormPostUrlId = "{A286CB37-83C6-4EAF-8966-780758793266}";
        public const string FormTitleId = "{88ECD62B-B569-4217-8E35-CF02391A9E2D}";
        public const string FirstNameLabelId = "{DEAD9997-9CA2-437B-B862-485D7F00F9C1}";
        public const string LastNameLabelId = "{6457FCD4-9ED5-4586-A349-44B07DCAA402}";
        public const string EmailLabelId = "{6423D175-736E-42F6-A471-59DA066A82CE}";
        public const string PhNumLabelId = "{1F3C5BB0-111F-41FC-B61A-1DC18C39AAC6}";
        public const string ContactLabelId = "{687C325C-AC80-4655-9393-7B47877F6E82}";
        public const string QuestionLabelId = "{3A884225-4FBD-421B-BB45-9211A738C97C}";
        public const string FooterNotesId = "{688C1508-0D72-423A-B21A-EFEE2B46BF64}";
        public const string MethodofContactId = "{464DE46F-7447-4DEC-9025-77F47A6B0A25}";
        public const string ReturnUrlId = "{93364E7A-ADDF-48EF-A2D4-12CD1F3CC118}";

        [SitecoreField(FieldName = FormPostUrlId)]
        public virtual Link FormPostUrl { get; set; }

        [SitecoreField(FieldName = FormTitleId)]
        public virtual string FormTitle { get; set; }

        [SitecoreField(FieldName = FirstNameLabelId)]
        public virtual string FirstNameLabel { get; set; }

        [SitecoreField(FieldName = LastNameLabelId)]
        public virtual string LastNameLabel { get; set; }

        [SitecoreField(FieldName = EmailLabelId)]
        public virtual string EmailLabel { get; set; }

        [SitecoreField(FieldName = PhNumLabelId)]
        public virtual string PhNumLabel { get; set; }

        [SitecoreField(FieldName = ContactLabelId)]
        public virtual string ContactLabel { get; set; }

        [SitecoreField(FieldName = QuestionLabelId)]
        public virtual string QuestionLabel { get; set; }

        [SitecoreField(FieldName = FooterNotesId)]
        public virtual string FooterNotes { get; set; }

        [SitecoreField(FieldName = MethodofContactId)]
        public virtual string MethodofContact { get; set; }

        [SitecoreField(FieldName = ReturnUrlId)]
        public virtual Link ReturnUrl { get; set; }

        public string[] GetOptionsList()
        {
            string[] result = null;
            if (!string.IsNullOrEmpty(MethodofContact))
            {
                result = MethodofContact.Split(',');
            }
            return result;
        }
    }
    
}