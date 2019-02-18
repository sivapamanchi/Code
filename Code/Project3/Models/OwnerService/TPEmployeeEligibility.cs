using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService
{
    public class TPEmployeeEligibilityReq
    {
        private string emailAddressField;

        private string passwordField;

        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;                
            }
        }

        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;               
            }
        }
    }

    public  class TPEmployeeEligibilityResp
    {

        private Error[] errorsField;

        private object memberIdField;

        private object employeeIdField;

        private object firstNameField;

        private object lastNameField;

        private object titleField;

        private object regionField;

        private object addressField;

        private object cityField;

        private object stateField;

        private object countryField;

        private object zipCodeField;

        private object phoneField;

        private object emailField;

      
        public Error[] Errors
        {
            get
            {
                return this.errorsField;
            }
            set
            {
                this.errorsField = value;
              
            }
        }

        
        public object MemberId
        {
            get
            {
                return this.memberIdField;
            }
            set
            {
                this.memberIdField = value;
               
            }
        }

      
        public object EmployeeId
        {
            get
            {
                return this.employeeIdField;
            }
            set
            {
                this.employeeIdField = value;
                
            }
        }

       
        public object FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
                
            }
        }

      
        public object LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
                
            }
        }

       
        public object Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
               
            }
        }

        public object Region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
             
            }
        }

  
        public object Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
               
            }
        }

      
        public object City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
               
            }
        }

     
        public object State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
               
            }
        }

      
        public object Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
             
            }
        }

      
        public object ZipCode
        {
            get
            {
                return this.zipCodeField;
            }
            set
            {
                this.zipCodeField = value;
              
            }
        }

      
        public object Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
              
            }
        }

       
        public object Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
               
            }
        }

        
    }
}