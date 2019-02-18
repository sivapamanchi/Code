using BGSitecore.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGSitecore.Models.Interface
{
    public interface IPayment
    {
        string AmountString { get; set; }
        decimal Amount { get; set; }

    }
}
