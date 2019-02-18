using System.Collections.Generic;

namespace BGSitecore.Models.MortgageService
{

    public class GetLoansSummaryByArvactRequest
    {
        public string Arvact { get; set; }

        public string Error { get; set; } = "0";
        
    }

    public class GetLoansSummaryByArvact
    {
        public List<Loans> Loans { get; set; } = new List<Loans>();
    }

    public class Loans
    {
        public string LoanNbr { get; set; }

        public string Delq_Days { get; set; }
        public string Delq_Amnt { get; set; }

        public string Loan_Status { get; set; }

    }
}