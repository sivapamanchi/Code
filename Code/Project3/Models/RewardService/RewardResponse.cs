using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.RewardService
{
    public class RewardResponse
    {
        public string Identifier { get; set; }
        public Account Account { get; set; } = new Account();
        public List<Errors> Errors { get; set; } = new List<Errors>();
        public List<Transactions> Transactions { get; set; }
    }

    public class Account
    {
        public Memberships Memberships { get; set; } = new Memberships();
    }
    public class Memberships
    {
        public BluegreenRewardsMembership BluegreenRewardsMembership { get; set; } = new BluegreenRewardsMembership();
    }
    public class BluegreenRewardsMembership
    {
        public List<Transactions> Transactions { get; set; } = new List<Transactions>();

        public List<Returns> Returns { get; set; } = new List<Returns>();
    }

    public class Transactions
    {
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string AccountLevel { get; set; }
        public string ResortCode { get; set; }
        public TransactionType TransactionType { get; set; } = new TransactionType();
        public string UnitCount { get; set; }
    }
    public class TransactionType
    {
        public string TransactionTypeID { get; set; }
        public string Active { get; set; }
        public string Description { get; set; }
        public string BronzePoints { get; set; }
        public string GoldPoints { get; set; }
        public string PlatinumPoints { get; set; }
        public string SilverPoints { get; set; }
        public string StandardPoints { get; set; }
        public TransactionCategory TransactionCategory { get; set; } = new TransactionCategory();
       
    }
    public class TransactionCategory
    {
        public string TransactionCategoryID { get; set; }
        public string Description { get; set; }
    }
    public class Returns
    {
        public string RetCode { get; set; }
        public string CodeDesc { get; set; }
    }

    public class Errors
    {
        public string Code { get; set; }
        public string ShortText { get; set; }
    }
    
}