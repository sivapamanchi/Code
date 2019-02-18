using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BGModern.Models
{
    public class CreditCardInfoModel
    {
        //Private Member Variables
        private String _mccError = String.Empty;
        
        //Public Variables
        public String Payment { get; set; }
        public String PaymentFromServiceCall { get; set; }

        //public Boolean DisplaySaveMyPoints { get; set; }
        
        public List<SelectListItem> lstExpDateMonth;
        public List<SelectListItem> lstExpDateYears; 
        public SelectList lstCreditCardTypes;
        
        public String ccError { get {return _mccError; }
                                set{ _mccError = value;} 
                               }

        public List<String> CreditCardInfoErrors { get; set; }

        //[Required]
        public String CreditCardName { get; set; }
        //[Required]
        public String CreditCardNumber {get;set;}
        [Required]
        public String CreditCardType {get;set;}
        [Required]
        public int CreditCardExpDateMonth {get;set;}
        [Required]
        public int CreditCardExpDateYear { get; set; }
        //[Required]
        public string CreditCardZipCode { get; set; }

        public Boolean InternationalZipCode {get;set;}

        
    }
}