using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public class TermDeposit : ITermDeposit
    {
        public int AccountNumber { get; set; }
        public double InterestRate { get; set; }
        public double Balance { get; set; }
        public int MaturityDate { get; set; }
        public bool isActive { get; set; }
        public List<string> Transaction { get; set; }

        public TermDeposit(double depositAmount, int maturityDate, double apr)
        {
            this.AccountNumber = NumberGen.rnd.Next(100000000, 1000000000);
            this.Balance = depositAmount;
            this.MaturityDate = maturityDate;
            this.InterestRate = apr;
            this.isActive = true;
            this.Transaction = new List<string>();
            this.Transaction.Add($"Account Type: Term Deposit | Account#: {this.AccountNumber}");
            this.Transaction.Add($"Account opened on {DateTime.Now.ToString()}");
            this.Transaction.Add($"Deposit Amount: ${this.Balance} | Maturity Date: {this.MaturityDate} | APR: {this.InterestRate*100}%");
        }
    }
}
