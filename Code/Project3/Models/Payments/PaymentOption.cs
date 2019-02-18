using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BGSitecore.Utils;
using BGSitecore.Models.Interface;
using System.Reflection;

namespace BGSitecore.Models.Payments
{
    public class PaymentOptions : BaseComponent
    {
        [SitecoreIgnore]
        public virtual AccountViewModel AccountInfo { get; set; }

        [SitecoreChildren]
        public virtual IEnumerable<PaymentOption> AllPaymentOptions { get; set; }

        //[SitecoreChildren]
        //public virtual CreditOrDebitCard CreditOrDebitCard { get; set; }


    }


    //public static class PaymentType
    //{
    //    public const string CreditCard = "CC";
    //    public const string CurrentAndSaving = "SA";
    //    public const string InstallmentPlan = "IP";
    //    public const string Mail = "MA";
    //    public const string Telephone = "TEL";
    //}

    /// <summary>
    /// this class is to be removed
    /// </summary>
    public class PaymentOptionSubmit
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }



    public class PaymentOption : Lookup, IPaymentOption
    {
        private const string IsDefaultFieldID = "{633508A7-BAB8-435D-9A14-2EAE9A3F9A94}";
        private const string ModelTypeFieldID = "{811DF34A-E440-4BD1-BC20-E053782C4339}";
        private const string ViewPathFieldID = "{8F4DD85A-B779-40AA-911E-350C3189AB87}";
        private const string ItemSourceFieldID = "{21FFAADE-5848-4D1A-B51C-44ED26FDB483}";

        public PaymentOption()
        {
            //PaymentOptionSection section = ConfigurationManager.GetSection("PaymentOptionSection") as PaymentOptionSection;
            //var config = section.Options[Code];
            //ViewName = config.ViewName;
            //ModelType = config.Model;
            //ItemID = config.Model;
        }
        //[SitecoreIgnore]
        //public IPaymentOption OptionDetails { get; private set; }

        //[SitecoreIgnore]
        [SitecoreField(ViewPathFieldID)]
        public string ViewName { get; set; }

        [SitecoreField(IsDefaultFieldID)]
        public bool IsDefault { get; set; }

        [SitecoreField(ModelTypeFieldID)]
        public string ModelType { get; set; }

        [SitecoreField(ItemSourceFieldID)]
        public string ItemID { get; set; }

        public object GetModal(AccountViewModel accountInfo)
        {
            try
            {
                MethodInfo method = typeof(SitecoreUtils).GetMethods().FirstOrDefault(m=>m.Name== "GetItem" && m.IsGenericMethod);
                Type type = Type.GetType(ModelType);
                var typeClass = Activator.CreateInstance(type);
                MethodInfo genericMethod = method.MakeGenericMethod(type);
                var returnType = !string.IsNullOrEmpty(ItemID) ? genericMethod.Invoke(null, new object[] { ItemID }) : typeClass;
                ((IPayment)returnType).Amount = accountInfo != null && accountInfo.UserPayInfoList != null && accountInfo.UserPayInfoList.Count > 0 ? accountInfo.UserPayInfoList.Sum(x => x.PaymentAmount) : 0;
                ((IPayment)returnType).AmountString = ((IPayment)returnType).Amount.ToString("N", Sitecore.Context.Language.CultureInfo);
                return returnType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool IsDisplay(AccountViewModel accountInfo)
        {
            switch (Code)
            {
                case "CC":
                case "ML": return true;
                case "CS": return accountInfo != null && accountInfo.GroupID == 50;
                case "IP":
                    return accountInfo != null && accountInfo.GroupID == 50 && accountInfo.TotalDues <= 0 && accountInfo.UserPayInfoList != null
                         && accountInfo.UserPayInfoList.Count() > 0 && accountInfo.UserPayInfoList.Where(x => x.SalesType == "A" || x.SalesType == "U").Count() > 0;
                default: return false;
            }
        }
    }

    //public class CreditCardOption : IPaymentOption
    //{
    //    public string ViewName
    //    {
    //        get;

    //    }

    //    public IPayment GetModal()
    //    {
    //        return SitecoreUtils.GetItem<CreditCard>(string.Empty);
    //    }

    //}

    //public class CurrentAndSavingOption : IPaymentOption
    //{
    //    public string ViewName
    //    {
    //        get
    //        {
    //            return "";
    //        }
    //    }

    //    public IPayment GetModal()
    //    {
    //        return SitecoreUtils.GetItem<CurrentSaving>(string.Empty);
    //    }
    //}

    //public class InstallmentPlanOption : IPaymentOption
    //{
    //    public string ViewName
    //    {
    //        get
    //        {
    //            return "";
    //        }
    //    }

    //    public IPayment GetModal()
    //    {
    //        return SitecoreUtils.GetItem<Installments>(string.Empty);
    //    }
    //}
}

