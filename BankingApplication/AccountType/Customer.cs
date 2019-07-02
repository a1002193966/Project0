using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountType
{
    public class Customer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<CheckingAccount> CheckingAccount = new List<CheckingAccount>();
        public List<BusinessAccount> BusinessAccount=new List<BusinessAccount>();
        public List<Loan> Loan=new List<Loan>();
        public List<TermDeposit> TermDeposit=new List<TermDeposit>();

        public Customer(string firstname, string lastname, string username, string password)
        {
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Username = username;
            this.Password = password;
        }          
    }
}
