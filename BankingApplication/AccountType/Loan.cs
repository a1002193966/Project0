using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public class Loan : ILoan
    {
        public int AccountNumber { get; set; }
        public double InterestRate { get; set; }
        public double Balance { get; set; }
        public int Term { get; set; }
        public bool isActive { get; set; }
        public List<string> Transaction { get; set; }

        public Loan(double loanAmount, int term, double apr)
        {
            this.AccountNumber = NumberGen.rnd.Next(100000000, 1000000000);
            this.Balance = loanAmount;
            this.Term = term;
            this.InterestRate = apr;
            this.isActive = true;
            this.Transaction = new List<string>();
            this.Transaction.Add($"Account Type: Loan | Account#: {this.AccountNumber}");
            this.Transaction.Add($"Account opened on {DateTime.Now.ToString()}");
            this.Transaction.Add($"Loan Amount: ${this.Balance} | Term: {this.Term} Months | APR: {this.InterestRate*100}%");
        }
    }
}
