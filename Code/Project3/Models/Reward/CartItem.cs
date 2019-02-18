using Glass.Mapper.Sc.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Reward
{
    [Serializable]
    public class CartItem
    {
        public virtual string Arvact { get; set; }

        public virtual string EncoreOwnerID { get; set; }

        public virtual string ItemID { get; set; }

        public virtual string ItemName { get; set; }

        public virtual int Quantity { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual decimal SubTotal { get; set; }

        public virtual int? NumberOfBedrooms { get; set; } = null;

        public virtual int? NumberOfNights { get; set; } = null;

        public virtual int? NumberOfAdults { get; set; } = null;

        public virtual int? NumberOfChildren { get; set; } = null;

        public virtual DateTime CheckInDate { get; set; } = Convert.ToDateTime("1/1/1753");
        public virtual DateTime CheckOutDate { get; set; } = Convert.ToDateTime("1/1/1753");

        public virtual string ModifiedBy { get; set; }
        
        public virtual string TransactionType { get; set; }
        
    }
    public class CartTotal
    {
        //Used only for Cart Total Component
        public virtual decimal AvailableRewards { get; set; }
        public virtual decimal TotalRewardsinCart { get; set; }
        public virtual decimal AvailablePoints { get; set; }
    }

}