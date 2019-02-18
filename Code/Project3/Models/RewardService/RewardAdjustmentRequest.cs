using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.RewardService
{
    public class RewardAdjustmentRequest
    {
        public int OwnerID { get; set; }
        public int BGRewardsOwnerID { get; set; }
        public Transaction Transaction { get; set; } = new Transaction();
        
        //TODO : unwanted request object for Reward adjustment
        //public LookUp Lookup { get; set; } = new LookUp();
    }
    
    public class Transaction
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string TransactionType { get; set; }
        public string StateCode { get; set; }
        public string ResortCode { get; set; }
        public string Comment { get; set; }
        public string PurchaseAmount { get; set; }

    }
    //public class LookUp
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string PostalCode { get; set; }
    //    public List<PhoneNumber> PhoneNumbers { get; set; }

    //}
    //public class PhoneNumber
    //{
    //    public string PhoneNbr { get; set; }

    //}
}