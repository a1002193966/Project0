using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public class BusinessAccount : CheckingAccount, ICnBaccount
    {
        public BusinessAccount(double initDep = 0.00)
        {
            this.AccountNumber = NumberGen.rnd.Next(100000000, 1000000000);
            this.RoutingNumber = NumberGen.rnd.Next(10000000, 100000000);
            this.Balance = initDep;
            this.isActive = true;
            this.Transaction = new List<string>();
            this.Transaction.Add($"Account Type: Business | Account#: {this.AccountNumber}");
            this.Transaction.Add($"Account opened on {DateTime.Now.ToString()}");
        }
    }
}
