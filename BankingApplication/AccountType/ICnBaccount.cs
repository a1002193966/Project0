using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public interface ICnBaccount : IAccount
    {   
        int RoutingNumber { get; set; }     
    }
}
