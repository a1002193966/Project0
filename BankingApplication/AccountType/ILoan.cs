using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public interface ILoan : IAccount
    {
        int Term { get; set; }
    }
}
