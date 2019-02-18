using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco;
using Umbraco.Core.Models;


namespace BGModern.Models
{
    public class MyPointsModel : MasterModel
    {
        public String Message { get; set; }

        public Boolean HidePanelChoice { get; set; }
        public Boolean HideAccountInfo { get; set; }
        public Boolean HideFreeStay { get; set; }
        public Boolean HideLinkChoice { get; set; }
        public Boolean HideLinkGoGreen { get; set; }
        public Boolean HideLinkMyPoints { get; set; }
        public Boolean HidePageDisclaimer { get; set; }
        public Boolean HidePanelReminder { get; set; }
        public Boolean HidePaymentInfo { get; set; }
        public Boolean HidePremier { get; set; }
        public Boolean HideRestricted { get; set; }
        public Boolean HideSavePointsButton { get; set; }
        public Boolean Show4K { get; set; }

        public Boolean IsSamplerOwner { get; set; }
        public String HomeProject { get; set; } 

        public CreditCardInfoModel CreditCardInfo { get; set; }

        public String ConvertYourPoints { get; set; }
        public String ConvertYourPointsURL { get; set; }
        public String PageDisclaimer { get; set; }


        ////Data Grid elements
        public List<AccountModel> AccountInfo { get; set; }
        //public MyPointsListModel AllPoints { get; set; }

  
        public MyPointsModel()
        {
            CreditCardInfo = new CreditCardInfoModel();
            //AllPoints = new MyPointsListModel();
        }

    }

}