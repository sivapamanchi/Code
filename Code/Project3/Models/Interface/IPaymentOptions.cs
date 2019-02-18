using BGSitecore.Models.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGSitecore.Models.Interface
{

    public interface IPaymentOption
    {
        string ViewName { get; }

        bool IsDefault { get; set; }

        string ModelType { get;  }
        object GetModal(AccountViewModel accountInfo);

        bool IsDisplay(AccountViewModel accountInfo);
    }

}
