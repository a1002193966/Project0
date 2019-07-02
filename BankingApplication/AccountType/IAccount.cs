using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public interface IAccount
    {
        int AccountNumber { get; set; }
        double InterestRate { get; set; }
        double Balance { get; set; }
        bool isActive { get; set; }
        List<string> Transaction { get; set; }
    }
}
